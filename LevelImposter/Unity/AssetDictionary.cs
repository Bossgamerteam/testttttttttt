using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelImposter.Unity
{
    class AssetDictionary
    {
        public static Dictionary<string, string> dictionary;

        public static void Import()
        {
            if (dictionary != null)
                return;

            string db = Properties.Resources.db;
            dictionary = db.Split(new[] { "\n" }, StringSplitOptions.None).ToDictionary(
                i => i.Split(':')[0],
                i => i.Split(':')[1]
            );
        }

        public static string Get(string id)
        {
            string output;
            bool exists = dictionary.TryGetValue(id, out output);
            return output;
        }
    }
}
