// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {

    var webjobListUrl = $("#WebjobListUrl").val();

    $("#WebJobName").change(function () {
        var webJobName = $(this).val();

        console.log(webJobName);

        if (webJobName !== "") {
            $.get(webjobListUrl, { WebJobName: webJobName }, function (result) {
                $("#WebJobList").html(result);
            });
        }
    });
});