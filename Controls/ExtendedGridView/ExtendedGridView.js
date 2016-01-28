function CheckAll(me, RowClassName, SelectedCssClass)
{
    var isFirst = true;
    var index = me.name.indexOf('_');  
    var prefix = me.name.substr(0, index); 
    var checkstatus = me.checked;
    for(var i=0; i<me.form.length; i++) 
    { 
        var o = me.form[i]; 
        if (o.type == 'checkbox') 
        { 
            if (me.name != o.name) 
            {
                if (o.name.substring(0, prefix.length) == prefix) 
                {
                    if(!o.disabled)
                    {
                        if(SelectedCssClass || !isFirst )
                        {
                            o.checked = me.checked;
                            if(SelectedCssClass)
                                o.checked ? HighLight(o, SelectedCssClass) : LowLight(o, RowClassName);
                            
                        }else{
                            o.click(); //ҳ�������������ϵ�ȫѡ��ѡ��Ļ�,����п��ܴ�������CheckAll().
                            isFirst = false;
                            o.checked = me.checked;
                        }
                    }
                }
            }
        } 
    } 
}

function CheckItem(me, HeaderID, RowClassName, SelectedCssClass, count)
{
    if (me.checked)
    {
        HighLight(me, SelectedCssClass);
    }
    else
    {
        LowLight(me, RowClassName);
    }
    
    var headerchk = document.getElementById(HeaderID)
    var index = headerchk.name.indexOf('_');  
    var prefix = headerchk.name.substr(0, index); 
    var totalnumber = 0;
    var totalchecked=0;
    if(headerchk.checked)
    {
        headerchk.checked = headerchk.checked & 0;
    }
    for(var i=0; i<document.forms[0].length; i++) 
    { 
        var o = document.forms[0][i]; 
        if (o.type == 'checkbox') 
        { 
            if (o.name != headerchk.name) 
            {
                if (o.name.substring(0, prefix.length) == prefix) 
                {
                    totalnumber++;
                    if (o.checked) totalchecked++;
                }
            }
        } 
    }
    if (totalnumber == totalchecked)
    {
        headerchk.checked = true;
        }
}

function HighLight(Element, SelectedCssClass)
{
    while (Element.tagName != "TR") { Element = Element.parentNode; }
    Element.className = SelectedCssClass;
    CurrentClassName = Element.className;
}

function LowLight(Element, RowClassName)
{
    while (Element.tagName != "TR") { Element = Element.parentNode; }
    Element.className = RowClassName;
    CurrentClassName = Element.className;
}

function MouseOver(me, MouseOverCssClass)
{
    CurrentClassName = me.className;
    me.className = MouseOverCssClass;
}

function MouseOut(me)
{
    me.className = CurrentClassName;
}

function RowDblclick(dblclickURL)
{
    window.location.href =dblclickURL;
}
