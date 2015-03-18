'use strict';

app.controller('BaseSettingsCtrl', ['$scope', '$http', function ($scope, $http) {

    $scope.entity = null;

    var getSettingsEntity = function () {

        setTimeout(function () {
            $http.get('/Admin/Settings/GetBaseSettings/').success(function (result) {
                $scope.entity = result;
            }).error(function () {
                console.log("请求发生错误");
            });
        });

    };

    getSettingsEntity();
}]);