//添加赋值
function AddAttr(addHref) {
    $(".add").attr("href", addHref);
}

//删除赋值
function RemoveAttr(delHref) {
    $(".remove").each(function () {
        var key = $(this).parent().find(".hideKey").val();
        var href = delHref + key;
        $(this).attr("href", href);
    });
}

//编辑赋值
function EditAttr(editHref) {
    $(".edit").each(function () {
        var key = $(this).parent().find(".hideKey").val();
        var href = editHref + key;
        $(this).attr("href", href);
    });
}

//表单设计赋值
function IndexDesignAttr() {
    $(".index-desgin").each(function () {
        var model = $(this).parent().find(".hideModel").val();
        if (model === "") {
            $(this).hide();
        } else {
            var key = $(this).parent().find(".hideKey").val();
            $(this).removeAttr("onclick");
            $(this).attr("onclick", "ShowDesignDialog('列表设计', '/Admin/Category/DesignIndex?Key=" + key + "&ModelKey=" + model + "', '/Admin/Category/DesignIndex')");
        }

    });
}

//表单设计赋值
function FormDesginAttr() {
    $(".form-desgin").each(function () {
        var model = $(this).parent().find(".hideModel").val();
        if (model === "") {
            $(this).hide();
        } else {
            var key = $(this).parent().find(".hideKey").val();
            $(this).removeAttr("onclick");
            $(this).attr("onclick", "ShowDesignDialog('表单设计', '/Admin/Category/DesignForm?Key=" + key + "&ModelKey=" + model + "', '/Admin/Category/DesignForm')");
        }

    });
}