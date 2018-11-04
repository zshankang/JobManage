using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManage.Log
{
    /// <summary>
    /// 文本日志记录类
    /// </summary>
    public class LogFileHelper
    {

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="context">内容</param>
        /// <param name="Type">文件名</param>
        /// <param name="path">文件夹名</param>
        public static void WriteLog(string context, string Type = null, string path = null)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\" + (string.IsNullOrEmpty(path) ? "logs" : path) + "";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fileName = filePath + string.Format("\\{0}_{1}.txt", DateTime.Now.ToString("yyyyMMdd"), string.IsNullOrEmpty(Type) ? "" : Type);
            deleteLogfile(filePath + "\\{0}_" + (string.IsNullOrEmpty(Type) ? "" : Type) + ".txt");
            context = string.Format("[{0}]***{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), context);
            if (!File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(context);
                sw.Close();
                fs.Close();
            }
            else
            {
                FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(context);
                sw.Close();
                fs.Close();
            }
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="context">内容</param>
        /// <param name="Type">文件名</param>
        /// <param name="path">文件夹名</param>
        public static void WriteLogFormat(string format, string Type, string path,params object[] arg)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\" + (string.IsNullOrEmpty(path) ? "logs" : path) + "";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fileName = filePath + string.Format("\\{0}_{1}.txt", DateTime.Now.ToString("yyyyMMdd"), string.IsNullOrEmpty(Type) ? "" : Type);
            deleteLogfile(filePath + "\\{0}_" + (string.IsNullOrEmpty(Type) ? "" : Type) + ".txt");
            string context = string.Format("[{0}]***", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) + string.Format(format, arg);
            if (!File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(context);
                sw.Close();
                fs.Close();
            }
            else
            {
                FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(context);
                sw.Close();
                fs.Close();
            }
        }

        private static void deleteLogfile(string format)
        {
            var filename = string.Format(format, DateTime.Today.AddDays(-5));
            if (File.Exists(filename))
                File.Delete(filename);
        }
    }
}
