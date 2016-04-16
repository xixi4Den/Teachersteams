angular.module('ttDirectives')
    .directive('noItemsMessage', function ($compile, $timeout) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var msg = (attrs.showEmptyMsg) ? attrs.showEmptyMsg : 'No items';
                var template = "<p class='no-items' ng-hide='gridOptions.data.length'>" + msg + "</p>";
                var tmpl = angular.element(template);
                $compile(tmpl)(scope);
                $timeout(function () {
                    var s = angular.element(element[0].querySelector('.ui-grid-viewport'));
                    s.append(tmpl);
                }, 0);
            }
        };
    })