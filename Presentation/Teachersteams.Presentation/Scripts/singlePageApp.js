var app = angular.module("ttApp");
app.config(function ($stateProvider) {
    $stateProvider
        .state('teacher/groups', {
            url: '/teacher/groups',
            templateUrl: '/Teacher/Group',
            controller: 'ttTeacherGroupsController'
        })
        .state('teacher/groups.details', {
            url: '/teacher/groups/details/{groupId}',
            templateUrl: '/Teacher/Group/Details',
            controller: function ($scope, $stateParams) {
                $scope.groupId = $stateParams.groupId;
            }
        })
        .state('teacher/board', {
            url: '/teacher/board',
            templateUrl: '/Teacher/Board',
            controller: 'ttTeacherBoardController'
        })
        .state('teacher/notifications', {
            url: '/teacher/notifications',
            templateUrl: '/Teacher/Notification',
            controller: 'ttTeacherNotificationsController'
        })
        .state('student/groups', {
            url: '/student/groups',
            templateUrl: '/Student/Group',
            controller: 'ttStudentGroupsController'
        })
        .state('student/board', {
            url: '/student/board',
            templateUrl: '/Student/Board',
            controller: 'ttStudentBoardController'
        })
        .state('student/notifications', {
            url: '/student/notifications',
            templateUrl: '/Student/Notification',
            controller: 'ttStudentNotificationsController'
        })
        .state('about', {
            url: '/about',
            templateUrl: '/Home/About',
            controller: 'ttAboutController'
        });
});

app.config(['ngDialogProvider', function (ngDialogProvider) {
    ngDialogProvider.setDefaults({
        className: 'ngdialog-theme-default',
        showClose: true,
        closeByDocument: true,
        closeByEscape: true
    });
}]);

app.config(['ngToastProvider', function (ngToastProvider) {
    ngToastProvider.configure({
        animation: 'slide',
        dismissOnTimeout: true,
        timeout: 4000,
        dismissButton: true,
        horizontalPosition: 'right',
        verticalPosition: 'top',
        maxNumber: 5
    });
}]);

app.value('AppContext', {});