using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.IO;


namespace Coletor.Examine
{
    public class ConfigurationManager
    {
        private static Configuration _Configuration;

        public static Configuration Configuration
        {
            get
            {
                if (_Configuration == null){
                    String configFileName = "coletor.json";
                    String configFilePath = IOHelper.MapPath( Path.Combine(Umbraco.Core.IO.SystemDirectories.Config, configFileName) );
                    
                    StreamReader r = new StreamReader(configFilePath);
                    String json = r.ReadToEnd();

                    return _Configuration = JsonConvert.DeserializeObject<Configuration>(json);
                }else{
                    return _Configuration;
                }
            }
        }
    }
}
