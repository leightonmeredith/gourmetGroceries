(function () {
    'use strict';

    angular
        .module('smartApp')
        .factory('ggCommon', ggCommon);

    ggCommon.$inject = ['$http', '$location'];

    function ggCommon($http, $location) {
        var service = {
            dashboardRedirect: dashboardRedirect,
            editRecipeRedirect: editRecipeRedirect,
            searchRecipeRedirect: searchRecipeRedirect,
            viewRecipeRedirect: viewRecipeRedirect,
            viewIngredientsRedirect: viewIngredientsRedirect,
            editIngredientRedirect: editIngredientRedirect
        };

        return service;

        function dashboardRedirect() {
            var url = 'dashboard';
            $location.path(url);
        }

        function searchRecipeRedirect() {
            var url = 'recipeView';
            $location.path(url);
        }

        function viewRecipeRedirect(recipeId) {
            var url = 'recipeDetail/' + recipeId;
            $location.path(url);
        }

        function editRecipeRedirect(recipeId) {
            var url = 'recipeEdit/' + recipeId;
            $location.path(url);
        }

        function viewIngredientsRedirect() {
            var url = 'ingredientView';
            $location.path(url);
        }

        function editIngredientRedirect(ingredientId) {
            var url = 'ingredientEdit/' + ingredientId;
            $location.path(url);
        }
    }
})();