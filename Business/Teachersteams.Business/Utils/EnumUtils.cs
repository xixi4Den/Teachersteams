using System;
using Teachersteams.Business.Enums;

namespace Teachersteams.Business.Utils
{
    public static class EnumUtils
    {
        public static TeacherBoardCompositeFilterType GetTeacherBoardCompositeFilterType(
            TeacherBoardCheckFilterType? checkFilterType, TeacherBoardAssignFilterType assignFilterType)
        {
            if (!checkFilterType.HasValue && assignFilterType == TeacherBoardAssignFilterType.NotAssigned)
            {
                return TeacherBoardCompositeFilterType.NotAssigned;
            }
            if (checkFilterType.HasValue)
            {
                var compositeValue = (byte) assignFilterType | (byte) checkFilterType.Value;
                return (TeacherBoardCompositeFilterType) (compositeValue);
            }
            throw new ArgumentException("incorrect combination of check filter and assign filter");
        }
    }
}
