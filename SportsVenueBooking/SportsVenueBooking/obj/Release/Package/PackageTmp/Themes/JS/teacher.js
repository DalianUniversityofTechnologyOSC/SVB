$(document).ready(function ()
{
    $("#sub_panel").tabs({
        fit: true
    });
    $("#user_info").propertygrid({
        url: '/Teacher/GetUserInfo',
        showGroup: true,
        scrollbarSize: 0
    });
});