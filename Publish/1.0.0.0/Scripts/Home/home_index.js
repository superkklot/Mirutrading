$(function () {
    //var virtualDomain = "/Mirutrading";

    $(".btnBrief").click(function () {
        $.ajax({
            url: virtualDomain + "/Home/GetPageBrief?index=1",
            type: "Get",
            success: function (data) {
                var a = "";
                for (var key in data.Items) {
                    var item = data.Items[key];
                    var tmpHtml = $("#bodyTemplate").html();
                    a += _.template(tmpHtml)(item);
                }
                $(".content").html(a);
                reset(data);
            }
        })
    });

    function reset(data) {
        var nextPageElem = $(".btnNext");
        nextPageElem.attr("pageIndex", 1);
        var disabled = "disabled"
        if (data.PageIndex == data.PageCount) {
            if (!nextPageElem.parent().hasClass(disabled)) {
                nextPageElem.parent().addClass(disabled);
            }
        } else {
            if (nextPageElem.parent().hasClass(disabled)) {
                nextPageElem.parent().removeClass(disabled);
            }
        }
    }

    $(".btnBra").click(function () {
        $.ajax({
            url: virtualDomain + "/Home/GetPageBra?index=1",
            type: "Get",
            success: function (data) {
                var a = "";
                for (var key in data.Items) {
                    var item = data.Items[key];
                    var tmpHtml = $("#bodyTemplate").html();
                    a += _.template(tmpHtml)(item);
                }
                $(".content").html(a);
                reset(data);
            }
        })
    });

    $(".btnNext").click(function (event) {
        var disabled = "disabled"
        var target = $(event.target);
        var parent = target.parent();
        if (parent.hasClass(disabled))
            return;
        var pageIndex = parseInt(target.attr("pageIndex"));
        var newPageIndex = pageIndex + 1;
        $.ajax({
            url: virtualDomain + "/Home/GetPageBra?index=" + newPageIndex,
            type: "Get",
            success: function (data) {
                var a = "";
                for (var key in data.Items) {
                    var item = data.Items[key];
                    var tmpHtml = $("#bodyTemplate").html();
                    a += _.template(tmpHtml)(item);
                }
                var oldContent = $(".content").html();
                $(".content").html(oldContent + a);
                target.attr("pageIndex", newPageIndex);
                if (data.PageIndex == data.PageCount) {
                    parent.addClass(disabled);
                }
            }
        })
    });
})
