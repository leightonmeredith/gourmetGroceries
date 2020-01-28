(function () {
    'use strict';

    angular
        .module('smartApp')
        .controller('ingredientEditCtrl', IngredientEditCtrl);

    IngredientEditCtrl.$inject = ['$route', 'ggCommon', 'ingredientService', 'ingredientCategoryService'];

    function IngredientEditCtrl($route, ggCommon, ingredientService, ingredientCategoryService) {
        /* jshint validthis:true */
        var vm = this;

        vm.title = "Ingredient";
        vm.ingredientCategories = [];

        vm.ingredient = { Id: 0 };
        vm.ingredientCategory = { Id: 0 };

        activate();
        vm.saveIngredientToMaster = saveIngredientToMaster;
        vm.saveIngredientCategory = saveIngredientCategory;
        vm.searchRecipeRedirect = ggCommon.searchRecipeRedirect;
        vm.editRecipeRedirect = ggCommon.editRecipeRedirect;
        vm.editIngredientRedirect = ggCommon.editIngredientRedirect;

        function activate() {
            var ingredientId = parseInt($route.current.params.ingredientId);

            ingredientCategoryService.getAll().then(function (ingredientCategories) {
                vm.ingredientCategories = ingredientCategories;
            });

            if (ingredientId !== 0) {
                return ingredientService.get(ingredientId).then(function (data) {
                    vm.ingredient = data;
                });
            }
            return null;
        }

        function saveIngredientToMaster(ingredient) {
            return ingredientService.saveToMaster(ingredient).then(function (data) {
                if (ingredient.Id === 0)
                    ggCommon.editIngredientRedirect(data.Id);

            });
        }

        function saveIngredientCategory(ingredientItemCategory) {
            return ingredientCategoryService.save(ingredientItemCategory).then(function (data) {
                vm.ingredientCategory = {
                    Id: 0
                };
                return vm.ingredientCategories.push(data);
            });
        }
    }
})();
