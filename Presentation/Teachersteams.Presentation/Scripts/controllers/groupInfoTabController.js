var app = angular.module("ttControllers");
app.controller('ttGroupInfoTabController', ['$scope', '$ttGroupService', '$vk', function ($scope, $ttGroupService, $vk) {
    $ttGroupService.getInfo($scope.groupId).then(function(response) {
        $scope.title = response.data.Title;
        $scope.description = response.data.Description;
        $scope.createDate = response.data.CreateDate;
        $scope.ownerId = response.data.OwnerId;

        $vk.init(function () {
            $vk.call('users.get', {
                    uids: $scope.ownerId,
                    fields: "first_name, last_name, photo"
                })
                .then(function (r) {
                        var ownerFirstName = r.response[0].first_name;
                        var ownerLastName = r.response[0].last_name;
                        $scope.ownerName = ownerFirstName + ' ' + ownerLastName;
                        $scope.ownerPhoto = r.response[0].photo;
                    },
                function (r) {
                    console.log("Error!!!");
                });
        });
    });
}]);