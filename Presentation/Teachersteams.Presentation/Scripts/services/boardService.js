angular.module('ttServices')
    .factory('$ttBoardService', [
        '$userHttp',
        '$q',
        '$vk',
        'AppContext',
        function ($userHttp, $q, $vk, AppContext) {
            var getForStudentUrl = 'board/GetForStudent';
            var countForStudentUrl = 'board/CountForStudent';
            var getForTeacherUrl = 'board/GetForTeacher';
            var countForTeacherUrl = 'board/CountForTeacher';
            var downloadAssignmentUrl = 'assignment/Download?fileType=1&file=';
            var downloadResultUrl = 'assignment/Download?fileType=2&file=';

            function addDownloadAssignmentUrl(data) {
                return _.map(data, function (val) {
                    val.AssignmentFileUrl = '' + AppContext.apiUrl + downloadAssignmentUrl + val.AssignmentFile;
                    return val;
                });
            }

            function addDownloadResultUrl(data) {
                return _.map(data, function (val) {
                    if (val.AssignmentResultFile) {
                        val.AssignmentResultFileUrl = '' + AppContext.apiUrl + downloadResultUrl + val.AssignmentResultFile;
                    }
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
                    fields: "uid, first_name, last_name"
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
                _.each(studentsInfo, function (student) {
                    var originalItems = _.filter(data, function (item) { return item.StudentUid === student.uid.toString() });
                    var name = student.last_name + ', ' + student.first_name;
                    _.each(originalItems, function (item) {
                        item["StudentName"] = name;
                    });
                });
            }

            function addAssigneeName(data, assigneesInfo) {
                _.each(assigneesInfo, function (teacher) {
                    var originalItems = _.filter(data, function (item) { return item.AssigneeTeacherUid === teacher.uid.toString() });
                    var name = teacher.last_name + ', ' + teacher.first_name;
                    _.each(originalItems, function (item) {
                        item["AssigneeName"] = name;
                    });
                });
            }

            return { 
                getForStudent: function (filterType, paginationOptions) {
                    var data = null;
                    return $userHttp.get(getForStudentUrl, {
                        filterType: filterType,
                        pageNumber: paginationOptions.PageNumber,
                        pageSize: paginationOptions.PageSize,
                        sortingColumn: paginationOptions.SortingColumn,
                        sortingDirection: paginationOptions.SortingDirection
                    }).then(function (r) {
                        data = r.data;
                        addDownloadAssignmentUrl(data);
                        addDownloadResultUrl(data);
                        return getAssigneeName(data);
                    }).then(function(r) {
                        addAssigneeName(data, r.response);
                        return data;
                    });
                },

                countForStudent: function (filterType) {
                    return $userHttp.get(countForStudentUrl, {
                        filterType: filterType
                    });
                },

                getForTeacher: function (checkFilterType, assignFilterType, paginationOptions) {
                    var data = null;
                    return $userHttp.get(getForTeacherUrl, {
                        checkFilterType: checkFilterType,
                        assignFilterType: assignFilterType,
                        pageNumber: paginationOptions.PageNumber,
                        pageSize: paginationOptions.PageSize,
                        sortingColumn: paginationOptions.SortingColumn,
                        sortingDirection: paginationOptions.SortingDirection
                    }).then(function (r) {
                        data = r.data;
                        addDownloadAssignmentUrl(data);
                        addDownloadResultUrl(data);
                        return $q.all([getStudentName(data), getAssigneeName(data)]);
                    }).then(function (values) {
                        addStudentName(data, values[0].response);
                        addAssigneeName(data, values[1].response);
                        return data;
                    });
                },

                countForTeacher: function (checkFilterType, assignFilterType) {
                    return $userHttp.get(countForTeacherUrl, {
                        checkFilterType: checkFilterType,
                        assignFilterType: assignFilterType
                    });
                }
            }
        }]);