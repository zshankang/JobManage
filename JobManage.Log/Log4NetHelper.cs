using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace JobManage.Log
{
    public class Log4NetHelper
    {
        static Log4NetHelper()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
        }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isLogConsole">是否输出到控制台</param>
        public static void Error(string msg, bool isLogConsole = false,string logger=null)
        {
            if (isLogConsole)
            {
                Console.WriteLine(msg);
            }
            log4net.ILog log = log4net.LogManager.GetLogger(logger);
            log.Error(msg);
        }

        public static void Info(string msg, bool isLogConsole = false, string logger = null)
        {
            if (isLogConsole)
            {
                Console.WriteLine(msg);
            }
            log4net.ILog log = log4net.LogManager.GetLogger(logger);
            log.Info(msg);
        }

        public static void Dubug(string msg, bool isLogConsole = false, string logger = null)
        {
            if (isLogConsole)
            {
                Console.WriteLine(msg);
            }
            log4net.ILog log = log4net.LogManager.GetLogger(logger);
            log.Debug(msg);
        }
                
        
    }
}
