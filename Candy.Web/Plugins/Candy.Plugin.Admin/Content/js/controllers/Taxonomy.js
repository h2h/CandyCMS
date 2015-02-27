'use strict';

app.controller('CategoryCtrl', ['$scope', '$http', function ($scope, $http) {

    $scope.action = 0;
    $scope.entities = [];
    var getPagedDataAsync = function (pageSize, pageIndex) {
        setTimeout(function () {
            var data;
            $http.get('/Admin/Taxonomy/GetCategoryList?pageSize=' + pageSize + '&pageIndex=' + pageIndex).success(function (result) {
                $scope.entities = result;
            }).error(function () {
                console.log("请求发生错误");
            });
        }, 100);
    };

    getPagedDataAsync(10, 0);

    $scope.category = {
        Name: "",
        Slug: "",
        Parent: 0,
        Description: "",
        Taxonomy: "Category"
    };

    $scope.createCategory = function () {
        $http({
            method: "POST",
            url: "/Admin/Taxonomy/Create/",
            data: $scope.category
        })
        .success(function (data) {
            if (data) {
                getPagedDataAsync(10, 0);
                $scope.category = {
                    Name: "",
                    Slug: "",
                    Parent: 0,
                    Description: "",
                    Taxonomy: "Category"
                };
            }
        })
        .error(function () {

        });
    };

}]);