(function () {
    'use strict';

    angular
        .module('smartApp')
        .controller('recipeDetailCtrl', RecipeDetailCtrl);

    RecipeDetailCtrl.$inject = ['ggCommon', '$scope', '$route', 'recipeService'];

    function RecipeDetailCtrl(ggCommon, $scope, $route, recipeService) {
        //jshint is needed in every funct that runs in strict mode
        /* jshint validthis:true */
        var vm = this;
        vm.title = "Recipe Detail";
        vm.recipe = { id: 0, Ingredients: [], Directions: [] };

        activate();
        vm.searchRecipeRedirect = ggCommon.searchRecipeRedirect;
        vm.editRecipeRedirect = ggCommon.editRecipeRedirect;
        vm.viewIngredientsRedirect = ggCommon.viewIngredientsRedirect;
        vm.addRecipeToCustomList = addRecipeToCustomList;

        function activate() {
            //maybe add common.js file that run activateController here...
            var recipeId = parseInt($route.current.params.recipeId);
            return recipeService.get(recipeId).then(function (data) {
                return vm.recipe = data;
            })
        }

        function addRecipeToCustomList(recipeId) {
            return recipeService.addRecipeToCustomList(recipeId).then(function () {
                return alert('recipe added to customIngredients');
            })
        }
    }
})();
