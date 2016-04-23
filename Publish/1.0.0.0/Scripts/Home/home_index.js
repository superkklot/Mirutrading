$(function () {
    //var virtualDomain = "/Mirutrading";

    $(".btnBrief").click(function () {
        $.ajax({
            url: virtualDomain + "/Home/GetFirstPageBrief",
            type: "Get",
            success: function (data) {
                var a = "";
                for (var key in data.Items) {
                    var item = data.Items[key];
                    var tmpHtml = $("#bodyTemplate").html();
                    a += _.template(tmpHtml)(item);
                }
                $(".content").html(a);
            }
        })
    });

    $(".btnBra").click(function () {
        $.ajax({
            url: virtualDomain + "/Home/GetFirstPageBra",
            type: "Get",
            success: function (data) {
                var a = "";
                for (var key in data.Items) {
                    var item = data.Items[key];
                    var tmpHtml = $("#bodyTemplate").html();
                    a += _.template(tmpHtml)(item);
                }
                $(".content").html(a);
            }
        })
    });
})
