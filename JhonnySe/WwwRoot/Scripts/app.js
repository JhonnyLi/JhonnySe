angular.module('jhonnyApp', [])
    .controller('JhonnySeController', ['$scope', '$http',function ($scope, $http) {
        $scope.model = {};
        $http.get('home/getmodel').then(function (response) {
            $scope.model = response.data;
        });
    }]);