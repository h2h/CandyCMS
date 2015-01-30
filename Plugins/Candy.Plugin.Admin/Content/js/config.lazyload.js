angular.module('app').constant('JQ_CONFIG', {
    chosen: ['Plugins/Candy.Plugin.Admin/Content/plugins/jquery/chosen/chosen.jquery.min.js',
			 'Plugins/Candy.Plugin.Admin/Content/plugins/jquery/chosen/chosen.css']
}).config(['$ocLazyLoadProvider', function ($ocLazyLoadProvider) {
    $ocLazyLoadProvider.config({
        debug: false,
        events: true,
        modules: [
			{
			    name: 'ui.select',
			    files: [
					'vendor/modules/angular-ui-select/select.min.js',
					'vendor/modules/angular-ui-select/select.min.css'
			    ]
			}
        ]
    });
}]);