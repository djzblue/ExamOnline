using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

    public class clsWinrar
    {
        /// <summary>
        /// 是否安装了Winrar
        /// </summary>
        /// <returns></returns>
        static public bool Exists()
        {
            RegistryKey the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
            return !string.IsNullOrEmpty(the_Reg.GetValue("").ToString());
        }

        /// <summary>
        /// 打包成Rar
        /// </summary>
        /// <param name="patch"></param>
        /// <param name="rarPatch"></param>
        /// <param name="rarName"></param>
        public void CompressRAR(string patch, string rarPatch, string rarName)
        {
            string the_rar;
            RegistryKey the_Reg;
            object the_Obj;
            string the_Info;
            ProcessStartInfo the_StartInfo;
            Process the_Process;
            try
            {
                the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
                the_Obj = the_Reg.GetValue("");
                the_rar = the_Obj.ToString();
                the_Reg.Close();
                the_rar = the_rar.Substring(1, the_rar.Length - 7);
                Directory.CreateDirectory(patch);
                //命令参数
                //the_Info = " a    " + rarName + " " + @"C:Test?70821.txt"; //文件压缩
                the_Info = " a    " + rarName + " " + patch + " -r"; ;
                the_StartInfo = new ProcessStartInfo();
                the_StartInfo.FileName = the_rar;
                the_StartInfo.Arguments = the_Info;
                the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //打包文件存放目录
                the_StartInfo.WorkingDirectory = rarPatch;
                the_Process = new Process();
                the_Process.StartInfo = the_StartInfo;
                the_Process.Start();
                the_Process.WaitForExit();
                the_Process.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="unRarPatch"></param>
        /// <param name="rarPatch"></param>
        /// <param name="rarName"></param>
        /// <returns></returns>
        public string unCompressRAR(string unRarPatch, string rarPatch, string rarName)
        {
            string the_rar;
            RegistryKey the_Reg;
            object the_Obj;
            string the_Info;


            try
            {
                the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
                the_Obj = the_Reg.GetValue("");
                the_rar = the_Obj.ToString();
                the_Reg.Close();
                //the_rar = the_rar.Substring(1, the_rar.Length - 7);

                if (Directory.Exists(unRarPatch) == false)
                {
                    Directory.CreateDirectory(unRarPatch);
                }
                the_Info = "e " + rarName + " " + unRarPatch + " -y";

                ProcessStartInfo the_StartInfo = new ProcessStartInfo();
                the_StartInfo.FileName = the_rar;
                the_StartInfo.Arguments = the_Info;
                the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                the_StartInfo.WorkingDirectory = rarPatch;//获取压缩包路径

                Process the_Process = new Process();
                the_Process.StartInfo = the_StartInfo;
                the_Process.Start();
                the_Process.WaitForExit();
                the_Process.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unRarPatch;
        }
    }


//RAR参数：

//一、压缩命令
//1、将temp.txt压缩为temp.rarrar a temp.rar temp.txt 
//2、将当前目录下所有文件压缩到temp.rarrar a temp.rar *.* 
//3、将当前目录下所有文件及其所有子目录压缩到temp.rarrar a temp.rar *.* -r 
//4、将当前目录下所有文件及其所有子目录压缩到temp.rar，并加上密码123rar a temp.rar *.* -r -p123

//二、解压命令
//1、将temp.rar解压到c:\temp目录rar e temp.rar c:\temprar e *.rar c:\temp(支持批量操作) 
//2、将temp.rar解压到c:\temp目录，并且解压后的目录结构和temp.rar中的目录结构一


//压缩目录test及其子目录的文件内容 
//Wzzip test.zip test -r -P 
//WINRAR A test.rar test -r 

//删除压缩包中的*.txt文件 
//Wzzip test.zip *.txt -d 
//WinRAR d test.rar *.txt 


//刷新压缩包中的文件，即添加已经存在于压缩包中但更新的文件 
//Wzzip test.zip test -f 
//Winrar f test.rar test 


//更新压缩包中的文件，即添加已经存在于压缩包中但更新的文件以及新文件 
//Wzzip test.zip test -u 
//Winrar u test.rar test 


//移动文件到压缩包，即添加文件到压缩包后再删除被压缩的文件 
//Wzzip test.zip -r -P -m 
//Winrar m test.rar test -r 


//添加全部 *.exe 文件到压缩文件，但排除有 a或b 
//开头名称的文件 
//Wzzip test *.exe -xf*.* -xb*.* 
//WinRAR a test *.exe -xf*.* -xb*.* 


//加密码进行压缩 
//Wzzip test.zip test 
//-s123。注意密码是大小写敏感的。在图形界面下打开带密码的压缩文件，会看到+号标记（附图1）。 
//WINRAR A test.rar test -p123 
//-r。注意密码是大小写敏感的。在图形界面下打开带密码的压缩文件，会看到*号标记（附图2）。 


//按名字排序、以简要方式列表显示压缩包文件 
//Wzzip test.zip -vbn 
//Rar l test.rar 


//锁定压缩包，即防止未来对压缩包的任何修改 
//无对应命令 
//Winrar k test.rar 


//创建360kb大小的分卷压缩包 
//无对应命令 
//Winrar a -v360 test 


//带子目录信息解压缩文件 
//Wzunzip test -d 
//Winrar x test -r 


//不带子目录信息解压缩文件 
//Wzunzip test 
//Winrar e test 


//解压缩文件到指定目录，如果目录不存在，自动创建 
//Wzunzip test newfolder 
//Winrar x test newfolder 


//解压缩文件并确认覆盖文件 
//Wzunzip test -y 
//Winrar x test -y 


//解压缩特定文件 
//Wzunzip test *.txt 
//Winrar x test *.txt 


//解压缩现有文件的更新文件 
//Wzunzip test -f 
//Winrar x test -f 


//解压缩现有文件的更新文件及新文件 
//Wzunzip test -n 
//Winrar x test -u 


//批量解压缩文件 
//Wzunzip *.zip 
//WinRAR e *.rar
