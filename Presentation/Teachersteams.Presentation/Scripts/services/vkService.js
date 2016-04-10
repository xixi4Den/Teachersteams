angular.module('ttServices')
    .factory('$vk', ['$q', function ($q) {
        return {
            init: function(func) {
                VK.init(function() {
                    func();
                });
            },

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
            },

            simpleCall: function (method, data) {
                var deferred = $q.defer();
                VK.init(function () {
                    VK.api(method, data, function (result) {
                        if (result.response) {
                            return deferred.resolve(result);
                        } else {
                            return deferred.reject(result);
                        }
                    });
                    return deferred;
                });
                return deferred.promise;
            }
        }
    }]);