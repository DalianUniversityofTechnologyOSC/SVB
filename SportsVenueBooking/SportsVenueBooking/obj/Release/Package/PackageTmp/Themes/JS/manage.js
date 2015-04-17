$(document).ready(function ()
{
    var openWindowList = new Array();
    var window_id = 1000;
    $("li").click(function ()
    {
        $(".nav").find("li").attr("class", "");
        $(this).attr("class", "action");
        if (!IsOpen($(this).val()))
        {
            window_id = $(this).val();
            GetHtmlContext(GetAccessFunction($(this).val()), GetParameters($(this).val()), window_id, $(this).text());
            addOpenWindow(window_id);
        }
        else
        {
            $("#sub_panel").tabs("select", $(this).text());
        }
    });
    function GetAccessFunction(value)
    {
        switch (value)
        {
            case 11: return "/Teacher/BookingSite"; break;
            case 12: return "/Teacher/MyReservation"; break;
            case 14: return "/Teacher/TeacherSet"; break;
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
    function GetHtmlContext(funname, parameters, window_id, title)
    {
        $("#sub_panel").tabs('add', {
            id: window_id,
            title: title,
            selected: true,
            closable: true,
            onClose: delOpenWindow,
            tentext: "<div style='windth:180px;margin:auto;margin-top:200px;text-align:center'><img src=\"/Themes/Images/loading.gif\"><p>玩命加载中....</p><div>"
        });

        $.ajax({
            type: "post",
            url: funname,
            data: {
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
    function addOpenWindow(value)
    {
        openWindowList[openWindowList.length] = value;
    }

    function IsOpen(value)
    {
        for (var i in openWindowList)
        {
            if (openWindowList[i] == value)
            {
                return true;
            }
        }
    }

    function delOpenWindow(title, index)
    {
        for (var i in openWindowList)
        {
            if (openWindowList[i] == value)
            {
                openWindowList[i] = "";
            }
        }
    }
});