(function () {
    'use strict';

    angular
        .module('smartApp')
        .service('directionService', directionService);

    directionService.$inject = ['$http'];

    function directionService($http) {
        this.save = save;
        this.remove = remove;

        function save(recipeId, directions) {
            if (!angular.isDefined(directions) || directions == [])
                return Promise.reject('directions does not exist');
            if (!angular.isDefined(recipeId) || recipeId === 0)
                return Promise.reject('recipe id invalid ');

            for (var i = 0; i < directions.length; i++) {
                directions[i].RecipeId = recipeId;
            }

            return $http({
                url: 'api/Directions/Save',
                method: 'POST',
                data: directions
            }).then(function (result) {
                return result.data;
            }, function (errorResponse) {
                alert('save failed');
                return directions;
            });
        }
        function remove(directionId) {
            return $http({
                url: 'api/Directions/' + directionId,
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