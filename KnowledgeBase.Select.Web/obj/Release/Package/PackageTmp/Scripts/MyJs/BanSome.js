
$(function () {
    //禁用鼠标右键，文档选择，复制
    $("body").bind("contextmenu", function (event) {
        return false;
    });
    $("body").bind("selectstart", function () { return false; });
    //禁用键盘相关操作
    $("body").bind("keydown", function (e) {
        e = window.event || e;
        //屏蔽F5刷新键
        if (event.keyCode == 116) {
            e.keyCode = false;
            return false;
        }
        //屏蔽 Alt+ 方向键 ←
        //屏蔽 Alt+ 方向键 →
        if ((event.altKey) && ((event.keyCode == 37) || (event.keyCode == 39))) {
            event.returnValue = false;
            return false;
        }
        屏蔽退格删除键
        if (event.keyCode == 8) {
            return false;
        }

        //屏蔽ctrl+R
        if ((event.ctrlKey) && (event.keyCode == 82)) {
            e.keyCode = 0;
            return false;
        }
        if (event.keyCode == 122) { event.keyCode = 0; event.returnValue = false; }    //屏蔽F11
        if (event.ctrlKey && event.keyCode == 78) event.returnValue = false;      //屏蔽Ctrl+n
        if (event.shiftKey && event.keyCode == 121) event.returnValue = false;    //屏蔽shift+F10
        if (window.event.srcElement.tagName == "A" && window.event.shiftKey)
            window.event.returnValue = false;       //屏蔽shift加鼠标左键新开一网页
        if ((window.event.altKey) && (window.event.keyCode == 115)) {             //屏蔽Alt+F4
            window.showModelessDialog("about:blank", "", "dialogWidth:1px;dialogheight:1px");
            return false;
        }
    });
});
