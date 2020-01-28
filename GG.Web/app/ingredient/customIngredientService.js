(function () {
    'use strict';

    angular
        .module('smartApp')
        .service('customIngredientService', customIngredientService);

    customIngredientService.$inject = ['$http'];

    function customIngredientService($http) {
        this.getAll = getAll;
        this.addCustomIngredientItemsToList = addCustomIngredientItemsToList;
        this.addRecipeToList = addRecipeToList;
        this.addIngredientToList = addIngredientToList;
        this.removeIngredientFromList = removeIngredientFromList;
        this.removeIngredientItemFromList = removeIngredientItemFromList;
        this.addRecipeToCustomList = addRecipeToCustomList;
        this.removeRecipesFromList = removeRecipesFromList;

        function getAll() {
            return $http({
                url: 'api/CustomIngredients',
                method: 'GET'
            }).then(function (result) {
                //if (result.data.length === 0)
                //return { Recipes: [], Ingredients: [] };
                return result.data;
            }, function (errorResponse) {
                alert('Error loading Ingredients master');
            });
        }

        function addCustomIngredientItemsToList(ingredients, customIngredients) {

            var tempCustomIngredients = []; //{ Recipes: [], Ingredients: [] };
            for (var i = 0; i < ingredients.length; i++) {
                var ingredient = {
                    //
                };
                ingredient.IngredientId = ingredients[i].Id;
                ingredient.Ingredient = ingredients[i];
                tempCustomIngredients.push(ingredient);
            }

            return $http({
                url: 'api/CustomIngredients/Save',
                method: 'POST',
                data: tempCustomIngredients
            }).then(function (result) {
                customIngredients = customIngredients.concat(result.data);
                return customIngredients;
            }, function (errorResponse) {
                alert('save failed');
                return customIngredients;
            });
        }

        function addIngredientToList(ingredient) {

            var custom = {};
            custom.IngredientId = ingredient.Id;
            custom.Ingredient = ingredient;

            return $http({
                url: 'api/CustomIngredients',
                method: 'POST',
                data: custom
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('save failed');
                return null;
            });
        }

        function addRecipeToCustomList(recipeId) {
            var tempCustomIngredients = [{ RecipeId: recipeId }];
            return $http({
                url: 'api/CustomIngredients/Save',
                method: 'POST',
                data: tempCustomIngredients
            }).then(function (result) {
                //customIngredients = customIngredients.concat(result.data);
                return null;
            }, function (errorResponse) {
                alert('save failed');
                return null;
            });
        }

        function removeIngredientItemFromList(ingredients, customIngredients) {
            return $http({
                url: 'api/CustomIngredients/Delete',
                method: 'POST',
                data: ingredients
            }).then(function (result) {
                for (var i = 0; i < ingredients.length; i++) {
                    var index = customIngredients.indexOf(ingredients[i]);
                    customIngredients.splice(index, 1);
                }

                return customIngredients;
            }, function (errorResponse) {
                alert('save failed');
                return customIngredients;
            });
        }

        function removeIngredientFromList(customIngredient) {
            return $http({
                url: 'api/CustomIngredients/' + customIngredient.Id,
                method: 'DELETE'
            }).then(function (result) {
                return customIngredient;
            }, function (errorResponse) {
                alert('save failed');
                return null;
            });
        }

        function addRecipeToList(recipe) {
            return $http({
                url: 'api/CustomIngredients',
                method: 'POST',
                data: recipe
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('save failed');
                return null;
            });
        }

        function removeRecipesFromList(selectedRecipes, recipeIngredients) {
            return $http({
                url: 'api/CustomIngredients/DeleteRecipe',
                method: 'POST',
                data: selectedRecipes
            }).then(function (result) {
                for (var i = 0; i < selectedRecipes.length; i++) {
                    var index = recipeIngredients.indexOf(selectedRecipes[i]);
                    recipeIngredients.splice(index, 1);
                }

                return recipeIngredients;
            }, function (errorResponse) {
                alert('save failed');
                return recipeIngredients;
            });
        }
    }
})();