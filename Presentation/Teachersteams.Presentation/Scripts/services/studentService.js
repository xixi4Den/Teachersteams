angular.module('ttServices')
    .factory('$ttStudentService', ['$ttUserService', function ($ttUserService) {
        var urls = {
            inviteUrl: 'Student/Post',
            getUrl: 'Student/Get',
            countUrl: 'Student/Count',
            requestsUrl: 'Student/Requests',
            anyRequestUrl: 'Student/AnyRequest',
            requestsCountUrl: 'Student/RequestsCount',
            responseUrl: 'Student/Response',
            deleteUrl: 'Student/Delete'
        }

        return angular.extend($ttUserService(urls), {});
    }]);