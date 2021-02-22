using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelImposter
{
    struct MapData
    {
        public string name { get; set; }
        public MapAsset[] assets { get; set; }
    }

    struct MapAsset
    {
        public string name { get; set; }
        public string type { get; set; }

        public uint unityType { get; set; }

        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public double xScale { get; set; }
        public double yScale { get; set; }
        public double zRotate { get; set; }

        public string curl { get; set; }

        public Collider[] colliders { get; set; }
    }

    struct Collider
    {
        public ColliderPoint[] points { get; set; }
    }

    struct ColliderPoint
    {
        public double x { get; set; }
        public double y { get; set; }
    }
}
