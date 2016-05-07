var app = angular.module("ttControllers");
app.controller('ttCompleteAssignmentDialogController', [
    '$scope',
    'FileUploader',
    'ngToast',
    '$ttAssignmentService',
    'AppContext',
    function ($scope, FileUploader, ngToast, $ttAssignmentService, AppContext) {
        $scope.data = {
            assignmentId: $scope.ngDialogData.id,
            file: null,
        };

        $scope.uploader = new FileUploader({
            url: AppContext.apiUrl + 'Assignment/Upload?fileType=2',
            autoUpload: true,
            queueLimit: 1,
            removeAfterUpload: true,
        });

        $scope.uploader.onSuccessItem = function (item, response, status, headers) {
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

        $scope.isUploadingToCloud = function () {
            return $scope.uploader.isUploading && $scope.uploader.progress === 100;
        }

        $scope.complete = function (e) {
            if ($scope.completeAssignmetForm.$valid) {
                if (!$scope.data.file) {
                    ngToast.danger(window.resources.fileIsNotAttachedMessage);
                    return;
                }
                $ttAssignmentService.complete($scope.data)
                    .then(function (response) {
                        ngToast.success(window.resources.assignmentCompletedSuccessMessage);
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