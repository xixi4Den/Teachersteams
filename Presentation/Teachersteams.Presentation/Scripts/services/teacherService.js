angular.module('ttServices')
    .factory('$ttTeacherService', ['$ttUserService', function ($ttUserService) {
        var urls = {
            inviteUrl: 'Teacher/Post',
            getUrl: 'Teacher/Get',
            countUrl: 'Teacher/Count',
            requestsUrl: 'Teacher/Requests',
            responseUrl: 'Teacher/Response'
        }

        return angular.extend($ttUserService(urls), {});
    }]);