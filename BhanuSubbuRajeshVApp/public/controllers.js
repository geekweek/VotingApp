var  teamApp = angular.module("Football",[]);
teamApp.controller("TeamController",function($scope,$http){
    $scope.output = "";
    $scope.add = function() {
		$http.post("team/add/" + $scope.team+"/" + $scope.captain).
		success(function(data, status, headers, config) {
            $scope.output = data["name"] + " with id: " + data["_id"] + " created successfully";
	    }).
	    error(function(data, status, headers, config) {
            $scope.output = "Error adding team " + data["name"];
      	}); 
    }
});