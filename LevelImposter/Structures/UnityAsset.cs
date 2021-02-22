using AssetsTools.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelImposter
{
    struct UnityAsset
    {
        public AssetTypeValueField data { get; set; }
        public UnityClass classID { get; set; }
        public long index { get; set; }
    }
}
