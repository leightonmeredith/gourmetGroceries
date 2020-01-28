(function () {
    'use strict';

    angular
        .module('smartApp')
        .service('recipeService', recipeService);

    recipeService.$inject = ['$http', 'customIngredientService'];

    function recipeService($http, customIngredientService) {
        this.get = get;
        this.getAll = getAll;
        this.save = save;
        this.search = search;
        this.addRecipeToCustomList = customIngredientService.addRecipeToCustomList;

        function get(recipeId) {
            return $http({
                //url: 'api/Recipes/' + recipeId,
                url: 'api/Recipes/',
                method: 'GET',
                data: recipeId
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('Error loading recipe');
            });
        }

        function getAll() {
            return $http({
                url: 'api/Recipes',
                method: 'GET'
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('Error loading recipes');
            });
        }

        function search(recipeName) {
            var d = [recipeName];
            return $http({
                url: 'api/Recipes/Search',
                method: 'POST',
                data: d
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                return alert('Error loading recipes');
            });
        }

        function save(recipe) {
            if (!angular.isDefined(recipe))
                return Promise.reject('recipe does not exist');
            if (recipe.Id === 0)
                return create(recipe);
            if (!isNaN(recipe.Id))
                return edit(recipe.Id, recipe);
            return Promise.reject('recipe id invalid');
        }

        function create(recipe) {
            //may get reset by referece here..
            var ingerdients = recipe.Ingredients;
            var directions = recipe.Directions;
            recipe.Ingredients = [];
            recipe.Directions = [];

            return $http({
                url: 'api/Recipes',
                method: 'POST',
                data: recipe
            }).then(function (result) {
                result.data.Ingredients = ingerdients;
                result.data.Directions = directions;
                return result.data;
            }, function (errorResponse) {
                alert('save failed');
                return recipe;
            });
        }

        function edit(recipeId, recipe) {
            return $http({
                url: 'api/Recipes/' + recipeId,
                method: 'PUT',
                data: recipe
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('update failed');
                return recipe;
            });
        }
    }
})();