using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektarbeitWPFar
{
    public class Config
    {
        private static Config settings;
        public string language { get; set; }
        public string selection { get; set; }
        public string file { get; set; }
        public string file_DE { get; set; }
        public string file_EN { get; set; }
        public string connectionString { get; set; }
        public string table_DE { get; set; }
        public string table_EN { get; set; }
        public string table { get; set; }


        private Config() 
        {
            
        }

        public static Config GetInstance()
        {
            if (settings == null)
            {
                settings = new Config();
            }
            return settings;
        }
    }
}
