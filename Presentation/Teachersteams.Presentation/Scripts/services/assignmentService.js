angular.module('ttServices')
    .factory('$ttAssignmentService', ['$userHttp', function ($userHttp) {
        var createUrl = 'assignment/Post';

        return {
            create: function (newAssignment) {
                return $userHttp.post(createUrl, newAssignment);
            },
        }
    }]);