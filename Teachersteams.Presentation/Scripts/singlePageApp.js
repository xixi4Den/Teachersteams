var app = angular.module("ttSinglePageApp", ['ui.router', 'ui.bootstrap', 'ttServices', 'NgSwitchery']);
app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("/teacher/groups");
    $stateProvider
        .state('teacher/groups', {
            url: '/teacher/groups',
            templateUrl: '/Groups/Teacher',
            controller: 'ttTeacherGroupsController'
        })
        .state('teacher/board', {
            url: '/teacher/board',
            templateUrl: '/Board/Teacher',
            controller: 'ttTeacherBoardController'
        })
        .state('teacher/notifications', {
            url: '/teacher/notifications',
            templateUrl: '/Notifications/Teacher',
            controller: 'ttTeacherNotificationsController'
        })
        .state('student/groups', {
            url: '/student/groups',
            templateUrl: '/Groups/Student',
            controller: 'ttStudentGroupsController'
        })
        .state('student/board', {
            url: '/student/board',
            templateUrl: '/Board/Student',
            controller: 'ttStudentBoardController'
        })
        .state('student/notifications', {
            url: '/student/notifications',
            templateUrl: '/Notifications/Student',
            controller: 'ttStudentNotificationsController'
        })
        .state('about', {
            url: '/about',
            templateUrl: '/About',
            controller: 'ttAboutController'
        });
});