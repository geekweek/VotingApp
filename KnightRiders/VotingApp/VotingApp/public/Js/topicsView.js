(function (angular) {
    var theModule = angular.module("topicsView", ["ui.bootstrap"]);
    
    theModule.controller("topicsViewController",
        ["$scope","$window","$http",
        function ($scope, $window, $http) {
        $scope.topic = [];
        $scope.downloadUrl = "";
        $scope.averageRating = 0;
        $scope.newTopicRating = {
            comments: "",
            rating :"3",
            author: "TestUser",
            IsExisting: false
        };
        var urlParts = $window.location.pathname.split("/");
        var topicName = urlParts[urlParts.length - 1];
        var topicUrl = "/api/topics/" + topicName;
        var topicRatingUrl = "/api/topicRating/" + topicName;
        var topicUpdateUrl = "/api/topics/update/" + topicName;
         
        $http.get(topicUrl)
        .then(function (result) {
            console.log(result.data);
            $scope.topic = result.data;
            $scope.averageRating = CalculateAverageRating(result.data);
            
        }, function (err) {
            console.log(err);
        });
        
        $http.get(topicRatingUrl)
        .then(function (result) {
            if (result.data) {
                $scope.newTopicRating = result.data;
            }
        }, function (err) {
            console.log(err);
        });

        $scope.save = function () {
            $http.post(topicUrl, $scope.newTopicRating)
            .then(function (result) {
                if (!$scope.newTopicRating.IsExisting) {
                    $scope.topic.votes.push(result.data);
                    $scope.newTopicRating.IsExisting = true;
                } else {
                    for (i = 0; i < $scope.topic.votes.length; i++) {
                        if ($scope.topic.votes[i].author === $scope.newTopicRating.author) {
                            $scope.topic.votes[i] = $scope.newTopicRating;
                        }
                    }
                }
                $('.collapse').collapse()
            }, function (err) {
                console.log(err);
            });
        };
        
        var CalculateAverageRating = function (topicData) {
            var avgRating = 0;
            if (topicData && topicData.votes) {
                for (var i = 0; i < topicData.votes.length; i++) {
                    avgRating = avgRating + (+topicData.votes[i].rating);
                }
                avgRating = avgRating / topicData.votes.length;
            }
            return avgRating.toFixed(2);
        };
        

        $scope.update = function () {
            $http.post(topicUpdateUrl, $scope.topic)
            .then(function (result) {
                console.log("update success");
            }, function (err) {
                console.log(err);
            });
        };

    }
  ]);

})(window.angular);