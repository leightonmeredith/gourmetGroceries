(function () {
    'use strict';

    angular
        .module('smartApp')
        .service('ingredientCategoryService', ingredientCategoryService);

    ingredientCategoryService.$inject = ['$http'];

    function ingredientCategoryService($http) {
        this.getAll = getAll;
        this.save = save;

        function getAll() {
            return $http({
                url: 'api/IngredientCategories/',
                method: 'GET'
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('Error loading ingredient list');
            });
        }

        function save(ingredientCategory) {
            if (!angular.isDefined(ingredientCategory))
                return Promise.reject('ingredient Category does not exist');
            if (ingredientCategory.Id === 0)
                return create(ingredientCategory);
            if (!isNaN(ingredientCategory.Id))
                return edit(ingredientCategory.Id, ingredientCategory);
            return Promise.reject('ingredient Category id invalid');
        }

        function create(ingredientCategory) {
            return $http({
                url: 'api/IngredientCategories',
                method: 'POST',
                data: ingredientCategory
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('save failed');
                return ingredientCategory;
            });
        }

        function edit(ingredientCategoryId, ingredientCategory) {
            return $http({
                url: 'api/IngredientCategories/' + ingredientCategoryId,
                method: 'PUT',
                data: ingredientCategory
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('update failed');
                return ingredientCategory;
            });
        }
    }
})();