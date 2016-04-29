$(function () {
    $(".search").keyup(function (event) {
        if (event.keyCode == 13) { // enter
            var text = $(event.target).val();
            if (text == "") { return; }
            alert(text);
        }
    })
})
