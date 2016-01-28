
/*
改造window.open函数,保证所有窗口居中弹出,并指定固定的几种窗口大小.以统一系统中的操作风格.

url        --   弹出窗口路径
sTiltle    --  窗口标题
sSize     --  窗口型号,可以自己再增加
*/
var nwin;      
function openwindow(url,sTitle,sSize)      
{      
    if (url==''){
      return false;
    }

    if (nwin && !nwin.closed){      
       nwin.close();      
    }      
  

    if (sSize == undefined) {
       alert("请指定窗口型号!");
       return false;
    }
    else if (sSize == 100) {
       sWidth  = screen.availWidth;
       sHeight = screen.availHeight;
    }

   else if (sSize == 1) {
       sWidth  = 280;
       sHeight = 120;
    }
    else if (sSize == 2) {
       sWidth  = 600;
       sHeight = 400;
    }

    var l = ( screen.availWidth - sWidth ) / 2;
    var  t = ( screen.availHeight - sHeight ) / 2;      

     
    nwin = window.open(url,sTitle,'left=' + l +',top=' + t 

+',width='+sWidth+',height='+sHeight+',scrollbars=yes,resizable=yes');      
    nwin.focus();      
}
