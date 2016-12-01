;
(function ($) {
    var pageService = angular.module('PageModule', []);
    pageService.factory('pageFactory', ['$http',
    function ($http) {

        var factory = {};

        factory.pageList = function (url) {
            var postData = {
                pageIndex: 1,
                pageSize: 1
            }

            //将参数传递的方式改成form
            var postCfg = {
                headers: { 'Content-Type': "application/x-www-form-urlencoded" },
                transformRequest: function (data) {
                    return $.param(data);
                }
            };
            $http.post(url, postData, postCfg)
                     .success(function (response) {
                         $scope.Rows = response.Rows;
                         var options = {
                             bootstrapMajorVersion: 2, //版本
                             currentPage: postData.pageIndex, //当前页数
                             totalPages: response.PageEntity.Total, //总页数
                             itemTexts: function (type, page) {
                                 switch (type) {
                                     case "first":
                                         return "首页";
                                     case "prev":
                                         return "上一页";
                                     case "next":
                                         return "下一页";
                                     case "last":
                                         return "末页";
                                     case "page":
                                         return page;
                                 }
                                 return "首页";
                             },//点击事件，用于通过Ajax来刷新整个list列表
                             onPageClicked: function (event, originalEvent, type, page) {
                                 postData.pageIndex = page;
                                 $http.post(url, postData, postCfg)
                                     .success(function (response) {
                                         $scope.Rows = response.Rows;
                                     });
                             }
                         };
                         $("#page").bootstrapPaginator(options);
                     });
        }
        return factory;
    }]);
})(jQuery);


