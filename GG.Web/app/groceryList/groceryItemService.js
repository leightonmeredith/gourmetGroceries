(function () {
    'use strict';

    angular
        .module('smartApp')
        .service('groceryItemService', groceryItemService);

    groceryItemService.$inject = ['$http'];

    function groceryItemService($http) {
        this.getAll = getAll;
        this.save = save;

        function getAll() {
            return $http({
                url: 'api/GroceryItems',
                method: 'GET'
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('Error loading grocery list');
            });
        }

        function save(groceryList) {
            if (!angular.isDefined(groceryList))
                return Promise.reject('grocery list does not exist');
            if (groceryList.Id === 0)
                return create(groceryList);
            if (!isNaN(groceryList.Id))
                return edit(groceryList.Id, groceryList);
            return Promise.reject('grocery list id invalid');
        }

        function create(groceryList) {
            return $http({
                url: 'api/GroceryItems',
                method: 'POST',
                data: groceryList
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('save failed');
                return groceryList;
            });
        }

        function edit(groceryListId, groceryList) {
            return $http({
                url: 'api/GroceryItems/' + groceryListId,
                method: 'PUT',
                data: groceryList
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('update failed');
                return groceryList;
            });
        }
    }
})();