(function () {
    'use strict';

    angular
        .module('smartApp')
        .service('ingredientService', ingredientService);

    ingredientService.$inject = ['$http', 'customIngredientService'];

    function ingredientService($http, customIngredientService) {
        this.getAll = getAll;
        this.get = get;
        this.saveToMaster = saveToMaster;

        //customIngredientService functions
        this.addCustomIngredientItemsToList = customIngredientService.addCustomIngredientItemsToList;
        this.addRecipeToList = customIngredientService.addRecipeToList;
        this.addIngredientToList = customIngredientService.addIngredientToList;
        this.removeIngredientFromList = customIngredientService.removeIngredientFromList;
        this.removeIngredientItemFromList = customIngredientService.removeIngredientItemFromList;
        this.getAllCustomIngredients = customIngredientService.getAll;
        this.removeRecipesFromList = customIngredientService.removeRecipesFromList;
        this.removeRecipeFromList = customIngredientService.removeRecipeFromList;

        //need to add recipeService function seperate!!!!!
        this.saveToRecipe = saveToRecipe;
        this.removeFromRecipe = removeFromRecipe;
        //this.getByRecipeId = getByRecipeId;


        function getAll() {
            return $http({
                url: 'api/Ingredients',
                method: 'GET'
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('Error loading Ingredients master');
            });
        }

        function get(ingredientId) {
            return $http({
                url: 'api/Ingredients/' + ingredientId,
                method: 'GET'
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('Error loading ingredient');
            });
        }

        function saveToMaster(ingredient) {
            if (!angular.isDefined(ingredient))
                return alert('ingredient does not exist');
            if (ingredient.Id === 0) {
                return createToMaster(ingredient);
            }
            if (ingredient.Id > 0) {
                return editToMaster(ingredient);
            }
            return alert('ingredient invalid');
        }

        function createToMaster(ingredient) {
            return $http({
                url: 'api/Ingredients',
                method: 'POST',
                data: ingredient
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('save failed');
                return ingredient;
            });
        }

        function editToMaster(ingredient) {
            return $http({
                url: 'api/Ingredients/' + ingredient.Id,
                method: 'PUT',
                data: ingredient
            }).then(function (result) {
                return ingredient;
            }, function (errorResponse) {
                alert('save failed');
                return ingredient;
            });
        }

        function saveToRecipe(recipeId, ingredients) {
            if (!angular.isDefined(ingredients) || ingredients == [])
                return Promise.reject('ingredients does not exist');
            if (!angular.isDefined(recipeId) || recipeId === 0)
                return Promise.reject('recipe id invalid ');

            return $http({
                url: 'api/Recipe/' + recipeId + '/Ingredients',
                method: 'POST',
                data: ingredients
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('save failed');
                return ingredients;
            });
        }

        function removeFromRecipe(ingredientId) {
            return $http({
                url: 'api/RecipeIngredients/' + ingredientId,
                method: 'DELETE'
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('save failed');
                return directions;
            });
        }
    }
})();