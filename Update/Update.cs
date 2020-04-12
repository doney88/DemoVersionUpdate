using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Update
{
    public partial class Update : Form
    {
        string _UpdateType;
        public Update()
        {
            InitializeComponent();
        }

        public Update(string updateType) : base()
        {
            _UpdateType = updateType;
        }

        private void btnCloseMainProcess_Click(object sender, EventArgs e)
        {
            CloseMainProcess();
        }

        private void CloseMainProcess()
        {
            System.Collections.ArrayList procList = new System.Collections.ArrayList();
            foreach (System.Diagnostics.Process thisProc in System.Diagnostics.Process.GetProcesses())
            {
                if (thisProc.ProcessName == "DemoVersionUpdate")
                { 
                    if (!thisProc.CloseMainWindow())
                        thisProc.Kill(); //当发送关闭窗口命令无效时强行结束进程                    
                }
            }
        }

        private void ReOpenMainProcess()
        {
            Process.Start(Application.StartupPath + "\\..\\DemoVersionUpdate.exe").Dispose();
        }

        private void btnReopenMainProcess_Click(object sender, EventArgs e)
        {
            ReOpenMainProcess();
        }

        private void btnUpdateMainProcess_Click(object sender, EventArgs e)
        {
            //经过Ftp将安装包下载至安装路径（如果有旧的注意覆盖）
            //然后启动安装包覆盖安装
            if (string.IsNullOrEmpty(_UpdateType)||_UpdateType =="0")
            {
                Process.Start(Application.StartupPath + "\\..\\SetUp.exe").Dispose();
            }
        }
    }
}
