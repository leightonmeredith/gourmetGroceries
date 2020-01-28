(function () {
    'use strict';

    angular.module('smartApp').controller('recipeEditCtrl', RecipeEditCtrl);

    RecipeEditCtrl.$inject = ['ggCommon', '$scope', '$route', 'recipeService', 'ingredientService', 'directionService'];

    function RecipeEditCtrl(ggCommon, $scope, $route, recipeService, ingredientService, directionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.recipe = { Id: 0, Ingredients: [], Directions: [] };
        vm.ingredients = [];
        vm.directions = [];
        vm.ingredientMaster = [];
        vm.title = "Recipe";

        vm.searchRecipeRedirect = ggCommon.searchRecipeRedirect;
        vm.viewIngredientsRedirect = ggCommon.viewIngredientsRedirect;
        vm.saveRecipe = saveRecipe;
        vm.addIngredient = addIngredient;
        vm.editIngredient = editIngredient;
        vm.removeIngredient = removeIngredient;
        vm.saveIngredients = saveIngredients;
        vm.addDirection = addDirection;
        vm.editDirection = editDirection;
        vm.removeDirection = removeDirection;
        vm.saveDirections = saveDirections;

        vm.recipes = [];

        activate();

        function activate() {
            var recipeId = parseInt($route.current.params.recipeId);

            ingredientService.getAll().then(function (data) {
                vm.ingredientMaster = data;
            });

            if (recipeId !== 0) {
                return recipeService.get(recipeId).then(function (data) {
                    vm.recipe = data;
                    vm.directions = vm.recipe.Directions;
                    vm.ingredients = vm.recipe.Ingredients;
                    vm.recipe.Directions = [];
                    return vm.recipe.Ingredients = [];
                });
            }
            return null;
        }

        function saveRecipe() {
            var isNewRecipe = true;
            if (vm.recipe.Id != 0)
                isNewRecipe = false;
            return recipeService.save(vm.recipe).then(function (data) {
                if (isNewRecipe)
                    return ggCommon.editRecipeRedirect(data.Id);
                return vm.recipe = data;
            });
        }

        //Create Dependency and put these in respective Ctrl

        function addIngredient(ingredient) {
            if (!angular.isDefined(ingredient.Id))
                ingredient.Id = 0;

            for (var i = 0; i < vm.recipe.Ingredients.length; i++) {
                if (vm.recipe.Ingredients[i].Description == ingredient.Description && vm.recipe.Ingredients[i].Ingredient.Name == ingredient.Ingredient.Name && vm.recipe.ingredients[i].Ingredient.Type == ingredient.Ingredient.Type) {
                    return alert('This is already in your list')
                }
            }
            ingredient.IngredientId = ingredient.Ingredient.Id;
            vm.ingredients.push(ingredient);
            vm.ingredient = {};
            //TODO: change to save just one ingredient at a time
            saveIngredients(vm.recipe.Id, vm.ingredients);
        }

        function editIngredient(ingredient) {
            vm.ingredient = ingredient;
            removeIngredient(ingredient);
        }

        function removeIngredient(ingredient) {
            var index = vm.ingredients.indexOf(ingredient);
            vm.ingredients.splice(index, 1);
            return ingredientService.removeFromRecipe(ingredient.Id);
        }

        function saveIngredients(recipeId, ingredients) {
            return ingredientService.saveToRecipe(recipeId, ingredients).then(function (data) {
                return vm.ingredients = data;
            });
        }

        function addDirection(direction) {
            if (angular.isDefined(direction.Id))
                direction.Id = 0;
            vm.directions.push(direction);
            vm.direction = {};
            //TODO: change to save just one direction
            saveDirections(vm.recipe.Id, vm.directions);
        }

        function editDirection(direction) {
            vm.direction = direction;
            removeDirection(direction);
        }

        function removeDirection(direction) {
            var index = vm.directions.indexOf(direction);
            vm.directions.splice(index, 1);
            return directionService.remove(direction.Id);
        }

        function saveDirections(recipeId, directions) {
            return directionService.save(recipeId, directions).then(function (data) {
                return vm.directions = data;
            })
        }
    }

})();