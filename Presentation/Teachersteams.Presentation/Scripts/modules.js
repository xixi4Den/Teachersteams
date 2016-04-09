﻿angular.module('ttApp', [
    'ui.router',
    'ui.bootstrap',
    'ui.bootstrap.tabset',
    'ngAnimate',
    'NgSwitchery',
    'ngDialog',
    'ngMessages',
    'ngToast',
    'ttControllers',
    'ttDirectives'
]);

angular.module('ttControllers', ['ttServices', 'ngAnimate']);

angular.module('ttServices', []);

angular.module('ttDirectives', []);