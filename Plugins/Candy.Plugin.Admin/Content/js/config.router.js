'use strict';

angular.module('app').run(
	[
		'$rootScope', '$state', '$stateParams',
		function ($rootScope, $state, $stateParams) {
		    $rootScope.$state = $state;
		    $rootScope.$stateParams = $stateParams;
		}
	]
).config(
	[
		'$stateProvider', '$urlRouterProvider',
		function ($stateProvider, $urlRouterProvider) {
		    $urlRouterProvider.otherwise('/admin/dashboard');

		    $stateProvider.state('admin', {
		        abstract: true,
		        url: '/admin',
		        templateUrl: '/admin/home/app/'
		    })
			.state('admin.dashboard', {
			    url: '/dashboard',
			    templateUrl: '/admin/home/index/'
			})
			.state('admin.settings', {
			    url: '/settings',
			    template: '<div ui-view class="fade-in-up"></div>'
			})
			.state('admin.settings.basesettings', {
			    url: '/basesettings',
			    templateUrl: '/Admin/Settings/'
			})
            .state('admin.page', {
                url: '/page',
                template: '<div ui-view class="fade-in-up"></div>'
            })
		    .state('admin.page.profile', {
		        url: '/profile',
		        templateUrl: '/Admin/User/Profile/'
		    })
            .state('admin.users', {
                url: '/users',
                template: '<div ui-view class="fade-in-up"></div>'
            })
		    .state('admin.users.list', {
		        url: '/list',
		        templateUrl: '/Admin/User/'
		    });
		}
	]
);