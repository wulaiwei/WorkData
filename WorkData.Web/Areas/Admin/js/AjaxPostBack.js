//全局Javascript工厂
function ExePostBack(url, action) {
    switch (action) {
        case "CheckFaceBook":
            ComExeNoCheckPostBack(url, "您是否确定批量审核这些数据", CheckOrDelete);
            break;
        case "DeleteFaceBook":
            ComExeNoCheckPostBack(url, "删除记录后不可恢复，您确定吗？", CheckOrDelete);
            break;
        case "CheckLink":
            ComExeNoCheckPostBack(url, "您是否确定批量审核这些数据", CheckOrDelete);
            break;
        case "DeleteLink":
            ComExeNoCheckPostBack(url, "删除记录后不可恢复，您确定吗？", CheckOrDelete);
            break;
        case "DeleteChannel":
            ComExeNoCheckPostBack(url, "删除记录后不可恢复，您确定吗？", DeleteChannel);
            break;
        default:
    }
}
//执行回传无复选框确认函数,并执行回调函数
function ComExeNoCheckPostBack(url, msg, callback) {
    parent.dialog({
        title: '提示',
        content: msg,
        okValue: '确定',
        ok: function () {
            callback(url);
        },
        cancelValue: '取消',
        cancel: function () { }
    }).showModal();
}

//审核或者删除
function CheckOrDelete(url) {
    var list = GetAllCheckedID();
    $.post(url, { data: list }, function (result) {
        if (result == "True") {
            document.location.reload();//当前页面
        }
    });
}

//删除频道信息
function DeleteChannel(url) {
    var list = GetAllCheckedID();
    $.post(url, { data: list }, function (result) {
        jsdialog(result);
    });
}

//获取所有选中项的ID
function GetAllCheckedID() {
    var allvalue = "";
    $(".checkall").each(function () {
        if ($(this).is(':checked')) {
            allvalue += $(this).val() + ",";
        }
    });
    return allvalue.substring(0, allvalue.length - 1);
}

//弹出一个Dialog窗口
function jsdialog(msgcontent) {
    var d = dialog({
        title: '提示',
        content: msgcontent,
        okValue: '确定',
        ok: function () {
            document.location.reload();//当前页面
        },
        onclose: function () {
            document.location.reload();//当前页面
        }
    }).showModal();
}