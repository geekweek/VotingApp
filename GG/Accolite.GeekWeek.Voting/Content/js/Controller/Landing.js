/// <reference path="../jquery-1.9.1-vsdoc.js" />

var Landing = function ($scope) {
    $scope.Model = JSON.parse($('#Viewmodel').val());

    setTimeout(function () { OnLoadMethod($scope); }, 1000);

    $scope.displayViewPresentationResults = function () {

        if ($('#PresentationStartDateValue').val()) {
            $scope.Model.ViewPresentationsDetails.StartDate = $('#PresentationStartDateValue').val();
        }
        if ($('#PresentationEndDateValue').val()) {
            $scope.Model.ViewPresentationsDetails.EndDate = $('#PresentationEndDateValue').val();
        }

        var params = { "VotingModel": angular.toJson($scope.Model) };

        $.ajax({
            type: 'POST',
            url: 'GetPresentations',
            data: JSON.stringify(params),
            datatype: 'json',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $scope.$apply(function () {
                    $scope.Model = data;
                    $("#divSearchResults").removeClass("hide");
                    $("#divSearchResults").removeClass("show");
                });
            },
            error: function (data) {
            }
        });
    }

    $scope.SetPresentationID = function (value) {
        $scope.Model.SelectedPresentation._id = value._id;
        $scope.Model.SelectedPresentation.Title = value.Title;
        $scope.Model.SelectedPresentation.Presenter = value.Presenter;
        $scope.Model.SelectedPresentation.AverageRating = value.AverageRating;
        $scope.Model.SelectedPresentation.IsEditable = value.IsEditable;
        $scope.Model.SelectedPresentation.UserName = value.UserName;
        $scope.Model.SelectedPresentation.PresentationDate = value.PresentationDate;
    }

    $scope.UpdateRatingDetails = function () {
        $scope.Model.RatingData.UserName = $scope.Model.UserData.UserID;
        $scope.Model.RatingData.ForPresentationID = $scope.Model.SelectedPresentation._id;
    }

    $scope.SubmitCurrentRating = function () {
        $scope.Model.RatingData.Ratingpoint = $("#ratePresent").val();

        var params = { "VotingModel": angular.toJson($scope.Model) };

        $.ajax({
            type: 'POST',
            url: 'AddNewUserRating',
            data: JSON.stringify(params),
            datatype: 'json',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $scope.$apply(function () {
                    $scope.Model = data;
                });
            },
            error: function (data) {
            }
        });
    }

    $scope.SubmitPresentation = function () {
        if ($('#AddPresentationDateValue').val()) {
            $scope.Model.NewPresentation.PresentationDate = $('#AddPresentationDateValue').val();
        }

        var params = { "VotingModel": angular.toJson($scope.Model) };

        $.ajax({
            type: 'POST',
            url: 'AddNewPresentations',
            data: JSON.stringify(params),
            datatype: 'json',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $scope.$apply(function () {
                    $scope.Model = data;
                });
            },
            error: function (data) {
            }
        });
    }

}
var OnLoadMethod = function ($scope) {
    var check = $scope;
}