using AssetsTools.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelImposter.Unity
{
    class TextureWriter
    {
        public const string TEXTURE_PATH = "custom.assets.resS";
        public static List<byte> bytes = new List<byte>();

        public static long AddTexture(UnityManager unityMgr, string curl)
        {
            string base64 = curl.Substring(curl.IndexOf(",") + 1);
            byte[] input = Convert.FromBase64String(base64);

            // Load Image
            MemoryStream memoryStream = new MemoryStream(input);
            memoryStream.Position = 0;
            Bitmap bmp = (Bitmap)Bitmap.FromStream(memoryStream);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            memoryStream.Close();
            int len = bmp.Width * bmp.Height * 4;

            byte[] byteBuffer = new byte[len];
            for (var y = 0; y < bmp.Height; y++)
            {
                for (var x = 0; x < bmp.Width; x++)
                {

                    Color pixel = bmp.GetPixel(x, y);
                    int pos = (y * bmp.Width + x) * 4;
                    byteBuffer[pos] = pixel.R;
                    byteBuffer[pos + 1] = pixel.G;
                    byteBuffer[pos + 2] = pixel.B;
                    byteBuffer[pos + 3] = pixel.A;
                }
            }
            var start = bytes.Count;
            bytes.AddRange(byteBuffer);

            long textureId = unityMgr.NextAssetId();
            long spriteId = unityMgr.NextAssetId();

            AssetGenerator generator = new AssetGenerator(unityMgr);
            UnityAsset texture2d = generator.CreateTexture2D(bmp.Width, bmp.Height, start, len, Path.Combine(Path.GetDirectoryName(unityMgr.assetFileDir),TEXTURE_PATH), textureId);
            UnityAsset sprite = generator.CreateSprite(textureId, bmp.Width, bmp.Height, spriteId);

            unityMgr.Save(texture2d);
            unityMgr.Save(sprite);

            return spriteId;
        }

        public static void Export(UnityManager unityMgr)
        {
            string path = Path.Combine(Path.GetDirectoryName(unityMgr.assetFileDir), TEXTURE_PATH);
            if (File.Exists(path))
                File.Delete(path);
            File.WriteAllBytes(path, bytes.ToArray());
        }
    }
}
