
var oEditer;
function CustomValidate()
{
var value = oEditer.GetXHTML(true);
if(value=='')
{
var obj = document.getElementById('error');//
obj.innerHTML="不允许为空!";
return false; 
}else{
var obj = document.getElementById('error');
obj.innerHTML="";
}
return true;
}
function FCKeditor_OnComplete( editorInstance )
{ 
oEditer = editorInstance;
}
