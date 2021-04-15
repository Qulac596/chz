using chz.WindowsServices.DataBase;
using chz.WindowsServices.Log;
using chz.WindowsServices.Setting;
using commoni.Infrastructure;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace chz.Infrastructure
{
    /*
     * Осуществляет логирование сообщений в файл
     */
    public class Logger : LoggerBase, ILogger
    {

        public Logger(string folderName, string serviceName, ISettingProvider setting, IDataBase dataBase) :
            base(folderName, serviceName, setting, dataBase)
        {
        }

        private readonly object locker = new object();

        public void WriteError(Exception ex)
        {
            Write("Error", ex);
        }

        public void WriteFatal(Exception ex)
        {
            Write("Fatal", ex);
        }


        public void WriteDebug(string message)
        {
            var text = "[Debug] [" + GetText(message);
            Write(text);
        }

        public void WriteInfo(string message)
        {
            var text = "[Info] [" + GetText(message);
            Write(text);
        }

        private void Write(string label, Exception ex)
        {
            var agg = ex as AggregateException;

            if (agg != null)
            {
                foreach (var e in agg.InnerExceptions)
                {
                    var text = "[" + label + "] " + GetText(e);
                    Write(text);
                }
            }
            else
            {
                var text = "[" + label + "] " + GetText(ex);
                Write(text);
            }
        }

        private static string GetText(string message)
        {
            return DateTime.Now + "] [" + message + "]\r\n";
        }

        private string GetText(Exception ex)
        {
            return "[" + DateTime.Now + "] [" + GetClassName(ex) + "] [" + GetMetchodName(ex) + "] [" + ex.Message + "]\r\n";
        }

        private void Write(string text)
        {
            try
            {
                var pattern = "\\[(?<str>\\w+)]";

                var msgType = Regex.Match(text, pattern).Groups["str"].Value;

                DataBase.Write(ServiceName, msgType, text);
            }
            catch (DataBaseException ex)
            {
                try
                {
                    lock (locker)
                    {
                        var dateTime = DateTime.Now;
                        var patch = GetPatch(dateTime);
                        Directory.CreateDirectory(patch);
                        File.AppendAllText(patch + dateTime.Hour + ".log", text);
                    }
                }
                catch (Exception e)
                {
                    //ни чего не делаем
                    throw;
                }
            }

            Console.WriteLine(text);
        }

        private string GetClassName(Exception e)
        {
            if (e.TargetSite != null && e.TargetSite.DeclaringType != null)
            {
                return e.TargetSite.DeclaringType + "";
            }
            return "";
        }

        private string GetMetchodName(Exception e)
        {
            if (e.TargetSite != null && e.TargetSite.Name != null)
            {
                return e.TargetSite + "";
            }
            return "";
        }
    }
}
