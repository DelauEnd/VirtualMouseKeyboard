using Newtonsoft.Json;
using System.IO;

namespace VirtualMouseKeyboard.Behaviour.Configuration
{
    public class ConfigurationManager
    {
        private const string CONFIG_FILE_PATH = @"configuration\Config.json";

        public Configuration Configuration { get; private set; }

        public ConfigurationManager() 
        {
            if (!File.Exists(CONFIG_FILE_PATH))
            {
                Configuration = new Configuration();

                FileInfo fileInfo = new FileInfo(CONFIG_FILE_PATH);
                if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();

                File.WriteAllText(CONFIG_FILE_PATH, JsonConvert.SerializeObject(Configuration));
            }
            else
            {
                Configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(CONFIG_FILE_PATH));
            }
        }
    }
}
