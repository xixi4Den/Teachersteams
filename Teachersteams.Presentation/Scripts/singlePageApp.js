var app = angular.module("ttSinglePageApp", ['ui.router', 'ui.bootstrap', 'ttServices', 'NgSwitchery']);
app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("/groups");
    $stateProvider
        .state('groups', {
            url: '/groups',
            templateUrl: '/Groups/Teacher',
            controller: 'ttTeacherGroupsController'
        })
        .state('board', {
            url: '/board',
            templateUrl: '/Board',
            controller: 'ttBoardController'
        })
        .state('notifications', {
            url: '/notifications',
            templateUrl: '/Notifications',
            controller: 'ttNotificationsController'
        })
        .state('about', {
            url: '/about',
            templateUrl: '/About',
            controller: 'ttAboutController'
        });
});