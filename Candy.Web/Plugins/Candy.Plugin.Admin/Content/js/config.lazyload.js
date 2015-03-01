angular.module('app').constant('JQ_CONFIG', {
    chosen: [
        'Plugins/Candy.Plugin.Admin/Content/plugins/jquery/chosen/chosen.jquery.min.js',
        'Plugins/Candy.Plugin.Admin/Content/plugins/jquery/chosen/chosen.css'
    ],
    simditor: [
        'Plugins/Candy.Plugin.Admin/Content/plugins/jquery/simditor/module.min.js',
        'Plugins/Candy.Plugin.Admin/Content/plugins/jquery/simditor/uploader.min.js',
        'Plugins/Candy.Plugin.Admin/Content/plugins/jquery/simditor/hotkeys.min.js',
        'Plugins/Candy.Plugin.Admin/Content/plugins/jquery/simditor/simditor.min.js',
        'Plugins/Candy.Plugin.Admin/Content/plugins/jquery/simditor/simditor.css',
    ],
    dataTable: [
        'Plugins/Candy.Plugin.Admin/Content/plugins/jquery/datatables/jquery.dataTables.min.js',
        'Plugins/Candy.Plugin.Admin/Content/plugins/jquery/datatables/dataTables.bootstrap.js',
        'Plugins/Candy.Plugin.Admin/Content/plugins/jquery/datatables/dataTables.bootstrap.css'
    ]

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