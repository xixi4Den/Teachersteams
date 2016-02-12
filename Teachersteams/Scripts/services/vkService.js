angular.module('services', [])
    .factory('$vk', ['$q', function ($q) {
        return {
            call: function (method, data) {
                var deferred = $q.defer();
                VK.api(method, data, function (result) {
                    if (result.response) {
                        deferred.resolve(result);
                    } else {
                        deferred.reject(result);
                    }
                });
                return deferred.promise;
            }
        }
    }]);