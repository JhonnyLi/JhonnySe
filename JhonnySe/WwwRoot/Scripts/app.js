angular.module("jhonnyApp", [])
    .controller("JhonnySeController", ["$scope", "$http",function ($scope, $http) {
        $scope.model = {};
        $http.get("home/getmodel").then(function (response) {
            $scope.model = response.data;
            response.data.repositorys.forEach(r => {
                r.createdDate = new Date(r.createdDate).toLocaleDateString("sv-SE");
                r.updatedAt = new Date(r.updatedAt).toLocaleDateString("sv-SE");
            });
        });
    }]);