angular.module('ttServices')
    .factory('$ttAssignmentService', [
        '$userHttp',
        'AppContext',
        'AssignmentStatus',
        '$vk',
        '$q',
        function ($userHttp, AppContext, AssignmentStatus, $vk, $q) {
            var createUrl = 'assignment/Post';
            var getAllUrl = 'assignment/GetAll';
            var countUrl = 'assignment/Count';
            var downloadAssignmentUrl = 'assignment/Download?fileType=1&file=';
            var downloadResultUrl = 'assignment/Download?fileType=2&file=';
            var completeUrl = 'assignment/CompleteAssignment';
            var getResultsUrl = 'assignment/GetAssignmentResults';
            var resultsCountUrl = 'assignment/ResultsCount';
            var assignResultUrl = 'assignment/assignResult';
            var gradeResultUrl = 'assignment/gradeResult';

            function addDownloadResultUrl(collection) {
                return _.map(collection, function (val) {
                    val.FileUrl = '' + AppContext.apiUrl + downloadResultUrl + val.File;
                    return val;
                });
            }

            function getStudentName(data) {
                var uids = _.map(data, function (item) {
                    return item.StudentUid;
                });
                if (!uids.length) {
                    var deferred = $q.defer();
                    deferred.resolve({ response: [] });
                    return deferred.promise;
                }
                return $vk.call('users.get', {
                    uids: uids,
                    fields: "uid, first_name, last_name, photo"
                });
            }

            function getAssigneeName(data) {
                var uids = _.map(data, function (item) {
                    return item.AssigneeTeacherUid;
                });
                uids = _.without(uids, null);
                if (!uids.length) {
                    var deferred = $q.defer();
                    deferred.resolve({ response: [] });
                    return deferred.promise;
                }
                return $vk.call('users.get', {
                    uids: uids,
                    fields: "uid, first_name, last_name"
                });
            }

            function addStudentName(data, studentsInfo) {
                _.each(studentsInfo.response, function (item) {
                    var originalItem = _.findWhere(data, { StudentUid: item.uid.toString() });
                    var name = item.last_name + ', ' + item.first_name;
                    originalItem["StudentName"] = name;
                    originalItem["Photo"] = item.photo;
                });
            }

            function addAssigneeName(data, assigneesInfo) {
                _.each(assigneesInfo.response, function (teacher) {
                    var originalItems = _.filter(data, function (item) { return item.AssigneeTeacherUid === teacher.uid.toString() });
                    var name = teacher.last_name + ', ' + teacher.first_name;
                    _.each(originalItems, function(item) {
                        item["AssigneeName"] = name;
                    });
                });
            }

            return {
                create: function (newAssignment) {
                    return $userHttp.post(createUrl, newAssignment);
                },

                get: function (groupId, paginationOptions) {
                    return $userHttp.get(getAllUrl, {
                        groupId: groupId,
                        pageNumber: paginationOptions.PageNumber,
                        pageSize: paginationOptions.PageSize,
                        sortingColumn: paginationOptions.SortingColumn,
                        sortingDirection: paginationOptions.SortingDirection
                    }).then(function (r) {
                        _.map(r.data, function (val, key) {
                            val.StatusName = AssignmentStatus.localizedName[val.Status]();
                            val.FileUrl = '' + AppContext.apiUrl + downloadAssignmentUrl + val.File;
                            return val;
                        });
                        return r.data;
                    });
                },

                count: function (groupId) {
                    return $userHttp.get(countUrl, {
                        groupId: groupId
                    });
                },

                complete: function (result) {
                    return $userHttp.post(completeUrl, result);
                },

                getResults: function (assignmentId, paginationOptions) {
                    var data = null;
                    return $userHttp.get(getResultsUrl, {
                        assignmentId: assignmentId,
                        pageNumber: paginationOptions.PageNumber,
                        pageSize: paginationOptions.PageSize,
                        sortingColumn: paginationOptions.SortingColumn,
                        sortingDirection: paginationOptions.SortingDirection
                    }).then(function(r) {
                        data = addDownloadResultUrl(r.data);
                        return $q.all([getStudentName(data), getAssigneeName(data)]);
                    }).then(function(values) {
                        addStudentName(data, values[0]);
                        addAssigneeName(data, values[1]);
                        return data;
                    });
                },

                resultsCount: function (assignmentId) {
                    return $userHttp.get(resultsCountUrl, {
                        assignmentId: assignmentId
                    });
                },

                assignResult: function(assignmentResultId) {
                    return $userHttp.post(assignResultUrl, null, {
                        assignmentResultId: assignmentResultId
                    });
                },

                gradeResult: function (assignmentResultId, grade) {
                    return $userHttp.post(gradeResultUrl, null, {
                        assignmentResultId: assignmentResultId,
                        grade: grade
                    });
                }
            }
    }]);