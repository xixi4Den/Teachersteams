angular.module('ttApp', [
    'ui.router',
    'ui.bootstrap',
    'ui.bootstrap.tabset',
    'ngAnimate',
    'NgSwitchery',
    'ngDialog',
    'ngMessages',
    'ngToast',
    'ui.grid',
    'ui.grid.pagination',
    'ttControllers',
    'ttDirectives'
]);

angular.module('ttControllers', ['ttServices', 'ngAnimate']);

angular.module('ttServices', []);

angular.module('ttDirectives', []);