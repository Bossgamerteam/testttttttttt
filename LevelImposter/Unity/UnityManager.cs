using AssetsTools.NET;
using AssetsTools.NET.Extra;
using LevelImposter.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelImposter
{
    class UnityManager : AssetsManager
    {
        public AssetsManager assetsManager;
        public AssetsFileInstance assetsFileInstance;
        public AssetsFileTable assetsFileTable;
        public List<AssetsReplacer> replacers = new List<AssetsReplacer>();
        public string assetFileDir;
        public long nextAssetId = 0;

        public UnityManager(string dataFileName, bool backup = true)
        {
            // Get Asset File Directory
            assetFileDir = GameFinder.FindDir();
            if (assetFileDir == null)
                throw new FileNotFoundException();
            assetFileDir = Path.Combine(assetFileDir, dataFileName);
            if (backup)
                BackupAssetFile();

            // AssetTools.Net Classes
            assetsManager = new AssetsManager();
            MemoryStream classdata = new MemoryStream(Properties.Resources.classdata);
            assetsManager.LoadClassPackage(classdata);
            string dirToLoad = backup ? assetFileDir + ".old" : assetFileDir;
            assetsFileInstance = assetsManager.LoadAssetsFile(dirToLoad, false);
            assetsManager.LoadClassDatabaseFromPackage(assetsFileInstance.file.typeTree.unityVersion);
            assetsFileTable = assetsFileInstance.table;

            // Next Asset ID
            nextAssetId = assetsFileTable.assetFileInfo.Max(i => i.index);
        }

        public UnityAsset GetAsset(long pathID)
        {
            UnityAsset asset = new UnityAsset();
            AssetFileInfoEx fileInfo = assetsFileTable.GetAssetInfo(pathID);

            asset.data = assetsManager.GetTypeInstance(assetsFileInstance.file, fileInfo).GetBaseField();
            asset.index = fileInfo.index;
            asset.classID = (UnityClass)fileInfo.curFileType;
            return asset;
        }

        public UnityAsset GetAsset(string name, UnityClass type)
        {
            UnityAsset asset = new UnityAsset();
            AssetFileInfoEx fileInfo = assetsFileTable.GetAssetInfo(name, (uint)type);

            asset.data = assetsManager.GetTypeInstance(assetsFileInstance.file, fileInfo).GetBaseField();
            asset.index = fileInfo.index;
            asset.classID = (UnityClass)fileInfo.curFileType;
            return asset;
        }

        public UnityAsset[] GetAssetsOfType(UnityClass type)
        {
            List<AssetFileInfoEx> fileInfos = assetsFileTable.GetAssetsOfType((int)type);
            List<UnityAsset> assets = new List<UnityAsset>();
            fileInfos.ForEach(fileInfo =>
            {
                UnityAsset asset = new UnityAsset();
                asset.data = assetsManager.GetTypeInstance(assetsFileInstance.file, fileInfo).GetBaseField();
                asset.index = fileInfo.index;
                asset.classID = (UnityClass)fileInfo.curFileType;
                assets.Add(asset);
            });
            return assets.ToArray();
        }

        public void Save(UnityAsset asset)
        {
            replacers.Add(new AssetsReplacerFromMemory(
                0,
                asset.index,
                (int)asset.classID,
                0xFFFF,
                asset.data.WriteToByteArray()
            ));
        }

        public void Export()
        {
            if (File.Exists(assetFileDir))
                File.Delete(assetFileDir);

            AssetsFileWriter writer = new AssetsFileWriter(File.OpenWrite(assetFileDir));
            assetsFileInstance.file.Write(writer, 0, replacers, 0);
            writer.Close();
        }

        public long NextAssetId()
        {
            nextAssetId++;
            return nextAssetId;
        }

        private void BackupAssetFile()
        {
            if (!File.Exists(assetFileDir))
                throw new FileNotFoundException();
            if (File.Exists(assetFileDir + ".old"))
                return;

            File.Copy(assetFileDir, assetFileDir + ".old");
        }
    }
}
