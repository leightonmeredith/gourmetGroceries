(function () {
    'use strict';

    angular
        .module('smartApp')
        .controller('ingredientListViewCtrl', ingredientListViewCtrl);

    ingredientListViewCtrl.$inject = ['ggCommon', 'ingredientService', 'ingredientCategoryService'];

    function ingredientListViewCtrl(ggCommon, ingredientService, ingredientCategoryService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'Ingredients';
        vm.subDisplay = 'selected';
        vm.masterIngredientSearch = '';
        vm.ingredientsMaster = [];
        vm.customIngredients = [];
        vm.recipeIngredients = [];
        vm.ingredientCategories = [];

        vm.ingredientItem = { Id: 0 };
        vm.ingredientCategory = { Id: 0 };

        activate();
        vm.saveIngredientToMaster = saveIngredientToMaster;
        vm.saveIngredientCategory = saveIngredientCategory;
        vm.addIngredientItemsToList = addIngredientItemsToList;
        vm.addIngredientToList = addIngredientToList;
        vm.addCustomIngredientToList = addCustomIngredientToList;
        vm.editIngredientRedirect = ggCommon.editIngredientRedirect;
        vm.removeIngredientFromList = removeIngredientFromList;
        vm.removeCustomIngredientFromList = removeCustomIngredientFromList;
        vm.removeIngredientItemFromList = removeIngredientItemFromList;
        vm.removeRecipeFromList = removeRecipeFromList;
        vm.searchRecipeRedirect = ggCommon.searchRecipeRedirect;
        vm.editRecipeRedirect = ggCommon.editRecipeRedirect;
        vm.setSubDisplay = setSubDisplay;
        vm.addRecipeToList = addRecipeToList;

        function activate() {
            ingredientCategoryService.getAll().then(function (ingredientCategories) {
                vm.ingredientCategories = ingredientCategories;
            });
            return ingredientService.getAllCustomIngredients().then(function (customIngredients) {
                vm.customIngredients = customIngredients;

                vm.customIngredients = customIngredients.filter(function (customIngredient) {
                    return customIngredient.RecipeId === null;
                });

                vm.recipeIngredients = customIngredients.filter(function (customIngredient) {
                    return customIngredient.IngredientId === null;
                });

                return ingredientService.getAll().then(function (ingredientsMaster) {

                    for (var i = 0; i < ingredientsMaster.length; i++) {
                        for (var c = 0; c < vm.customIngredients.length; c++) {
                            if (vm.customIngredients[c].IngredientId === ingredientsMaster[i].Id) {
                                ingredientsMaster[i].IsBaggit = true;

                                var index = customIngredients.indexOf(ingredientsMaster[i]);
                                customIngredients.splice(index, 1);

                                break;
                            }
                        }
                        var t = [];
                        for (var r = 0; r < vm.recipeIngredients.length; r++) {
                            for (var ri = 0; ri < vm.recipeIngredients[r].Recipe.Ingredients.length; ri++) {
                                if (vm.recipeIngredients[r].Recipe.Ingredients[ri].Ingredient.Id === ingredientsMaster[i].Id) {
                                    ingredientsMaster[i].IsRecipeBaggit = true;
                                    t.push({ingName: vm.recipeIngredients[r].Recipe.Ingredients[ri].Ingredient.Name})
                                    var index = customIngredients.indexOf(ingredientsMaster[i]);
                                    customIngredients.splice(index, 1);

                                    break;
                                }
                            }
                        }
                    }

                    return vm.ingredientsMaster = ingredientsMaster;
                })
            });
        }

        function saveIngredientToMaster(ingredientItem) {
            return ingredientService.saveToMaster(ingredientItem).then(function (data) {
                vm.ingredientItem = {
                    Id: 0
                };
                return vm.ingredientsMaster.push(data);
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

        function addIngredientToList(ingredient) {
            return ingredientService.addIngredientToList(ingredient).then(function (data) {
                ingredient.IsBaggit = true;
                return vm.customIngredients.push(data);
            });
        }

        function addCustomIngredientToList(ingredient) {
            var index = vm.customIngredients.indexOf(ingredient);
            vm.customIngredients.splice(index, 1);

            return addIngredientToList(ingredient.Ingredient);
        }

        function addIngredientItemsToList(selectedMasterIngredientItems) {
            if (!angular.isDefined(selectedMasterIngredientItems) || selectedMasterIngredientItems.length <= 0) {
                return alert("select a master ingredient");
            }

            return ingredientService.addCustomIngredientItemsToList(selectedMasterIngredientItems, vm.customIngredients).then(function (data) {
                return vm.customIngredients = data;
                //return vm.customIngredients.push(selectedMasterIngredientItems);
            });
        }

        function removeIngredientFromList(ingredient) {

            var removedIngredient = vm.customIngredients.filter(function (customIngredient) {
                return customIngredient.IngredientId === ingredient.Id;
            });

            return ingredientService.removeIngredientFromList(removedIngredient[0]).then(function (data) {
                ingredient.IsBaggit = false;

                var index = vm.customIngredients.indexOf(data);
                return vm.customIngredients.splice(index, 1);
            });
        }

        function removeCustomIngredientFromList(ingredient) {
            return ingredientService.removeIngredientFromList(ingredient).then(function (data) {
                var selectedIngredient = $.grep(vm.ingredientsMaster, function (e) {
                    return e.Id === data.IngredientId;
                });

                if (selectedIngredient.length === 0) {
                    //redirect To 404 page
                }
                if (selectedIngredient.length > 0) {
                    selectedIngredient[0].IsBaggit = false;
                }

                var index = vm.customIngredients.indexOf(data);
                return vm.customIngredients[index].Ingredient.IsRemoved = true;
            });
        }

        function removeIngredientItemFromList(selectedCustomIngredientItems) {
            if (!angular.isDefined(selectedCustomIngredientItems) || selectedCustomIngredientItems.length <= 0) {
                return alert("select a custom ingredient");
            }

            return ingredientService.removeIngredientItemFromList(selectedCustomIngredientItems, vm.customIngredients).then(function (data) {
                return vm.customIngredients = data;
            });
        }

        function removeRecipeFromList(recipe) {
            return ingredientService.removeIngredientFromList(recipe).then(function (data) {
                var index = vm.recipeIngredients.indexOf(data);
                return vm.recipeIngredients[index].Recipe.IsRemoved = true;
            });
        }

        function addRecipeToList(recipe) {
            return ingredientService.addRecipeToList(recipe).then(function (data) {

                //data.Recipe.IsRemoved = false;

                var index = vm.recipeIngredients.indexOf(recipe);
                vm.recipeIngredients[index].Id = data.Id;
                return vm.recipeIngredients[index].Recipe.IsRemoved = false;
            })
        }

        function removeRecipesFromList(selectedRecipes) {
            if (!angular.isDefined(selectedRecipes) || selectedRecipes.length <= 0) {
                return alert("select a recipe");
            }

            return ingredientService.removeRecipesFromList(selectedRecipes, vm.recipeIngredients).then(function (data) {
                return vm.recipeIngredients = data;
            });

        }

        function setSubDisplay(val) {
            return vm.subDisplay = val;
        }
    }
})();
