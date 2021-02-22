using AssetsTools.NET;
using AssetsTools.NET.Extra;
using LevelImposter.Unity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LevelImposter
{
    class MapWriter
    {
        public UnityManager unityMgr;
        public UnityAsset polus;
        public UnityAsset polusComp;

        public MapWriter()
        {
            unityMgr = new UnityManager("sharedassets0.assets");
            polus = unityMgr.GetAsset("PlanetMap", UnityClass.GameObject);
            polusComp = GetComponents(polus);
        }

        public void AddAsset(MapAsset asset)
        {
            switch ((UnityClass)asset.unityType)
            {
                case UnityClass.Sprite:
                    string assetNameA = AssetDictionary.Get(asset.type);
                    long spriteIDA = unityMgr.GetAsset(assetNameA, (UnityClass)asset.unityType).index;
                    AddAsset(asset, spriteIDA);
                    break;
                case UnityClass.GameObject:
                    string assetNameB = AssetDictionary.Get(asset.type);
                    UnityAsset assetB = unityMgr.GetAsset(assetNameB, UnityClass.GameObject);
                    EditAsset(asset, assetB);
                    break;
                case UnityClass.CustomAsset:
                    long spriteIDB = TextureWriter.AddTexture(unityMgr, asset.curl);
                    AddAsset(asset, spriteIDB);
                    break;
            }
        }

        public void ClearPolus()
        {
            foreach (AssetTypeValueField child in polusComp.data.Get("m_Children").Get("Array").GetChildrenList())
            {
                UnityAsset asset = unityMgr.GetAsset(child.Get("m_PathID").GetValue().AsInt64());
                asset.data.Get("m_LocalScale").Get("x").GetValue().Set(0);
                asset.data.Get("m_LocalScale").Get("z").GetValue().Set(0);
                asset.data.Get("m_LocalScale").Get("y").GetValue().Set(0);
                asset.data.Get("m_LocalPosition").Get("x").GetValue().Set(0);
                asset.data.Get("m_LocalPosition").Get("y").GetValue().Set(0);
                asset.data.Get("m_LocalPosition").Get("z").GetValue().Set(0);

                unityMgr.Save(asset);
            }
        }

        public void EditAsset(MapAsset asset, UnityAsset edit)
        {
            // Get and Clear Child's Father
            UnityAsset comp = GetComponents(edit);

            long fatherID = comp.data.Get("m_Father").Get("m_PathID").GetValue().AsInt64();
            comp.data.Get("m_Father").Get("m_PathID").GetValue().Set(16553);
            UnityAsset father = unityMgr.GetAsset(fatherID);

            // Get and Clear Father's Child
            AssetTypeValueField[] fatherChildren = father.data.Get("m_Children").Get("Array").GetChildrenList();
            AssetTypeValueField[] newChildren = fatherChildren.Where(child => child.Get("m_PathID").GetValue().AsInt64() != comp.index).ToArray();
            father.data.Get("m_Children").Get("Array").SetChildrenList(newChildren);

            unityMgr.Save(father);

            // Get and Set New Father
            AssetTypeValueField newChild = ValueBuilder.DefaultValueFieldFromArrayTemplate(polusComp.data.Get("m_Children").Get("Array"));
            newChild.Get("m_FileID").GetValue().Set(0);
            newChild.Get("m_PathID").GetValue().Set(comp.index);
            polusComp.data.Get("m_Children").Get("Array").SetChildrenList(polusComp.data.Get("m_Children").Get("Array").children.Concat(new List<AssetTypeValueField>() { newChild }).ToArray());

            // Set Transform
            comp.data.Get("m_LocalPosition").Get("x").GetValue().Set(asset.x);
            comp.data.Get("m_LocalPosition").Get("y").GetValue().Set(asset.y);
            comp.data.Get("m_LocalPosition").Get("z").GetValue().Set(asset.z);
            comp.data.Get("m_LocalScale").Get("x").GetValue().Set(asset.xScale);
            comp.data.Get("m_LocalScale").Get("y").GetValue().Set(asset.yScale);
            comp.data.Get("m_LocalScale").Get("z").GetValue().Set(1);
            comp.data.Get("m_LocalRotation").Get("z").GetValue().Set(asset.zRotate);
            comp.data.Get("m_LocalRotation").Get("w").GetValue().Set(1);

            unityMgr.Save(comp);
        }

        public void AddAsset(MapAsset asset, long spriteID)
        {
            long spriteRendererId = unityMgr.NextAssetId();
            long transformId = unityMgr.NextAssetId();
            long gameObjectId = unityMgr.NextAssetId();

            AssetGenerator generator = new AssetGenerator(unityMgr);
            UnityAsset gameObject   = generator.CreateGameObject("Custom Map Asset", gameObjectId);
            UnityAsset transform = generator.CreateTransformComponent(gameObjectId, asset, transformId);
            UnityAsset spriteRender = generator.CreateSpriteRendererComponent(gameObjectId, spriteID, spriteRendererId);

            unityMgr.Save(spriteRender);
            unityMgr.Save(transform);

            AddComponentReference(gameObject, transformId);
            AddComponentReference(gameObject, spriteRendererId);

            AssetTypeValueField gameObjectRef = ValueBuilder.DefaultValueFieldFromArrayTemplate(polusComp.data.Get("m_Children").Get("Array"));
            gameObjectRef.Get("m_PathID").GetValue().Set(transformId);
            AddChild(polusComp.data.Get("m_Children").Get("Array"), gameObjectRef);

            unityMgr.Save(gameObject);
        }

        public void Export()
        {
            unityMgr.Save(polusComp);
            unityMgr.Export();
            TextureWriter.Export(unityMgr);
        }

        private void AddComponentReference(UnityAsset parent, long componentId)
        {
            AssetTypeValueField components = parent.data.Get("m_Component").Get("Array");
            AssetTypeValueField reference = ValueBuilder.DefaultValueFieldFromArrayTemplate(components.GetTemplateField()).Get("component");
            reference.Get("m_PathID").GetValue().Set(componentId);
            AddChild(components, reference);
        }
        private void AddChild(AssetTypeValueField parent, AssetTypeValueField child)
        {
            parent.SetChildrenList(parent.children.Concat(new List<AssetTypeValueField>() { child }).ToArray());
        }
        private UnityAsset GetComponents(UnityAsset asset)
        {
            AssetTypeValueField componentArray = asset.data.Get("m_Component").Get("Array");
            AssetTypeValueField transformRef = componentArray[0].Get("component");
            AssetExternal assetExternal = unityMgr.assetsManager.GetExtAsset(unityMgr.assetsFileInstance, transformRef);

            UnityAsset unityAsset = new UnityAsset();
            unityAsset.data = assetExternal.instance.GetBaseField();
            unityAsset.classID = (UnityClass)assetExternal.info.curFileType;
            unityAsset.index = assetExternal.info.index;

            return unityAsset;
        }
    }
}
