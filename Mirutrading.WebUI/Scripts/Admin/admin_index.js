$(function () {
	
	var virtualDomain = "/Mirutrading";

	// 添加产品
	$(".add_product").click(function () {
		$("#myModal").attr("purpose", "add");
		$("#myModalLabel").html("产品添加");
		$("#add_modify_type").val("1");
		$("#add_modify_name").val("");
		$("#add_modify_price").val("");
		$("#add_modify_link").val("");
		$("#myModal").modal("show");
	})

	// 修改产品
	$(".modify_product").click(function () {
		$("#myModal").attr("purpose", "modify");
		$("#myModalLabel").html("产品修改");
		var tr = event.target.parentNode.parentNode;
		$("#add_modify_id").val($(tr).attr("id"));
		$("#add_modify_type").val($(tr).children(".item_type").html());
		$("#add_modify_name").val($(tr).children(".item_name").html());
		$("#add_modify_price").val($(tr).children(".item_price").html());
		$("#add_modify_link").val($(tr).children(".item_link").html());
		$("#myModal").modal("show");
	})

	// 删除产品
	$(".delete_product").click(function () {
		if(confirm("确定要删除该产品？"))
		{
			var tr = event.target.parentNode.parentNode;
			var id = $(tr).attr("id");
			$.ajax({
				url: virtualDomain + "/Admin/DeleteProduct",
				type: "POST",
				data: "_id=" + id,
				success: function (data) {
					if (data.ErrorCode != 0) {
						alert(data.ErrorMessage);
						return false;
					}
					$("#myModal").modal("hide");
					window.location.href = virtualDomain + "/Admin/Index";
				}
			});
		}
	})

	// 添加、修改产品
	$("#add_modify_btn").click(function () {
		var purpose = $("#myModal").attr("purpose");
		if(purpose == "add")
		{
			var type = $("#add_modify_type").val();
			var name = $("#add_modify_name").val();
			var price = $("#add_modify_price").val();
			var link = $("#add_modify_link").val();
			$.ajax({
				url: virtualDomain + "/Admin/AddProduct",
				type: "POST",
				data: 'Type=' + type + "&Name=" + name + "&Price=" + price + "&LinkUrl=" + link,
				success: function (data) {
					if (data.ErrorCode != 0) {
						alert(data.ErrorMessage);
						return false;
					}
					$("#myModal").modal("hide");
					window.location.href = virtualDomain + "/Admin/Index";
				}
			});
		}
		else if(purpose == "modify")
		{
			var id = $("#add_modify_id").val();
			var type = $("#add_modify_type").val();
			var name = $("#add_modify_name").val();
			var price = $("#add_modify_price").val();
			var link = $("#add_modify_link").val();
			$.ajax({
				url: virtualDomain + "/Admin/ModifyProduct",
				type: "POST",
				data: "_id=" + id +'&Type=' + type + "&Name=" + name + "&Price=" + price + "&LinkUrl=" + link,
				success: function (data) {
					if (data.ErrorCode != 0) {
						alert(data.ErrorMessage);
						return false;
					}
					$("#myModal").modal("hide");
					window.location.href = virtualDomain + "/Admin/Index";
				}
			});
		}
		
	})

	// 分页
	$(".pageBtn").click(function () {
		var index = $(event.target).html();
		window.location.href = virtualDomain + "/Admin/Index?index=" + index;
	})
})