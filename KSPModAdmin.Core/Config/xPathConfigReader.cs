using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSPModAdmin.Core.Config
{
    public class xPathConfigReader
    {
        public xPathConfigReader()
        { }

        public static Dictionary<string, string> Read(string configFileContent)
        {
            var result = new Dictionary<string, string>();

            var rows = configFileContent.Replace('\r', ' ').Split('\n');

            foreach (var row in rows)
            {
                if (string.IsNullOrEmpty(row.Trim()))
                    continue;

                var index = row.IndexOf('=');
                var key = row.Substring(0, index).Trim();
                var value = row.Substring(index+1).Trim();

                result.Add(key, value);
            }

            return result;
        }
    }
}
