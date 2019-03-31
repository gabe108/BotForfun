using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

class Config
{
    private const string m_configFolder = "Resources";
    private const string m_configFile = "config.json";
    private const string m_fullPath = m_configFolder + "/" + m_configFile;

    public static BotConfig bot;

    static Config()
    {
        if (!Directory.Exists(m_configFolder))
            Directory.CreateDirectory(m_configFolder);

        if(!File.Exists(m_fullPath))
        {
            bot = new BotConfig();
            string json = JsonConvert.SerializeObject(bot, Formatting.Indented);
            File.WriteAllText(m_fullPath, json);
        }
        else
        {
            string json = File.ReadAllText(m_fullPath);
            var data = JsonConvert.DeserializeObject<BotConfig>(json);

            bot = data;
        }
    }
}

public struct BotConfig
{
    public string token;
    public string cmdPrefix;
}
