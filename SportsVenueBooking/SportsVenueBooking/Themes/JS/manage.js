$(document).ready(function ()
{
    var window_id = "1000";
    $("li").click(function ()
    {
        $(".nav").find("li").attr("class", "");
        $(this).attr("class", "action");
        GetHtmlContext(GetAccessFunction($(this).val()), GetParameters($(this).val()), window_id++);
    });
    function GetAccessFunction(value)
    {
        switch (value)
        {
            case 11: return "/Teacher/BookingSite"; break;
        }
    }
    function GetParameters(value)
    {
        var parameters = new Array();
        switch (value)
        {
        }
        return parameters;
    }
    function GetHtmlContext(funname, parameters, window_id)
    {
        $("#sub_panel").tabs('add', {
            id: window_id,
            title: '新选项卡面板',
            selected: true,
            closable: true,
            tentext: "<div style='windth:180px;margin:auto;margin-top:200px;text-align:center'><img src=\"../../images/loading.gif\"><p>玩命加载中....</p><div>"
        });

        $.ajax({
            type: "post",
            url: funname,
            data: {
                window_id: window_id,
                parameter0: parameters[0],
                parameter1: parameters[1],
                parameter2: parameters[2],
                parameter3: parameters[3],
                parameter4: parameters[4],
                parameter5: parameters[5],
                parameter6: parameters[6],
                parameter7: parameters[7],
                parameter8: parameters[8],
                parameter9: parameters[9],
            },
            dataType: "html",
            success: function (data)
            {
                var id = "#" + window_id;
                $(id).html(data);
            }
        });
    }
});