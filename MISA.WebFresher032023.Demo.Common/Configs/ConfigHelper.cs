using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.Common.Configs
{
    public static class ConfigHelper
    {

        public static AppConfig ReadDefaultConfig()
        {
            string path = Path.Combine("..", "MISA.WebFresher032023.Demo.Common", "Configs", "Default.json");
            string jsonData = File.ReadAllText(path);
            var config = JsonSerializer.Deserialize<AppConfig>(jsonData);
            return config;
        }

        public static AppConfig ReadConfig(string fileName)
        {
            string path = Path.Combine("..", "MISA.WebFresher032023.Demo.Common", "Configs", fileName);
            string jsonData = File.ReadAllText(path);
            var config = JsonSerializer.Deserialize<AppConfig>(jsonData);
            return config;
        }


    }
}
