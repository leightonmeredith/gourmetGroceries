(function () {
    'use strict';

    angular
        .module('smartApp')
        .controller('dashboardViewCtrl', DashboardViewCtrl);

    DashboardViewCtrl.$inject = ['$scope', 'ggCommon', 'recipeService'];

    function DashboardViewCtrl($scope, ggCommon, recipeService) {

        var vm = this;
        vm.title = 'Bagit';
        vm.recipeName = '';

        activate();
        vm.editRecipeRedirect = ggCommon.editRecipeRedirect;
        vm.viewIngredientsRedirect = ggCommon.viewIngredientsRedirect;
        vm.search = search;

        function activate() { }

        function search(recipeName) {
            //TODO: Redirect to search recipe and display results
            //if (angular.isDefined(recipeName) && recipeName !== '' && recipeName !== null)
            //    ggCommon.searchRecipeRedirect(recipeName);
            //else
                return null;
        }
    }
})();
