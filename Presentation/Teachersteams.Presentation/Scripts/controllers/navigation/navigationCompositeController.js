var app = angular.module("ttControllers");
app.controller('ttNavigationCompositeController', ['$scope', '$timeout', function ($scope, $timeout) {
    $scope.isCollapsed = true;
    $scope.$on('$routeChangeSuccess', function () {
        $scope.isCollapsed = true;
    });

    function decodeNewLines(str) {
        var find = "&lt;br/&gt;";
        var reg = new RegExp(find, 'g');
        return str.replace(reg, '<br/>');
    }

    $timeout(function () {
        $scope.introOptions = {
            steps: [
                {
                    element: document.querySelector('.tt-brand'),
                    intro: decodeNewLines(window.resources.introMessageCommon)
                },
                {
                    element: document.querySelector('.tt-switch-user-mode'),
                    intro: decodeNewLines(window.resources.introMessageSeparateUserMode)
                },
                {
                    element: document.querySelector('.tt-groups-nav-tab'),
                    intro: decodeNewLines(window.resources.introMessageGroupsTab)
                },
                {
                    element: document.querySelector('.tt-board-nav-tab'),
                    intro: decodeNewLines(window.resources.introMessageBoardTab)
                },
                {
                    element: document.querySelector('.tt-notifications-nav-tab'),
                    intro: decodeNewLines(window.resources.introMessageNotificationsTab)
                }
            ],
            showStepNumbers: false,
            showBullets: true,
            exitOnOverlayClick: true,
            exitOnEsc: true,
            nextLabel: window.resources.introDialogNextLabel,
            prevLabel: window.resources.introDialogPreviousLabel,
            skipLabel: window.resources.introDialogExitLabel,
            doneLabel: window.resources.introDialogDoneLabel
        };
    }, 500);

    //this is a trick to update elements, because they do not update automatically (issue of introjs)
    $scope.beforeChange = function () {
        this._introItems[2].element = document.querySelector('.tt-groups-nav-tab');
        this._introItems[3].element = document.querySelector('.tt-board-nav-tab');
        this._introItems[4].element = document.querySelector('.tt-notifications-nav-tab');
    }
}]);