$(document).ready(function () {

    $(".datetimepicker8").datetimepicker();
    $(".datetimepicker9").datetimepicker();
    $(".datetimepicker8").on("dp.change", function (e) {
        $(".datetimepicker9").data("DateTimePicker").setMinDate(e.date);
    });
    $(".datetimepicker9").on("dp.change", function (e) {
        $(".datetimepicker8").data("DateTimePicker").setMaxDate(e.date);
    });
    
});

function displayResults(divSearchResults) {
    var tab = document.getElementById("divSearchResults");
    tab.style.display='';
}

function changeTab(changedToTab, changedToLink, div) {
    ClearAll();
    var tab = document.getElementById(changedToTab);
    var lnk = document.getElementById(changedToLink);
    var div = document.getElementById(div);

    tab.classList.add("active");
    div.style.display = "";
}

function ClearAll() {
    document.getElementById("dashboard").style.display = 'none';
    document.getElementById("addPresentation").style.display = 'none';
    document.getElementById("ratePresentation").style.display = 'none';

    document.getElementById("lstDashboard").classList.remove("active");
    document.getElementById("lstAddPresent").classList.remove("active");
    document.getElementById("lstRate").classList.remove("active");
}



