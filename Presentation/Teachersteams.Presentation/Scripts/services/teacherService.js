angular.module('ttServices')
    .factory('$ttTeacherService', ['$ttUserService', function ($ttUserService) {
        var urls = {
            inviteUrl: 'Teacher/Post',
            getUrl: 'Teacher/Get',
            countUrl: 'Teacher/Count',
            requestsUrl: 'Teacher/Requests',
            anyRequestUrl: 'Teacher/AnyRequest',
            requestsCountUrl: 'Teacher/RequestsCount',
            responseUrl: 'Teacher/Response',
            deleteUrl: 'Teacher/Delete'
        }

        return angular.extend($ttUserService(urls), {});
    }]);