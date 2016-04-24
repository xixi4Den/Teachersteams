angular.module('ttServices')
    .factory('$userHttp', ['$http', '$q', 'AppContext', 'ngToast', function ($http, $q, AppContext, ngToast) {
        function buildUrl(relativePath) {
            if (typeof AppContext.apiUrl === "undefined") {
                throw new Error("Url to business api is missing.");
            }
            return AppContext.apiUrl + relativePath;
        }

        function buildParams(params) {
            if (typeof AppContext.uid === "undefined") {
                throw new Error("UserId is missing.");
            }
            params = typeof params !== 'undefined' ? params : {};
            params.userId = AppContext.uid;
            return params;
        }

        return {
            get: function (relativePath, params) {
                var url = buildUrl(relativePath);
                var userSpecificParams = buildParams(params);

                return $http.get(url, {
                    params: userSpecificParams
                }).catch(function (response) {
                    ngToast.danger(response.data.Message);
                    return $q.reject(response);
                });
            },

            post: function (relativePath, data, params) {
                var url = buildUrl(relativePath);
                var userSpecificParams = buildParams(params);

                return $http.post(url, data, {
                    params: userSpecificParams
                }).catch(function (response) {
                    ngToast.danger(response.data.Message);
                    return $q.reject(response);
                });
            },

            delete: function (relativePath, params) {
                var url = buildUrl(relativePath);
                var userSpecificParams = buildParams(params);

                return $http.delete(url, {
                    params: userSpecificParams
                }).catch(function (response) {
                    ngToast.danger(response.data.Message);
                    return $q.reject(response);
                });
            }
        }
    }]);