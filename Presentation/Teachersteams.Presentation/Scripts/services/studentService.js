angular.module('ttServices')
    .factory('$ttStudentService', ['$ttUserService', function ($ttUserService) {
        var urls = {
            inviteUrl: 'Student/Post',
            getUrl: 'Student/Get',
            countUrl: 'Student/Count',
            requestsUrl: 'Student/Requests',
            responseUrl: 'Student/Response'
        }

        return angular.extend($ttUserService(urls), {});
    }]);