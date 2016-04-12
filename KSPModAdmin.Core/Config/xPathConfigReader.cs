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

            var rows = configFileContent.Split(new [] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var row in rows)
            {
                var pair = row.Split('=');
                if (pair.Length != 2)
                    continue;

                result.Add(pair[0], pair[1]);
            }

            return result;
        }
    }
}
