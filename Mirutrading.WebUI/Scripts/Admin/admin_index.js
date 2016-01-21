$(function () {
	//$("#btnAdd").click(function () {
	//	$("#myModal").modal("show");
	//})

	$("#add_modify_btn").click(function () {
		var type = $("#add_modify_type").val();
		var name = $("add_modify_name").val();
		var price = $("add_modify_price").val();
		var link = $("add_modify_link").val();
		$.ajax({
			url: "~/Admin/AddProduct",
			type: "POST",
			data: 'Type=' + type + "&Name=" + name + "&Price=" + price + "&link=" + link,
			success: function (data) {
				if (data.ErrorCode != 0) {
					alert(data.ErrorMessage);
					return false;
				}
				$("#myModal").modal("hide");
			}
		});
	})
})