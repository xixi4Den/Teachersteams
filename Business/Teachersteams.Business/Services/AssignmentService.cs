using System;
using System.Collections.Generic;
using Autofac.Features.Indexed;
using AutoMapper;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Exceptions;
using Teachersteams.Business.Extensions;
using Teachersteams.Business.Helpers;
using Teachersteams.Business.Retrievers.Assignment;
using Teachersteams.Business.Services.Contracts;
using Teachersteams.Business.ViewModels.Assignment;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;
using Teachersteams.Domain.Query;
using Teachersteams.Shared.Validation;
using AssignmentStatus = Teachersteams.Domain.Enums.AssignmentStatus;

namespace Teachersteams.Business.Services
{
    public class AssignmentService: IAssignmentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IGridOptionsHelper gridOptionsHelper;
        private readonly IIndex<UserType, IAssignmentRetriever> assignmentRetrievers;

        public AssignmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IGridOptionsHelper gridOptionsHelper,
            IIndex<UserType, IAssignmentRetriever> assignmentRetrievers)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.gridOptionsHelper = gridOptionsHelper;
            this.assignmentRetrievers = assignmentRetrievers;
        }

        public AssignmentViewModel CreateAssignment(string uid, AssignmentViewModel viewModel)
        {
            ValidateCreator(uid, viewModel);

            var entity = mapper.Map<Assignment>(viewModel);
            entity.Status = AssignmentStatus.Active;
            var insertedEntity = unitOfWork.InsertOrUpdate(entity);
            unitOfWork.Commit();
            return mapper.Map<AssignmentViewModel>(insertedEntity);
        }

        public IEnumerable<AssignmentViewModel> GetAllAssignments(Guid groupId, UserType userType, string uid, GridOptions gridOptions)
        {
            return assignmentRetrievers[userType].Retrieve(groupId, uid, gridOptions);
        }

        public int AssignmentCount(Guid groupId)
        {
            return unitOfWork.Count(new QueryParameters<Assignment>
            {
                FilterRules = x => x.GroupId == groupId,
            });
        }

        public void CompleteAssignment(string uid, AssignmentCompletionViewModel viewModel)
        {
            var assignment = unitOfWork.Get<Assignment>(viewModel.AssignmentId);
            var newEntity = mapper.Map<AssignmentResult>(viewModel);
            SetStudent(newEntity, uid, assignment.GroupId);
            SetComplitionDate(newEntity, assignment);
            ValidateAssignmnetResult(newEntity);
            unitOfWork.InsertOrUpdate(newEntity);
            unitOfWork.Commit();
        }

        public IEnumerable<AssignmentResultViewModel> GetAssignmentResults(Guid assignmentId, GridOptions gridOptions)
        {
            var results = unitOfWork.GetAll(new QueryParameters<AssignmentResult>
            {
                FilterRules = x => x.AssignmentId == assignmentId,
                PageRules = new PageSettings(gridOptions.PageNumber, gridOptions.PageSize),
                SortRules = gridOptionsHelper.BuidDynamicOrderedQuery<AssignmentResult>(gridOptions)
            });

            return mapper.MapManyTo<AssignmentResultViewModel>(results);
        }

        public int ResultCount(Guid assignmentId)
        {
            return unitOfWork.Count(new QueryParameters<AssignmentResult>
            {
                FilterRules = x => x.AssignmentId == assignmentId,
            });
        }

        public AssignmentResultViewModel GetAssignmentResult(Guid assignmentId, string studentUid)
        {
            var result = unitOfWork.GetSingleOrDefault(new QueryParameters<AssignmentResult>
            {
                FilterRules = x => x.AssignmentId == assignmentId && x.Student.Uid == studentUid
            });
            return mapper.Map<AssignmentResultViewModel>(result);
        }


        public void AssignResult(Guid assignmentResultId, string teacherUid)
        {
            var assignmentResult = unitOfWork.Get<AssignmentResult>(assignmentResultId);

            if (assignmentResult.AssigneeTeacherId.HasValue)
            {
                throw new AssignmentResultAlreadyAssignedException();
            }

            var teacher = unitOfWork.GetSingleOrDefault(new QueryParameters<Teacher>
            {
                FilterRules = x => x.Uid == teacherUid
            });

            if (assignmentResult.Assignment.Group.OwnerId != teacherUid && assignmentResult.Assignment.GroupId != teacher.GroupId)
            {
                throw new InvalidOperationException();
            }

            assignmentResult.AssigneeTeacherId = teacher.Id;

            unitOfWork.InsertOrUpdate(assignmentResult);
            unitOfWork.Commit();
        }

        public void GradeAssignmentResult(Guid assignmentResultId, byte grade, string teacherUid)
        {
            var assignmentResult = unitOfWork.Get<AssignmentResult>(assignmentResultId);

            if (grade > 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            var teacher = unitOfWork.GetSingleOrDefault(new QueryParameters<Teacher>
            {
                FilterRules = x => x.Uid == teacherUid
            });

            if (assignmentResult.Assignment.Group.OwnerId != teacherUid && assignmentResult.Assignment.GroupId != teacher.GroupId)
            {
                throw new InvalidOperationException();
            }

            if (!assignmentResult.AssigneeTeacherId.HasValue || assignmentResult.AssigneeTeacherId != teacher.Id)
            {
                throw new InvalidOperationException();
            }

            assignmentResult.Grade = grade;
            assignmentResult.CheckDate = DateTime.UtcNow;

            unitOfWork.InsertOrUpdate(assignmentResult);
            unitOfWork.Commit();
        }

        private void ValidateCreator(string uid, AssignmentViewModel viewModel)
        {
            Contract.NotNull<ArgumentNullException>(viewModel);
            Contract.NotDefault<Guid, ArgumentException>(viewModel.GroupId);
            Contract.NotNullAndNotEmpty<ArgumentException>(viewModel.File);

            var isValidTeacher = unitOfWork.Any(new QueryParameters<Teacher>
            {
                FilterRules = x => x.Uid == uid && x.GroupId == viewModel.GroupId
            });

            var isOwner = unitOfWork.Any(new QueryParameters<Group>
            {
                FilterRules = x => x.Id == viewModel.GroupId && x.OwnerId == uid
            });

            Contract.Assert<UnauthorizedAccessException>(isValidTeacher || isOwner);
        }

        private void SetStudent(AssignmentResult newEntity, string uid, Guid groupId)
        {
            var student = unitOfWork.GetSingleOrDefault(new QueryParameters<Student>
            {
                FilterRules = x => x.Uid == uid && x.GroupId == groupId
            });

            newEntity.StudentId = student.Id;
        }

        private void SetComplitionDate(AssignmentResult newEntity, Assignment assignment)
        {
            newEntity.CompletionDate = DateTime.UtcNow;

            if (newEntity.CompletionDate > assignment.ExpirationDate)
            {
                ExpireAssignment(assignment);
                throw new ExpiredAssignmentException();
            }
        }

        private void ExpireAssignment(Assignment assignment)
        {
            assignment.Status = AssignmentStatus.Expired;
            unitOfWork.InsertOrUpdate(assignment);
            unitOfWork.Commit();
        }

        private void ValidateAssignmnetResult(AssignmentResult newEntity)
        {
            var doesResultExist = unitOfWork.Any(new QueryParameters<AssignmentResult>
            {
                FilterRules = x => x.StudentId == newEntity.StudentId && x.AssignmentId == newEntity.AssignmentId
            });

            if (doesResultExist)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
