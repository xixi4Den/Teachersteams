var app = angular.module("ttControllers");
app.controller('ttCreateAssignmentsDialogController', [
    '$scope',
    'FileUploader',
    'ngToast',
    function ($scope, FileUploader, ngToast) {
        $scope.file = null;

        $scope.uploader = new FileUploader({
            url: 'https://localhost:44302/api/Assignment/Upload?fileType=1',
            autoUpload: true,
            queueLimit: 1,
            removeAfterUpload: true,
        });

        $scope.uploader.onSuccessItem = function(item, response, status, headers) {
            $scope.file = response.FileName;
            
        }

        $scope.uploader.onErrorItem = function (item, response, status, headers) {
            ngToast.danger(response.data);
        }


        $scope.isProgressBarVisible = function () {
            return $scope.uploader.isUploading || $scope.file;
        }

        $scope.getProgress = function () {
            return $scope.file ? 100 : $scope.uploader.progress;
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
                if (!$scope.file) {
                    ngToast.danger(window.resources.fileIsNotAttachedMessage);
                    return;
                }
                ngToast.success(window.resources.assignmentCreatedMessage);
                //$ttAssignmentService.create($scope.data)
                //    .then(function (response) {
                //        ngToast.success("New assignment has been added");
                //        $scope.closeThisDialog(response.data);
                //    });
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