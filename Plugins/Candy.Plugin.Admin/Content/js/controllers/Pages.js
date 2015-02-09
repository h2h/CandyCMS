'use strict';

app.controller('PagesCtrl', ['$scope', '$http', 'uiLoad', 'JQ_CONFIG', function ($scope, $http, uiLoad, JQ_CONFIG) {

    var editor = undefined;

    uiLoad.load(JQ_CONFIG['simditor']).then(function () {
        editor = new Simditor({ textarea: $('#simditor') });
    });

}]);