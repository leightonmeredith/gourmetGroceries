var smartApp = angular.module('smartApp', ['ngRoute']);//replace ngRoute with ui.router

smartApp.config(function ($routeProvider, $locationProvider) {//rep routeProvider with stateProvider
    //$locationProvider.html5Mode(false);
    //$locationProvider.hashPrefix('!');
    $routeProvider
        .when('/', {
            redirectTo: '/dashboard'
        })
        .when('/dashboard', {//rep when with state
            controller: 'dashboardViewCtrl',
            templateUrl: 'app/dashboard/view/dashboardView.html'
        })
        .when('/recipeView', {
            controller: 'recipeViewCtrl',
            templateUrl: 'app/recipe/view/recipeView.html'
        })
        .when('/recipeView/:recipeName', {
            controller: 'recipeViewCtrl',
            templateUrl: 'app/recipe/view/recipeView.html',
            params: { recipeName: 'recipeName' }
        })
        .when('/recipeDetail/:recipeId', {
            controller: 'recipeDetailCtrl',
            templateUrl: 'app/recipe/detail/recipeDetail.html',
            params: { recipeId: 'recipeId' }
        })
        .when('/recipeEdit/:recipeId', {
            controller: 'recipeEditCtrl',
            templateUrl: 'app/recipe/edit/recipeEdit.html',
            params: { recipeId: 'recipeId' }
        })
        .when('/ingredientView', {
            controller: 'ingredientListViewCtrl',
            templateUrl: 'app/ingredient/view/ingredientListView.html',
        })
        .when('/ingredientEdit/:ingredientId', {
            controller: 'ingredientEditCtrl',
            templateUrl: 'app/ingredient/edit/ingredientEdit.html',
            params: { ingredientId: 'ingredientId' }
        })
        .when('/contact', {
            templateUrl: 'app/contact/contact.html',
        })

        .otherwise({ redirectTo: '/' });//look into whether otherwise is needed for ui-router
});