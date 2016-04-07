angular.module('ttDirectives')
    .directive('loader', ['$http', function ($http) {
        return {
            restrict: 'A',
            template:   '<div class=\'loader\' ng-show=\'isLoading()\' ng-hide=\'!isLoading()\'></div>', //+ 
                        //'<div class=\'overlay\' ng-show=\'isLoading()\' ng-hide=\'!isLoading()\'></div>',
            scope: {},
            controller: ['$scope', function ($scope) {
                $scope.isLoading = function () {
                    return $http.pendingRequests.length > 0;
                };
            }]
        };
    }]);