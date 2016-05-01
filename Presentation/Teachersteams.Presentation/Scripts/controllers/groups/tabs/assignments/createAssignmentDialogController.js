var app = angular.module("ttControllers");
app.controller('ttCreateAssignmentsDialogController', [
    '$scope',
    'FileUploader',
    'ngToast',
    '$ttAssignmentService',
    'AppContext',
    '$stateParams',
    function ($scope, FileUploader, ngToast, $ttAssignmentService, AppContext, $stateParams) {
        $scope.data = {
            file: null,
        };

        $scope.uploader = new FileUploader({
            url: AppContext.apiUrl + 'Assignment/Upload?fileType=1',
            autoUpload: true,
            queueLimit: 1,
            removeAfterUpload: true,
        });

        $scope.uploader.onSuccessItem = function(item, response, status, headers) {
            $scope.data.file = response.FileName;
        }

        $scope.uploader.onErrorItem = function (item, response, status, headers) {
            ngToast.danger(response.data);
        }


        $scope.isProgressBarVisible = function () {
            return $scope.uploader.isUploading || $scope.data.file;
        }

        $scope.getProgress = function () {
            return $scope.data.file ? 100 : $scope.uploader.progress;
        }

        $scope.isUploadingToCloud = function() {
            return $scope.uploader.isUploading && $scope.uploader.progress === 100;
        }

        $scope.expirationDatePopup = {
            opened: false,
            open: function() {
                $scope.expirationDatePopup.opened = true;
            }
        };

        var today = new Date();
        var yearAhead = new Date();
        yearAhead.setFullYear(yearAhead.getFullYear() + 1, yearAhead.getMonth(), yearAhead.getHours());

        $scope.dateOptions = {
            formatYear: 'yy',
            maxDate: yearAhead,
            minDate: today,
            startingDay: 1
        };

        $scope.create = function (e) {
            if ($scope.addAssignmetForm.$valid) {
                if (!$scope.data.file) {
                    ngToast.danger(window.resources.fileIsNotAttachedMessage);
                    return;
                }
                $scope.data.groupId = $stateParams.groupId;
                $ttAssignmentService.create($scope.data)
                    .then(function (response) {
                        ngToast.success(window.resources.assignmentCreatedMessage);
                        $scope.closeThisDialog(response.data);
                    });
            }
        }

        $scope.cancel = function (e) {
            if (confirm(window.resources.addAssignmentDialogCloseConfirmationMessage)) {
                $scope.uploader.cancelAll();
                $scope.uploader.destroy();
                $scope.closeThisDialog();
            }
        }
}]);