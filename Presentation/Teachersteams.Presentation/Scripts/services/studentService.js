angular.module('ttServices')
    .factory('$ttStudentService', ['$ttUserService', function ($ttUserService) {
        var urls = {
            inviteUrl: 'Student/Post',
            getUrl: 'Student/Get',
            countUrl: 'Student/Count'
        }

        return angular.extend($ttUserService(urls), {});
    }]);