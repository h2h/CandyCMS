var app = angular.module('app').config(
    [
        '$controllerProvider', '$compileProvider', '$provide',
        function ($controllerProvider, $compileProvider, $provide) {
            app.controller = $controllerProvider.register;
            app.directive = $compileProvider.directive;
            app.service = $provide.service;
            app.value = $provide.value;
        }
    ]
);