var smartApp = angular.module('smartApp');

smartApp.controller('recipeViewCtrl', RecipeViewCtrl);

RecipeViewCtrl.$inject = ['$scope', '$route', 'ggCommon', 'recipeService'];

function RecipeViewCtrl($scope, $route, ggCommon, recipeService) {

    var vm = this;
    vm.currentPage = 1;
    vm.pageSize = 3;

    vm.title = "Recipes";

    vm.viewRecipeRedirect = ggCommon.viewRecipeRedirect;
    vm.editRecipeRedirect = ggCommon.editRecipeRedirect;
    vm.viewIngredientsRedirect = ggCommon.viewIngredientsRedirect;
    vm.searchRecipes = searchRecipes;
    vm.getRecipeList = getRecipeList;

    activate();

    vm.recipes = [];

    function activate() {
        var recipeName = $route.current.params.recipeName;
        return getRecipeList();
    }

    function getRecipeList() {
        //if (angular.isDefined(recipeName) && recipeName != '') {
        //    return recipeService.search(recipeName).then(function (data) {
        //        return vm.recipes = data;
        //    });
        //}
        //else {
        return recipeService.getAll().then(function (data) {
            return vm.recipes = data;
        });
        //}
    }

    function searchRecipes(recipeName) {
        return recipeService.search(recipeName).then(function (data) {
            return vm.recipes = data;
        });
    }
}