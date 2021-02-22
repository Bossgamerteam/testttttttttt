using AssetsTools.NET;
using AssetsTools.NET.Extra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelImposter.Unity
{
    class AssetGenerator
    {
        public UnityManager unityMgr { get; set; }

        public AssetGenerator(UnityManager unityMgr)
        {
            this.unityMgr = unityMgr;
        }

        public AssetTypeValueField GetDefaultValueField(UnityClass unityClass)
        {
            AssetTypeTemplateField template = new AssetTypeTemplateField();
            ClassDatabaseType cldbGameObject = AssetHelper.FindAssetClassByName(unityMgr.assetsManager.classFile, unityClass.ToString());
            template.FromClassDatabase(unityMgr.assetsManager.classFile, cldbGameObject, 0);
            return ValueBuilder.DefaultValueFieldFromTemplate(template);
        }

        public UnityAsset CreateGameObject(string name, long id)
        {
            AssetTypeValueField gameObject = GetDefaultValueField(UnityClass.GameObject);

            gameObject.Get("m_Name").GetValue().Set(name);
            gameObject.Get("m_IsActive").GetValue().Set(true);
            gameObject.Get("m_Component").Get("Array").GetValue().Set(new AssetTypeArray());

            UnityAsset unityAsset = new UnityAsset();
            unityAsset.data = gameObject;
            unityAsset.classID = UnityClass.GameObject;
            unityAsset.index = id;

            return unityAsset;
        }
        public UnityAsset CreateTransformComponent(long parentID, MapAsset asset, long id)
        {
            AssetTypeValueField transform = GetDefaultValueField(UnityClass.Transform);

            transform.Get("m_LocalPosition").Get("x").GetValue().Set(asset.x);
            transform.Get("m_LocalPosition").Get("y").GetValue().Set(asset.y);
            transform.Get("m_LocalPosition").Get("z").GetValue().Set(asset.z);
            transform.Get("m_LocalScale").Get("x").GetValue().Set(asset.xScale);
            transform.Get("m_LocalScale").Get("y").GetValue().Set(asset.yScale);
            transform.Get("m_LocalScale").Get("z").GetValue().Set(1);
            transform.Get("m_LocalRotation").Get("z").GetValue().Set(asset.zRotate);
            transform.Get("m_LocalRotation").Get("w").GetValue().Set(1);
            transform.Get("m_Father").Get("m_FileID").GetValue().Set(0);
            transform.Get("m_Father").Get("m_PathID").GetValue().Set(16553);
            transform.Get("m_GameObject").Get("m_FileID").GetValue().Set(0);
            transform.Get("m_GameObject").Get("m_PathID").GetValue().Set(parentID);

            UnityAsset unityAsset = new UnityAsset();
            unityAsset.data = transform;
            unityAsset.classID = UnityClass.Transform;
            unityAsset.index = id;

            return unityAsset;
        }
        public UnityAsset CreateSpriteRendererComponent(long parentIndex, long spriteIndex, long id)
        {
            AssetTypeValueField sprite = GetDefaultValueField(UnityClass.SpriteRenderer);

            sprite.Get("m_Enabled").GetValue().Set(true);
            sprite.Get("m_CastShadows").GetValue().Set(0);
            sprite.Get("m_ReceiveShadows").GetValue().Set(0);
            sprite.Get("m_DynamicOccludee").GetValue().Set(1);
            sprite.Get("m_MotionVectors").GetValue().Set(1);
            sprite.Get("m_LightProbeUsage").GetValue().Set(1);
            sprite.Get("m_ReflectionProbeUsage").GetValue().Set(1);
            sprite.Get("m_RayTracingMode").GetValue().Set(0);
            sprite.Get("m_RenderingLayerMask").GetValue().Set(1);
            sprite.Get("m_RendererPriority").GetValue().Set(0);
            sprite.Get("m_LightmapIndex").GetValue().Set(65535);
            sprite.Get("m_LightmapIndexDynamic").GetValue().Set(65535);
            sprite.Get("m_LightmapTilingOffset").Get("x").GetValue().Set(1);
            sprite.Get("m_LightmapTilingOffset").Get("y").GetValue().Set(1);
            sprite.Get("m_LightmapTilingOffset").Get("z").GetValue().Set(0);
            sprite.Get("m_LightmapTilingOffset").Get("w").GetValue().Set(0);
            sprite.Get("m_LightmapTilingOffsetDynamic").Get("x").GetValue().Set(1);
            sprite.Get("m_LightmapTilingOffsetDynamic").Get("y").GetValue().Set(1);
            sprite.Get("m_LightmapTilingOffsetDynamic").Get("z").GetValue().Set(0);
            sprite.Get("m_LightmapTilingOffsetDynamic").Get("w").GetValue().Set(0);

            AssetTypeValueField mat = ValueBuilder.DefaultValueFieldFromArrayTemplate(sprite.Get("m_Materials").Get("Array"));
            mat.Get("m_FileID").GetValue().Set(1);
            mat.Get("m_PathID").GetValue().Set(2);
            sprite.Get("m_Materials").Get("Array").SetChildrenList(new AssetTypeValueField[] { mat });

            sprite.Get("m_StaticBatchInfo").Get("firstSubMesh").GetValue().Set(0);
            sprite.Get("m_StaticBatchInfo").Get("subMeshCount").GetValue().Set(0);
            sprite.Get("m_StaticBatchRoot").Get("m_FileID").GetValue().Set(0);
            sprite.Get("m_StaticBatchRoot").Get("m_PathID").GetValue().Set(0);
            sprite.Get("m_ProbeAnchor").Get("m_FileID").GetValue().Set(0);
            sprite.Get("m_ProbeAnchor").Get("m_PathID").GetValue().Set(0);
            sprite.Get("m_LightProbeVolumeOverride").Get("m_FileID").GetValue().Set(0);
            sprite.Get("m_LightProbeVolumeOverride").Get("m_PathID").GetValue().Set(0);
            sprite.Get("m_SortingLayerID").GetValue().Set(0);
            sprite.Get("m_SortingLayer").GetValue().Set(0);
            sprite.Get("m_SortingOrder").GetValue().Set(0);
            sprite.Get("m_Sprite").Get("m_FileID").GetValue().Set(0);
            sprite.Get("m_Sprite").Get("m_PathID").GetValue().Set(spriteIndex);
            sprite.Get("m_Color").Get("r").GetValue().Set(1);
            sprite.Get("m_Color").Get("g").GetValue().Set(1);
            sprite.Get("m_Color").Get("b").GetValue().Set(1);
            sprite.Get("m_Color").Get("a").GetValue().Set(1);
            sprite.Get("m_FlipX").GetValue().Set(false);
            sprite.Get("m_FlipY").GetValue().Set(false);
            sprite.Get("m_DrawMode").GetValue().Set(0);
            sprite.Get("m_Size").Get("x").GetValue().Set(1.57);
            sprite.Get("m_Size").Get("y").GetValue().Set(1.11);
            sprite.Get("m_AdaptiveModeThreshold").GetValue().Set(0.5);
            sprite.Get("m_SpriteTileMode").GetValue().Set(0);
            sprite.Get("m_WasSpriteAssigned").GetValue().Set(true);
            sprite.Get("m_MaskInteraction").GetValue().Set(0);
            sprite.Get("m_SpriteSortPoint").GetValue().Set(0);
            sprite.Get("m_GameObject").Get("m_FileID").GetValue().Set(0);
            sprite.Get("m_GameObject").Get("m_PathID").GetValue().Set(parentIndex);

            UnityAsset unityAsset = new UnityAsset();
            unityAsset.data = sprite;
            unityAsset.classID = UnityClass.SpriteRenderer;
            unityAsset.index = id;

            return unityAsset;
        }
        public UnityAsset CreateColliderComponent(long parentIndex, Collider colliderData, long id)
        {
            AssetTypeValueField collider = GetDefaultValueField(UnityClass.SpriteRenderer);

            collider.Get("m_Enabled").GetValue().Set(1);
            collider.Get("m_Density").GetValue().Set(1);
            collider.Get("m_Material").Get("m_FileID").GetValue().Set(0);
            collider.Get("m_Material").Get("m_PathID").GetValue().Set(0);
            collider.Get("m_IsTrigger").GetValue().Set(false);
            collider.Get("m_UsedByEffector").GetValue().Set(false);
            collider.Get("m_UsedByComposite").GetValue().Set(false);
            collider.Get("m_Offset").Get("x").GetValue().Set(0);
            collider.Get("m_Offset").Get("y").GetValue().Set(0);
            collider.Get("m_EdgeRadius").GetValue().Set(0);
            collider.Get("m_GameObject").Get("m_FileID").GetValue().Set(0);
            collider.Get("m_GameObject").Get("m_PathID").GetValue().Set(parentIndex);

            AssetTypeValueField colliderPoints = collider.Get("m_Points").Get("Array");
            List<AssetTypeValueField> points = new List<AssetTypeValueField>();
            foreach (ColliderPoint p in colliderData.points)
            {
                AssetTypeValueField newPoint = ValueBuilder.DefaultValueFieldFromArrayTemplate(colliderPoints);
                newPoint.Get("x").GetValue().Set(p.x);
                newPoint.Get("y").GetValue().Set(p.y);
                points.Add(newPoint);
            }
            colliderPoints.SetChildrenList(points.ToArray());

            UnityAsset unityAsset = new UnityAsset();
            unityAsset.data = collider;
            unityAsset.classID = UnityClass.EdgeCollider2D;
            unityAsset.index = id;

            return unityAsset;
        }
        public UnityAsset CreateSprite(long texture2dID, int width, int height, long id)
        {
            AssetTypeValueField sprite = GetDefaultValueField(UnityClass.Sprite);

            sprite.Get("m_Name").GetValue().Set("Custom Sprite");
            sprite.Get("m_Rect").Get("x").GetValue().Set(0);
            sprite.Get("m_Rect").Get("y").GetValue().Set(0);
            sprite.Get("m_Rect").Get("width").GetValue().Set(width);
            sprite.Get("m_Rect").Get("height").GetValue().Set(height);
            sprite.Get("m_Offset").Get("x").GetValue().Set(0);
            sprite.Get("m_Offset").Get("y").GetValue().Set(0);
            sprite.Get("m_Border").Get("x").GetValue().Set(0);
            sprite.Get("m_Border").Get("y").GetValue().Set(0);
            sprite.Get("m_Border").Get("z").GetValue().Set(0);
            sprite.Get("m_Border").Get("w").GetValue().Set(0);
            sprite.Get("m_PixelsToUnits").GetValue().Set(100);
            sprite.Get("m_Pivot").Get("x").GetValue().Set(0.5);
            sprite.Get("m_Pivot").Get("y").GetValue().Set(0.5);
            sprite.Get("m_Extrude").GetValue().Set(1);
            sprite.Get("m_IsPolygon").GetValue().Set(false);
            sprite.Get("m_SpriteAtlas").Get("m_FileID").GetValue().Set(0);
            sprite.Get("m_SpriteAtlas").Get("m_PathID").GetValue().Set(0);
            sprite.Get("m_RenderDataKey").Get("second").GetValue().Set(21300000);

            // Render Data (RD)
            AssetTypeValueField RD = sprite.Get("m_RD");
            RD.Get("texture").Get("m_FileID").GetValue().Set(0);
            RD.Get("texture").Get("m_PathID").GetValue().Set(texture2dID);
            RD.Get("alphaTexture").Get("m_FileID").GetValue().Set(0);
            RD.Get("alphaTexture").Get("m_PathID").GetValue().Set(0);

            // m_RD > m_IndexBuffer
            int[] tightMesh = new int[] { 0, 0, 1, 0, 2, 0, 2, 0, 1, 0, 3, 0 };
            List<AssetTypeValueField> indexBufferArr = new List<AssetTypeValueField>();
            foreach (int num in tightMesh)
            {
                AssetTypeValueField dataNum = ValueBuilder.DefaultValueFieldFromArrayTemplate(RD.Get("m_IndexBuffer").Get("Array"));
                dataNum.GetValue().Set(num);
                indexBufferArr.Add(dataNum);
            }
            RD.Get("m_IndexBuffer").Get("Array").SetChildrenList(indexBufferArr.ToArray());

            // m_RD > m_SubMeshes
            AssetTypeValueField newMeshChild = ValueBuilder.DefaultValueFieldFromArrayTemplate(RD.Get("m_SubMeshes").Get("Array"));
            newMeshChild.Get("indexCount").GetValue().Set(6);
            newMeshChild.Get("vertexCount").GetValue().Set(4);
            RD.Get("m_SubMeshes").Get("Array").SetChildrenList(new[] { newMeshChild });

            // m_RD > m_VertexData > m_Channels
            RD.Get("m_VertexData").Get("m_VertexCount").GetValue().Set(4);
            AssetTypeValueField arr = RD.Get("m_VertexData").Get("m_Channels").Get("Array");
            List<AssetTypeValueField> newChildren = new List<AssetTypeValueField>();
            for (var i = 0; i < 14; i++)
            {
                newChildren.Add(ValueBuilder.DefaultValueFieldFromArrayTemplate(arr));
            }
            arr.SetChildrenList(newChildren.ToArray());
            RD.Get("m_VertexData").Get("m_Channels").Get("Array")[0].Get("dimension").GetValue().Set(3);
            RD.Get("m_VertexData").Get("m_Channels").Get("Array")[4].Get("stream").GetValue().Set(1);
            RD.Get("m_VertexData").Get("m_Channels").Get("Array")[4].Get("dimension").GetValue().Set(2);

            // m_RD > m_VertexData > m_DataSize
            float wScale = width / 175;
            float hScale = height / 175;
            float[] offsetVertex = new float[] { -wScale, hScale, 0, wScale, hScale, 0, -wScale, -hScale, 0, wScale, -hScale };

            List<AssetTypeValueField> vertexArr = new List<AssetTypeValueField>();

            foreach (float num in offsetVertex)
            {
                byte[] buffer = BitConverter.GetBytes(num);

                foreach (byte data in buffer)
                {
                    AssetTypeValueField dataNum = ValueBuilder.DefaultValueFieldFromArrayTemplate(RD.Get("m_VertexData").Get("m_DataSize"));
                    dataNum.GetValue().Set(data);
                    vertexArr.Add(dataNum);
                }
            }
            while (vertexArr.Count < 80)
            {
                AssetTypeValueField dataNum = ValueBuilder.DefaultValueFieldFromArrayTemplate(RD.Get("m_VertexData").Get("m_DataSize"));
                dataNum.GetValue().Set(0);
                vertexArr.Add(dataNum);
            }
            RD.Get("m_VertexData").Get("m_DataSize").SetChildrenList(vertexArr.ToArray());

            RD.Get("textureRect").Get("x").GetValue().Set(0);
            RD.Get("textureRect").Get("y").GetValue().Set(0);
            RD.Get("textureRect").Get("width").GetValue().Set(width);
            RD.Get("textureRect").Get("height").GetValue().Set(height);
            RD.Get("textureRectOffset").Get("x").GetValue().Set(0);
            RD.Get("textureRectOffset").Get("y").GetValue().Set(0);
            RD.Get("atlasRectOffset").Get("x").GetValue().Set(-1);
            RD.Get("atlasRectOffset").Get("y").GetValue().Set(-1);
            RD.Get("settingsRaw").GetValue().Set(0); // 64
            RD.Get("uvTransform").Get("x").GetValue().Set(100);
            RD.Get("uvTransform").Get("y").GetValue().Set(width / 2);
            RD.Get("uvTransform").Get("z").GetValue().Set(100);
            RD.Get("uvTransform").Get("w").GetValue().Set(height / 2);
            RD.Get("downscaleMultiplier").GetValue().Set(1);

            AssetTypeValueField physArr = ValueBuilder.DefaultValueFieldFromArrayTemplate(sprite.Get("m_PhysicsShape").Get("Array"));
            AssetTypeValueField physSubArrA = ValueBuilder.DefaultValueFieldFromArrayTemplate(physArr.Get("Array"));
            AssetTypeValueField physSubArrB = ValueBuilder.DefaultValueFieldFromArrayTemplate(physArr.Get("Array"));
            AssetTypeValueField physSubArrC = ValueBuilder.DefaultValueFieldFromArrayTemplate(physArr.Get("Array"));
            AssetTypeValueField physSubArrD = ValueBuilder.DefaultValueFieldFromArrayTemplate(physArr.Get("Array"));
            physSubArrA.Get("x").GetValue().Set(-0.5);
            physSubArrA.Get("y").GetValue().Set(0.5);
            physSubArrB.Get("x").GetValue().Set(-0.5);
            physSubArrB.Get("y").GetValue().Set(-0.5);
            physSubArrC.Get("x").GetValue().Set(0.5);
            physSubArrC.Get("y").GetValue().Set(-0.5);
            physSubArrD.Get("x").GetValue().Set(0.5);
            physSubArrD.Get("y").GetValue().Set(0.5);
            physArr.Get("Array").SetChildrenList(new[] { physSubArrA, physSubArrB, physSubArrC, physSubArrD });
            sprite.Get("m_PhysicsShape").Get("Array").SetChildrenList(new[] { physArr });

            UnityAsset unityAsset = new UnityAsset();
            unityAsset.data = sprite;
            unityAsset.classID = UnityClass.Sprite;
            unityAsset.index = id;

            return unityAsset;
        }
        public UnityAsset CreateTexture2D(int width, int height, int dataOffset, int dataLength, string path, long id)
        {
            AssetTypeValueField baseField = GetDefaultValueField(UnityClass.Texture2D);

            baseField.Get("m_Name").GetValue().Set("Custom Texture");
            baseField.Get("m_ForcedFallbackFormat").GetValue().Set(TextureFormat.RGBA32);
            baseField.Get("m_DownscaleFallback").GetValue().Set(false);
            baseField.Get("m_Width").GetValue().Set(width);
            baseField.Get("m_Height").GetValue().Set(height);
            baseField.Get("m_CompleteImageSize").GetValue().Set(dataLength);
            baseField.Get("m_TextureFormat").GetValue().Set(TextureFormat.RGBA32);
            baseField.Get("m_MipCount").GetValue().Set(1);
            baseField.Get("m_IsReadable").GetValue().Set(false);
            baseField.Get("m_IgnoreMasterTextureLimit").GetValue().Set(false);
            baseField.Get("m_StreamingMipmaps").GetValue().Set(false);
            baseField.Get("m_StreamingMipmapsPriority").GetValue().Set(0);
            baseField.Get("m_ImageCount").GetValue().Set(1);
            baseField.Get("m_TextureDimension").GetValue().Set(2);

            baseField.Get("m_TextureSettings").Get("m_FilterMode").GetValue().Set(1);
            baseField.Get("m_TextureSettings").Get("m_Aniso").GetValue().Set(1);
            baseField.Get("m_TextureSettings").Get("m_MipBias").GetValue().Set(0);
            baseField.Get("m_TextureSettings").Get("m_WrapU").GetValue().Set(1);
            baseField.Get("m_TextureSettings").Get("m_WrapV").GetValue().Set(1);
            baseField.Get("m_TextureSettings").Get("m_WrapW").GetValue().Set(1);

            baseField.Get("m_LightmapFormat").GetValue().Set(6);
            baseField.Get("m_ColorSpace").GetValue().Set(1);

            baseField.Get("m_StreamData").Get("offset").GetValue().Set(dataOffset);
            baseField.Get("m_StreamData").Get("size").GetValue().Set(dataLength);
            baseField.Get("m_StreamData").Get("path").GetValue().Set(path);

            UnityAsset unityAsset = new UnityAsset();
            unityAsset.data = baseField;
            unityAsset.classID = UnityClass.Texture2D;
            unityAsset.index = id;

            return unityAsset;
        }
    }
}
