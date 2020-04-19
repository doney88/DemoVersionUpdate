using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoVersionUpdate
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //检查版本是否与最新版本一致
            string currentVersion="";
            string serverVersion="";
            string updateType="";

            if (CheckSysVerionNew(ref currentVersion,ref serverVersion,ref updateType))
            {
                if (MessageBox.Show($"发现新版本！是否更新？ \r\n当前版本{currentVersion} \r\n最新版本{serverVersion}","版本更新",MessageBoxButtons.YesNo)== DialogResult.Yes)
                {
                    //异步打开下载程序
                    //解压下载的安装包
                    //运行安装包
                    Process.Start(Application.StartupPath + "\\Update\\Update.exe",updateType).Dispose();
                    return;
                }
            }
            Application.Run(new Form1());
        }

        private static bool CheckSysVerionNew(ref string currentVersion, ref string serverVersion,ref string updateType)
        {
            currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            using (SqlConnection conn = new SqlConnection("server=.;database=HG;uid=sa;pwd=Chendong144216,"))
            {
                string sql = "SELECT TOP 1 FVersion,FUpdateType FROM tblSysVersion ORDER BY FUpdateTime DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    serverVersion = dr["FVersion"].ToString();
                    //int indexUpdateType = dr.GetOrdinal("FUpdateType");
                    //updateType = dr.GetInt32(indexUpdateType);
                    updateType = dr["FUpdateType"].ToString();

                    if (serverVersion == null || serverVersion ==currentVersion)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
