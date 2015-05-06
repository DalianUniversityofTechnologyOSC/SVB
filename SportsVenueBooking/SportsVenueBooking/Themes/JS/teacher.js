$(document).ready(function ()
{
    $("#sub_panel").tabs({
        fit: true,
        onClose: function (title, index)
        {
            delOpenWindow(title, index);
        }
    });
    $("#user_info").propertygrid({
        url: '/Teacher/GetUserInfo',
        showGroup: true,
        scrollbarSize: 0
    });
});