using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelImposter
{
    // TODO Complete this List
    // 54 out of 318 Unity Classes Listed
    // https://docs.unity3d.com/Manual/ClassIDReference.html

    enum UnityClass : uint
    {
        // Unity
        Object,
        GameObject,
        Component,
        LevelGameManager,
        Transform,
        TimeManager,
        GlobalGameManager,
        Behavior = 8,
        GameManager,
        AudioManager = 11,
        InputManager = 13,
        EditorExtension = 18,
        Physics2DSettings,
        Camera,
        Material,
        MeshRenderer = 23,
        Renderer = 25,
        Texture = 27,
        Texture2D,
        OcclusionCullingSettings,
        GraphicsSettings,
        MeshFilter = 33,
        OcclusionPortal = 41,
        Mesh = 43,
        Skybox = 45,
        QualitySettings = 47,
        Shader,
        TextAsset,
        Rigidbody2D,
        Collider2D = 53,
        Rigidbody,
        PhysicsManager,
        Collider,
        Joint,
        CircleCollider2D,
        HingeJoint,
        PolygonCollider2D,
        BoxCollider2D,
        PhysicsMaterial2D,
        MeshCollider = 64,
        BoxCollider,
        CompositeCollider2D,
        EdgeCollider2D = 68,
        CapsuleCollider2D = 70,
        ComputeShader = 72,
        AnimationClip = 74,
        ConstantForce,
        TagManager = 78,
        AudioListener = 81,
        AudioSource,
        AudioClip,
        // ...
        PlayerSettings = 129,
        SpriteRenderer = 212,
        Sprite,
        
        // Internal Usage
        CustomAsset = Object       
    }
}
