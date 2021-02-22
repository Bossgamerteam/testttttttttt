using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelImposter
{
    class VersionCheck
    {
        public const string AMONGUS_VERSION = "2020.12.9";

        public static string GetVersion()
        {
            UnityManager unityMgr = new UnityManager("globalgamemanagers", false);
            UnityAsset playerSettings = unityMgr.GetAssetsOfType(UnityClass.PlayerSettings)[0];
            string version = playerSettings.data.Get("bundleVersion").GetValue().AsString();

            Console.WriteLine("Running Among Us " + version);
            return version;
        }

        public static bool IsCurrent()
        {
            return GetVersion().Equals(AMONGUS_VERSION);
        }
    }
}
