using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework.System
{
    public static class LoggerSingleton
    {
        static Logger logger;

        public static Logger GetInstance()
        {
            return (logger != null) ? logger : (logger = new Logger());
        }
    }

    public delegate void IOWrite(string text);
    public delegate string IORead();

    public class IOStream
    {
        public IOWrite Write { get; set; }
        public IORead Read { get; set; }

        public IOStream()
        {

        }
    }

    /// <summary>
    /// IO for debug
    /// </summary>
    public class Logger
    {
        public IOStream Stream { get; set; }

        public Logger()
        {

        }

        public void Write(string text)
        {
            if (this.Stream != null)
            {
                if (this.Stream.Write != null)
                    this.Stream.Write(text);
                else
                    throw new Exception("Method \"IOStream.Write\" is not redefined");
            }
            else
                throw new Exception("Field \"Stream\" is not initialize");
        }

        public string Read()
        {
            if (this.Stream != null)
            {
                if (this.Stream.Read != null)
                    return this.Stream.Read();
                else
                    throw new Exception("Method \"IOStream.Read\" is not redefined");
            }
            else
                throw new Exception("Field \"Stream\" is not initialize");
        }
    }
}
