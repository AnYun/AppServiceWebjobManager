// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {

    var webjobListUrl = $("#WebjobListUrl").val();

    $("#WebJobSettingName").change(function () {
        var webJobSettingName = $(this).val();

        if (webJobSettingName !== "") {
            $.get(webjobListUrl, { WebJobSettingName: webJobSettingName }, function (result) {
                $("#WebJobList").html(result);
            });
        }
    });
});