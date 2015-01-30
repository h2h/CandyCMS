'use strict';

app.controller('UserProfileCtrl', ['$scope', function ($scope) {
}]);

app.controller('UserListCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.entities = [];

    $scope.searchText = '';
    $scope.action = 0;
    $scope.selected = [];

    var updateSelected = function (action, id) {
        if (action == 'add' & $scope.selected.indexOf(id) == -1) $scope.selected.push(id);
        if (action == 'remove' && $scope.selected.indexOf(id) != -1) $scope.selected.splice($scope.selected.indexOf(id), 1);
    }

    var getPagedDataAsync = function (pageSize, pageIndex) {
        setTimeout(function () {
            var data;
            $http.get('/Admin/User/List?pageSize=' + pageSize + '&pageIndex=' + pageIndex).success(function (result) {
                $scope.entities = result;
            }).error(function () {
                console.log("请求发生错误");
            });
        }, 100);
    }

    getPagedDataAsync(10, 0);

    $scope.updateSelection = function ($event, id) {
        var checkbox = $event.target;
        var action = (checkbox.checked ? 'add' : 'remove');
        updateSelected(action, id);
    };

    $scope.selectAll = function ($event) {
        var checkbox = $event.target;
        var action = (checkbox.checked ? 'add' : 'remove');
        for (var i = 0; i < $scope.entities.length; i++) {
            var entity = $scope.entities[i];
            updateSelected(action, entity.id);
        }
    };

    $scope.getSelectedClass = function (id) {
        return $scope.isSelected(id) ? 'selected' : '';
    }

    $scope.isSelected = function (id) {
        return $scope.selected.indexOf(id) >= 0;
    }

    $scope.isSelectedAll = function () {
        return $scope.selected.length === $scope.entities.length;
    }

    $scope.Search = function () {
        console.log($scope.searchText);
        console.log($scope.action);
    }

    $scope.Excute = function () {
        console.log($scope.action);
        if ($scope.action == 1) {
            removeEntity();
        }
    }

    var removeEntity = function () {
        console.log('删除数据');
    }
}]);