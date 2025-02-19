﻿#define SHOW_CONVERTER_ON_STARTUP
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System;
using System.Text;
using System.Reflection;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

#if false
namespace UnityEditor.Rendering.Toon
{
    [Serializable]
    internal struct ConverterItems
    {
        public List<ConverterItemDescriptor> itemDescriptors;
    }

    [Serializable]
//    [InitializeOnLoad]
    internal class UTS3ConverterWindow : EditorWindow
    {

        internal class UTS2GUID
        {
            public UTS2GUID(string guid, string shaderName)
            {
                m_ShaderName = shaderName;
                m_Guid = guid;
            }
            internal string m_ShaderName;
            internal string m_Guid;
        }
        static readonly UTS2GUID[] stdShaders =
        {
            new UTS2GUID(  "9baf30ce95c751649b14d96da3a4b4d5", "Toon_DoubleShadeWithFeather"),
            new UTS2GUID(  "96d4d9f975e6c8849bd1a5c06acfae84", "ToonColor_DoubleShadeWithFeather"),
            new UTS2GUID(  "ccd13b7f8710b264ea8bd3bc4f51f9e4", "ToonColor_DoubleShadeWithFeather_Clipping"),
            new UTS2GUID(  "9c3978743d5db18448a8b945c723a6eb", "ToonColor_DoubleShadeWithFeather_Clipping_StencilMask"),
            new UTS2GUID(  "d7da29588857e774bb0650f1fae494c6", "ToonColor_DoubleShadeWithFeather_Clipping_StencilOut"),
            new UTS2GUID(  "315897103223dab42a0746aa65ec251a", "ToonColor_DoubleShadeWithFeather_StencilMask"),
            new UTS2GUID(  "2e5cc2da6af713844956264245e092e4", "ToonColor_DoubleShadeWithFeather_StencilOut"),
            new UTS2GUID(  "369d674ae1ba36249bb00e2f73b0cd10", "ToonColor_DoubleShadeWithFeather_TransClipping"),
            new UTS2GUID(  "8600b2bec3ae31145afa80084df20c61", "ToonColor_DoubleShadeWithFeather_TransClipping_StencilMask"),
            new UTS2GUID(  "43d0eeb4c46f52841b0941e99ac9b16b", "ToonColor_DoubleShadeWithFeather_TransClipping_StencilOut"),
            new UTS2GUID(  "97b7edb5fc0f5744c9b264c2224a0b1e", "ToonColor_DoubleShadeWithFeather_Transparent"),
            new UTS2GUID(  "3b20fc0febd34f94baf0304bf47841d8", "ToonColor_DoubleShadeWithFeather_Transparent_StencilOut"),
            new UTS2GUID(  "af8454e09b3a41448a4140e792059446", "ToonColor_ShadingGradeMap"),
            new UTS2GUID(  "295fec4a7029edd4eb9522bef07f41ce", "ToonColor_ShadingGradeMap_AngelRing"),
            new UTS2GUID(  "e32270aa38f4b664b90f04cc475fdb81", "ToonColor_ShadingGradeMap_AngelRing_StencilOut"),
            new UTS2GUID(  "29a860a3f3c4cec43ab821338e28eac8", "ToonColor_ShadingGradeMap_AngelRing_TransClipping"),
            new UTS2GUID(  "d5d9c1f4718235248ad37448b0c74c68", "ToonColor_ShadingGradeMap_AngelRing_TransClipping_StencilOut"),
            new UTS2GUID(  "6439813c08a1f8947bb0ca6599499dd7", "ToonColor_ShadingGradeMap_StencilMask"),
            new UTS2GUID(  "b39692f1382224b4cbe21c12ae51c639", "ToonColor_ShadingGradeMap_StencilOut"),
            new UTS2GUID(  "cd7e85b59edbb7740841003baeb510b5", "ToonColor_ShadingGradeMap_TransClipping"),
            new UTS2GUID(  "6b4b6d07944415f44b1fc2f0fc24535f", "ToonColor_ShadingGradeMap_TransClipping_StencilMask"),
            new UTS2GUID(  "31c75b34739dfc64fb57bf49005e942d", "ToonColor_ShadingGradeMap_TransClipping_StencilOut"),
            new UTS2GUID(  "7737ca8c4e3939f4086a6e08f93c2ebd", "ToonColor_ShadingGradeMap_Transparent"),
            new UTS2GUID(  "be27d4be45de7dd4ab2e69c992876edb", "ToonColor_ShadingGradeMap_Transparent_StencilOut"),
            new UTS2GUID(  "345def18d0906d544b7d12b050937392", "Toon_DoubleShadeWithFeather_Clipping"),
            new UTS2GUID(  "7a735f9b121d96349b6da0a077299424", "Toon_DoubleShadeWithFeather_Clipping_StencilMask"),
            new UTS2GUID(  "ed7fba947f3bccb4cbc78f55d7a56a70", "Toon_DoubleShadeWithFeather_Clipping_StencilOut"),
//            new UTS2GUID(  "1d10c7840eb6ba74c889a27f14ba6081", "Toon_DoubleShadeWithFeather_Mobile"),
//            new UTS2GUID(  "88791c14394118d42a5e176b433af322", "Toon_DoubleShadeWithFeather_Mobile_Clipping"),
//            new UTS2GUID(  "41f4ee183cb66ad40bc74a9f8f944974", "Toon_DoubleShadeWithFeather_Mobile_Clipping_StencilMask"),
//            new UTS2GUID(  "dec01cbdbc5b8da4ca8671815cda1557", "Toon_DoubleShadeWithFeather_Mobile_StencilMask"),
//            new UTS2GUID(  "55e8b9eeaaff205469365133fe7bc744", "Toon_DoubleShadeWithFeather_Mobile_StencilOut"),
//            new UTS2GUID(  "d4c592285a93c3844aafdaafffc07ec7", "Toon_DoubleShadeWithFeather_Mobile_TransClipping"),
//            new UTS2GUID(  "100d373b596f44d49ac9bb944d671d32", "Toon_DoubleShadeWithFeather_Mobile_TransClipping_StencilMask"),
            new UTS2GUID(  "036bc90bfe3475b4c9fadb85d0520621", "Toon_DoubleShadeWithFeather_StencilMask"),
            new UTS2GUID(  "0a1e4c9dcc0e9ea4db38ae9cb5059608", "Toon_DoubleShadeWithFeather_StencilOut"),
            new UTS2GUID(  "e8e7d781c3155254b9ea8956c5bd1218", "Toon_DoubleShadeWithFeather_TransClipping"),
            new UTS2GUID(  "79add09e32e5c4541980118f6c4045b6", "Toon_DoubleShadeWithFeather_TransClipping_StencilMask"),
            new UTS2GUID(  "fb47be5a840097b45bac228446468ef3", "Toon_DoubleShadeWithFeather_TransClipping_StencilOut"),
            new UTS2GUID(  "42a47eda2ed77084c9136507eadb8641", "Toon_OutlineObject"),
            new UTS2GUID(  "2e2edd12fbf6bcb4ea1f34c17ee42df5", "Toon_OutlineObject_StencilOut"),
            new UTS2GUID(  "ca035891872022e4f80c952b3916e450", "Toon_ShadingGradeMap"),
            new UTS2GUID(  "9aadc53d7cdc63f4898ea042aa9d853b", "Toon_ShadingGradeMap_AngelRing"),
            new UTS2GUID(  "23e399973d807464fb195291a44a614c", "Toon_ShadingGradeMap_AngelRing_Mobile"),
            new UTS2GUID(  "8d33e4e4084e5af449f3e762fecce3c9", "Toon_ShadingGradeMap_AngelRing_Mobile_StencilOut"),
            new UTS2GUID(  "415f07ab6fd766048ac6f8c2f2b406a9", "Toon_ShadingGradeMap_AngelRing_StencilOut"),
            new UTS2GUID(  "b2a70923168ea0c40a3051a013c93a8a", "Toon_ShadingGradeMap_AngelRing_TransClipping"),
            new UTS2GUID(  "d1e11a558d143f14c864edf263332764", "Toon_ShadingGradeMap_AngelRing_TransClipping_StencilOut"),
            new UTS2GUID(  "f90e11a40dcf4f745ae6b21b857943fa", "Toon_ShadingGradeMap_Mobile"),
            new UTS2GUID(  "206c554c8b0c60041a9d242385f543d3", "Toon_ShadingGradeMap_Mobile_StencilMask"),
            new UTS2GUID(  "cfc201757f2519c4bb6ef9265a046582", "Toon_ShadingGradeMap_Mobile_StencilOut"),
            new UTS2GUID(  "cce1da34c52aff745adf0222f56a356c", "Toon_ShadingGradeMap_Mobile_TransClipping"),
            new UTS2GUID(  "e88039bab21b7894e918126e8fce5d1b", "Toon_ShadingGradeMap_Mobile_TransClipping_StencilMask"),
            new UTS2GUID(  "aa2e05ed58ca15441bd0989f008da78b", "Toon_ShadingGradeMap_StencilMask"),
            new UTS2GUID(  "923058fda1b61544b93d91eeee772086", "Toon_ShadingGradeMap_StencilOut"),
            new UTS2GUID(  "aebd33b74ef849a4882b4a8d55f0f0c9", "Toon_ShadingGradeMap_TransClipping"),
            new UTS2GUID(  "0a05dd221bacbb448afac3d63e6bd833", "Toon_ShadingGradeMap_TransClipping_StencilMask"),
            new UTS2GUID(  "67212ac11ff43b04a833d3986b997a9f", "Toon_ShadingGradeMap_TransClipping_StencilOut"),

        };
        static readonly UTS2GUID[] tessShaders =
        {
            new UTS2GUID(  "5b8a1502578ed764c9880a7be65c9672", "ToonColor_DoubleShadeWithFeather_Clipping_Tess"),
            new UTS2GUID(  "682e6e6cf60a51040ade19437a3f53e2", "ToonColor_DoubleShadeWithFeather_Clipping_Tess_StencilMask"),
            new UTS2GUID(  "148d1eca2cf299e4eb949d15c4cf95ee", "ToonColor_DoubleShadeWithFeather_Clipping_Tess_StencilOut"),
            new UTS2GUID(  "e987cf9cca0941042aa68d1dd51ee20f", "ToonColor_DoubleShadeWithFeather_Tess"),
            new UTS2GUID(  "97df86a7afe06ef41b2a2c242b10593e", "ToonColor_DoubleShadeWithFeather_Tess_StencilMask"),
            new UTS2GUID(  "b179fb8a87955a347b5f594a18b43475", "ToonColor_DoubleShadeWithFeather_Tess_StencilOut"),
            new UTS2GUID(  "60fe384b76fb67d40bc7e38411073dd6", "ToonColor_DoubleShadeWithFeather_TransClipping_Tess"),
            new UTS2GUID(  "4a20b66d106d3f5409f759b5193ecdc2", "ToonColor_DoubleShadeWithFeather_TransClipping_Tess_StencilMask"),
            new UTS2GUID(  "a7842aa9522c7584cae2169b8e1ddb86", "ToonColor_DoubleShadeWithFeather_TransClipping_Tess_StencilOut"),
            new UTS2GUID(  "0cb6c9e6216a91e4a9d38cd2acb4ccb6", "ToonColor_DoubleShadeWithFeather_Transparent_Tess"),
            new UTS2GUID(  "f28bba8b2f259bb40b697d91849c8794", "ToonColor_DoubleShadeWithFeather_Transparent_Tess_StencilOut"),
            new UTS2GUID(  "4876871966ca2344793e439d7391d7b0", "ToonColor_ShadingGradeMap_AngelRing_Tess"),
            new UTS2GUID(  "7c48bdc9fed28c14b8ad0748673b1369", "ToonColor_ShadingGradeMap_AngelRing_Tess_StencilOut"),
            new UTS2GUID(  "d3fb22770ec830b43bdb5ccb973e6f76", "ToonColor_ShadingGradeMap_AngelRing_Tess_TransClipping"),
            new UTS2GUID(  "11e8f1e181e558a47a387492d3ecdb88", "ToonColor_ShadingGradeMap_AngelRing_TransClipping_Tess_StencilOut"),
            new UTS2GUID(  "01494e58d87212f44ab51d29caea84e4", "ToonColor_ShadingGradeMap_Tess"),
            new UTS2GUID(  "24c20b8ed5be113499b40f4e3b6b03e6", "ToonColor_ShadingGradeMap_Tess_StencilMask"),
            new UTS2GUID(  "9cf7e8eb46e9128438d50adf7a841de6", "ToonColor_ShadingGradeMap_Tess_StencilOut"),
            new UTS2GUID(  "3c39a77fda28b5043a7a17c7877cf7b2", "ToonColor_ShadingGradeMap_TransClipping_Tess"),
            new UTS2GUID(  "bf840a439c33c8b4a99d52e6c3d8511f", "ToonColor_ShadingGradeMap_TransClipping_Tess_StencilMask"),
            new UTS2GUID(  "8eff803eae89c994fae3acf2f686fafa", "ToonColor_ShadingGradeMap_TransClipping_Tess_StencilOut"),
            new UTS2GUID(  "0959cb8822a344c4da890457e668fdc9", "ToonColor_ShadingGradeMap_Transparent_Tess"),
            new UTS2GUID(  "6d115cf94d14d1842a56dfff76b57f42", "ToonColor_ShadingGradeMap_Transparent_Tess_StencilOut"),
            new UTS2GUID(  "f0b2fc9b8a189134da9c7d24f361caf4", "Toon_DoubleShadeWithFeather_Clipping_Tess"),
            new UTS2GUID(  "8c94ee3046ef0574f87f6b658b4e4691", "Toon_DoubleShadeWithFeather_Clipping_Tess_StencilMask"),
            new UTS2GUID(  "c4aed8662ca0f194284f3ab649e66d23", "Toon_DoubleShadeWithFeather_Clipping_Tess_StencilOut"),
            new UTS2GUID(  "1f248db3b28fc5f44aabd7aca618bd1e", "Toon_DoubleShadeWithFeather_Tess"),
            new UTS2GUID(  "a3214384442742648aa664ef0039d397", "Toon_DoubleShadeWithFeather_Tess_Light"),
            new UTS2GUID(  "3073cd2564e4cde45a19c05e0012d22a", "Toon_DoubleShadeWithFeather_Tess_Light_StencilMask"),
            new UTS2GUID(  "7e7690a767a07da4f943439680e70db8", "Toon_DoubleShadeWithFeather_Tess_Light_StencilOut"),
            new UTS2GUID(  "08c65988dc25d9f44b791fcc18fb543a", "Toon_DoubleShadeWithFeather_Tess_StencilMask"),
            new UTS2GUID(  "f937ea4ce96dfbe448afc0fb671198e5", "Toon_DoubleShadeWithFeather_Tess_StencilOut"),
            new UTS2GUID(  "3fb99ac3775edeb4aa9530db5a614c92", "Toon_DoubleShadeWithFeather_TransClipping_Tess"),
            new UTS2GUID(  "9855f226cd8152d4e99085272aceede6", "Toon_DoubleShadeWithFeather_TransClipping_Tess_StencilMask"),
            new UTS2GUID(  "2a0d4af863770404faee6488b86fe3c9", "Toon_DoubleShadeWithFeather_TransClipping_Tess_StencilOut"),
            new UTS2GUID(  "1847c44f729b68e49ba63610abdf9132", "Toon_OutlineObject_Tess"),
            new UTS2GUID(  "06cae78b869a3234bab02eeb52197e1c", "Toon_OutlineObject_Tess_StencilOut"),
            new UTS2GUID(  "3a1af221400a61a4b94bae19aa79da2b", "Toon_ShadingGradeMap_AngelRing_Tess"),
            new UTS2GUID(  "a1449ab672051624ca3160737b630f5e", "Toon_ShadingGradeMap_AngelRing_Tess_Light"),
            new UTS2GUID(  "79d3dc54c32b69b42be17c48d33575f2", "Toon_ShadingGradeMap_AngelRing_Tess_Light_StencilOut"),
            new UTS2GUID(  "18c9172cdf36a344f9aca9bbc0e7002d", "Toon_ShadingGradeMap_AngelRing_Tess_StencilOut"),
            new UTS2GUID(  "54a94f776a43a074c8c2d205bb934005", "Toon_ShadingGradeMap_AngelRing_TransClipping_Tess"),
            new UTS2GUID(  "d496a1c70c797ad43836d5bfff575b5f", "Toon_ShadingGradeMap_AngelRing_TransClipping_Tess_StencilOut"),
            new UTS2GUID(  "183ea557143786346b1bfc862ad22636", "Toon_ShadingGradeMap_Tess"),
            new UTS2GUID(  "356dd5af8f0d40e41b647d3d0a0555c1", "Toon_ShadingGradeMap_Tess_Light"),
            new UTS2GUID(  "ffadecfbd9e31f840ba4109fea0f0436", "Toon_ShadingGradeMap_Tess_Light_StencilMask"),
            new UTS2GUID(  "98ac5d198a471494da681b7b8d1e1727", "Toon_ShadingGradeMap_Tess_Light_StencilOut"),
            new UTS2GUID(  "0d799eb857c0e2c45bbdfb2c033d33e6", "Toon_ShadingGradeMap_Tess_StencilMask"),
            new UTS2GUID(  "e667137c8b6fd3d4390fc364b2e5c70b", "Toon_ShadingGradeMap_Tess_StencilOut"),
            new UTS2GUID(  "feba437d8ff93f745a78828529e9a272", "Toon_ShadingGradeMap_TransClipping_Tess"),
            new UTS2GUID(  "8d1395a9f4bfad44d8fddd0f2af19b1e", "Toon_ShadingGradeMap_TransClipping_Tess_StencilMask"),
            new UTS2GUID(  "08c6bb334aed21c4198cf46b71ebca2d", "Toon_ShadingGradeMap_TransClipping_Tess_StencilOut"),

        };

        const string kLegacyShaderFileName = "LegacyToon";
        const string kShaderFileNameExtention = ".shader";


        internal UTS3GUI.CullingMode m_cullingMode;
        internal int _autoRenderQueue = 1;
        internal int _renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
        internal UTS3GUI.UTS_TransparentMode _Transparent_Setting;
        internal int _StencilNo_Setting;


        // for converter
        static int s_materialCount = 0;

        // Status for each row item to say in which state they are in.
        // This will make sure they are showing the correct icon
        [Serializable]
        enum Status
        {
            Pending,
            Warning,
            Error,
            Success
        }

        // This is the serialized class that stores the state of each item in the list of items to convert
        [Serializable]
        class ConverterItemState
        {
            public bool isActive;

            // Message that will be displayed on the icon if warning or failed.
            public string message;

            // Status of the converted item, Pending, Warning, Error or Success
            public Status status;

            internal bool hasConverted = false;
        }

        // Each converter uses the active bool
        // Each converter has a list of active items/assets
        // We do this so that we can use the binding system of the UI Elements
        [Serializable]
        class ConverterState
        {
            // This is the enabled state of the whole converter
            public bool isEnabled;
            public bool isActive;
            public bool isLoading; // to name
            public bool isInitialized;
            public List<ConverterItemState> items = new List<ConverterItemState>();

            public int pending;
            public int warnings;
            public int errors;
            public int success;
            internal int index;

            public bool isActiveAndEnabled => isEnabled && isActive;
            public bool requiresInitialization => !isInitialized && isActiveAndEnabled;
        }

        [Serializable]
        internal struct ConverterItems
        {
            public List<ConverterItemDescriptor> itemDescriptors;
        }

        // these are not included in CoreEditorStyles in 2020.3 and so on.
        /// <summary> Warning icon </summary>
        public static Texture2D iconWarn;
        /// <summary> Help icon </summary>
        public static Texture2D iconHelp;
        /// <summary> Fail icon </summary>
        public static Texture2D iconFail;
        /// <summary> Success icon </summary>
        public static Texture2D iconSuccess;
        /// <summary> Complete icon </summary>
        public static Texture2D iconComplete;
        /// <summary> Pending icon </summary>
        public static Texture2D iconPending;

        Tuple<string, Texture2D> converterStateInfoDisabled;
        Tuple<string, Texture2D> converterStateInfoPendingInitialization;
        Tuple<string, Texture2D> converterStateInfoPendingConversion;
        Tuple<string, Texture2D> converterStateInfoPendingConversionWarning;
        Tuple<string, Texture2D> converterStateInfoCompleteErrors;
        Tuple<string, Texture2D> converterStateInfoComplete;
        [SerializeField]
        internal VisualTreeAsset converterEditorAsset;
        [SerializeField]
        internal VisualTreeAsset converterItem;
        [SerializeField]
        internal VisualTreeAsset converterWidgetMainAsset;

        ScrollView m_ScrollView;
        VisualElement m_ConverterSelectedVE;
        Button m_ConvertButton;
        Button m_InitButton;
        Button m_InitAnConvertButton;
        Button m_ContainerHelpButton;

        bool m_InitAndConvert;

        List<RenderPipelineConverter> m_CoreConvertersList = new List<RenderPipelineConverter>();
        List<VisualElement> m_VEList = new List<VisualElement>();
        // This list needs to be as long as the amount of converters
        List<ConverterItems> m_ItemsToConvert = new List<ConverterItems>();

        //List<List<ConverterItemDescriptor>> m_ItemsToConvert = new List<List<ConverterItemDescriptor>>();
        SerializedObject m_SerializedObject;

        List<string> m_ContainerChoices = new List<string>();
        List<RenderPipelineConverterContainer> m_Containers = new List<RenderPipelineConverterContainer>();
        int m_ContainerChoiceIndex = 0;
        int m_WorkerCount;

        // This is a list of Converter States which holds a list of which converter items/assets are active
        // There is one for each Converter.
        [SerializeField] List<ConverterState> m_ConverterStates = new List<ConverterState>();

        TypeCache.TypeCollection m_ConverterContainers;

        RenderPipelineConverterContainer currentContainer => m_Containers[m_ContainerChoiceIndex];

        Vector2 m_scrollPos;

        bool m_initialzed;
        static string[] s_guids;
        static int s_versionErrorCount = 0;
        const string legacyShaderPrefix = "UnityChanToonShader/";
        readonly string[] m_RendderPipelineNames = { "Built-in", "Universal RP", "HDRP" };
        readonly string[] lineSeparators = new[] { "\r\n", "\r", "\n" };
        readonly string[] targetSepeartors = new[] { ":", "," };
        readonly string[] targetSepeartors2 = new[] { ":" };
        UTS3GUI.RenderPipeline m_selectedRenderPipeline;
        List<Material> m_ConvertingMaterials = new List<Material>();
        Dictionary<Material, string> m_Material2GUID_Dictionary = new Dictionary<Material, string>();
        Dictionary<string, UTS2GUID> m_GuidToUTSID_Dictionary = new Dictionary<string, UTS2GUID>();
        static int frameToWait;
#if SHOW_CONVERTER_ON_STARTUP
        static UTS3ConverterWindow()
        {

            ConverterBehaviour();

        }
#endif
        static void ConvertorBehaviourDelayed()
        {
            if (frameToWait > 0)
            { 
                --frameToWait;
            }
            else
            {
                EditorApplication.update -= ConvertorBehaviourDelayed;

                //Application.isPlaying cannot be called in constructor. Do it here
                if (Application.isPlaying)
                    return;
                if (UnityToonShaderSettings.instance.m_ShowConverter == true)
                {
                    ShowWindow();

                }
            }
        }
#if SHOW_CONVERTER_ON_STARTUP
        [Callbacks.DidReloadScripts]
#endif
        static void ConverterBehaviour()
        {
            //We need to wait at least one frame or the popup will not show up
            frameToWait = 10;
            EditorApplication.update += ConvertorBehaviourDelayed;
        }

        static string packageFullPath
        {
            get;set;
        }
        void OnEnable()
        {
            InitIfNeeded();

        }
        void InitIfNeeded()
        {
            m_selectedRenderPipeline = UTS3GUI.currentRenderPipeline;
            packageFullPath = GetPackageFullPath();

            bool isUtsInstalled = CheckUTS2isInstalled();
            bool isUtsSupportedVersion = CheckUTS2VersionError();

            if (m_CoreConvertersList.Any())
                return;
            m_CoreConvertersList = new List<RenderPipelineConverter>();
            // This is the drop down choices.
            m_ConverterContainers = TypeCache.GetTypesDerivedFrom<RenderPipelineConverterContainer>();
            foreach (var containerType in m_ConverterContainers)
            {
                var container = (RenderPipelineConverterContainer)Activator.CreateInstance(containerType);
                m_Containers.Add(container);
            }

            // this need to be sorted by Priority property
            m_Containers = m_Containers
                .OrderBy(o => o.priority).ToList();

            foreach (var container in m_Containers)
            {
                m_ContainerChoices.Add(container.name);
            }

            if (m_ConverterContainers.Any())
            {
                GetConverters();
            }
            else
            {
                ClearConverterStates();
            }
        }

        void GetConverters()
        {
            ClearConverterStates();
            var converterList = TypeCache.GetTypesDerivedFrom<RenderPipelineConverter>();

            for (int i = 0; i < converterList.Count; ++i)
            {
                // Iterate over the converters that are used by the current container
                RenderPipelineConverter conv = (RenderPipelineConverter)Activator.CreateInstance(converterList[i]);
                m_CoreConvertersList.Add(conv);
            }

            // this need to be sorted by Priority property
            m_CoreConvertersList = m_CoreConvertersList
                .OrderBy(o => o.priority).ToList();

            for (int i = 0; i < m_CoreConvertersList.Count; i++)
            {
                // Create a new ConvertState which holds the active state of the converter
                var converterState = new ConverterState
                {
                    isEnabled = m_CoreConvertersList[i].isEnabled,
                    isActive = false,
                    isInitialized = false,
                    items = new List<ConverterItemState>(),
                    index = i,
                };
                m_ConverterStates.Add(converterState);

                // This just creates empty entries in the m_ItemsToConvert.
                // This list need to have the same amount of entries as the converters
                List<ConverterItemDescriptor> converterItemInfos = new List<ConverterItemDescriptor>();
                m_ItemsToConvert.Add(new ConverterItems { itemDescriptors = converterItemInfos });
            }
        }
        void ClearConverterStates()
        {
            m_CoreConvertersList.Clear();
            m_ConverterStates.Clear();
            m_ItemsToConvert.Clear();
            m_VEList.Clear();
        }
//        [MenuItem("Window/Rendering/Unity Toon Shader Converter", false, 51)]
        static private UTS3ConverterWindow ShowWindow()
        {
            var wnd = GetWindow<UTS3ConverterWindow>();
            wnd.titleContent = new GUIContent("Unity Toon Shader Converter");
            DontSaveToLayout(wnd);
            wnd.minSize = new Vector2(650f, 400f);
            wnd.Show();
            return wnd;
        }
        private void CreateGUI()
        {
#if false
#if false // UNITY_2021_1_OR_NEWER
            iconHelp = CoreEditorUtils.FindTexture("_Help");
#endif
            iconWarn = CoreEditorUtils.LoadIcon("icons", "console.warnicon", ".png");
            iconFail = CoreEditorUtils.LoadIcon("icons", "console.erroricon", ".png");
            iconSuccess = EditorGUIUtility.FindTexture("TestPassed");
            iconComplete = CoreEditorUtils.LoadIcon("icons", "GreenCheckmark", ".png");
            iconPending = EditorGUIUtility.FindTexture("Toolbar Minus");

            converterStateInfoDisabled = new Tuple<string, Texture2D>("Converter Disabled", null);
            converterStateInfoPendingInitialization = new Tuple<string, Texture2D>("Pending Initialization", iconPending);
            converterStateInfoPendingConversion = new Tuple<string, Texture2D>("Pending Conversion", iconPending);
            converterStateInfoPendingConversionWarning = new Tuple<string, Texture2D>("Pending Conversion with Warnings", iconWarn);
            converterStateInfoCompleteErrors = new Tuple<string, Texture2D>("Conversion Complete with Errors", iconFail);
            converterStateInfoComplete = new Tuple<string, Texture2D>("Conversion Complete", iconComplete);

            string theme = EditorGUIUtility.isProSkin ? "dark" : "light";
            InitIfNeeded();

            if (m_ConverterContainers.Any())
            {
                m_SerializedObject = new SerializedObject(this);
                converterEditorAsset.CloneTree(rootVisualElement);
#if false
                rootVisualElement.Q<DropdownField>("conversionsDropDown").choices = m_ContainerChoices;
                rootVisualElement.Q<DropdownField>("conversionsDropDown").index = m_ContainerChoiceIndex;
#else
                rootVisualElement.Q<PopupField<string>>("conversionsDropDown").choices = m_ContainerChoices;
                rootVisualElement.Q<PopupField<string>>("conversionsDropDown").index = m_ContainerChoiceIndex;
#endif
                // Getting the scrollview where the converters should be added
                m_ScrollView = rootVisualElement.Q<ScrollView>("convertersScrollView");

                m_ConvertButton = rootVisualElement.Q<Button>("convertButton");
                m_ConvertButton.RegisterCallback<ClickEvent>(Convert);

                m_InitButton = rootVisualElement.Q<Button>("initializeButton");
                m_InitButton.RegisterCallback<ClickEvent>(InitializeAllActiveConverters);

                m_InitAnConvertButton = rootVisualElement.Q<Button>("initializeAndConvert");
                m_InitAnConvertButton.RegisterCallback<ClickEvent>(InitializeAndConvert);
#if UNITY_2021_1_OR_NEWER
                m_ContainerHelpButton = rootVisualElement.Q<Button>("containerHelpButton");
//                m_ContainerHelpButton.RegisterCallback<ClickEvent>(GotoHelpURL);
                m_ContainerHelpButton.Q<Image>("containerHelpImage").image = CoreEditorStyles.iconHelp;
                m_ContainerHelpButton.RemoveFromClassList("unity-button");
                m_ContainerHelpButton.AddToClassList(theme);
#endif
                RecreateUI();
            }
#endif
        }


        void InitializeAndConvert(ClickEvent evt)
        {
            m_InitAndConvert = ShouldCreateSearchIndex();

            InitializeAllActiveConverters(evt);
            if (!m_InitAndConvert)
            {
                Convert(evt);
            }
        }
        private bool SaveCurrentSceneAndContinue()
        {
#if false
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.isDirty)
            {
                if (EditorUtility.DisplayDialog("Scene is not saved.",
                    "Current scene is not saved. Please save the scene before continuing.", "Save and Continue",
                    "Cancel"))
                {
                    EditorSceneManager.SaveScene(currentScene);
                }
                else
                {
                    return false;
                }
            }
#endif
            return true;
        }

        bool ShouldCreateSearchIndex()
        {
            for (int i = 0; i < m_ConverterStates.Count; ++i)
            {
                if (m_ConverterStates[i].requiresInitialization)
                {
                    var converter = m_CoreConvertersList[i];
                    if (converter.needsIndexing)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        void AddToContextMenu(ContextualMenuPopulateEvent evt, int coreConverterIndex)
        {
            var ve = (VisualElement)evt.target;
            // Checking if this context menu should be enabled or not
            var isActive = m_ConverterStates[coreConverterIndex].items[(int)ve.userData].isActive &&
                !m_ConverterStates[coreConverterIndex].items[(int)ve.userData].hasConverted;
#if false
            evt.menu.AppendAction("Run converter for this asset",
                e =>
                {
                    ConvertIndex(coreConverterIndex, (int)ve.userData);
                    // Refreshing the list to show the new state
#if UNITY_2021_1_OR_NEWER
                    m_ConverterSelectedVE.Q<ListView>("converterItems").Rebuild();
#endif
                },
                isActive == true ? DropdownMenuAction.AlwaysEnabled : DropdownMenuAction.AlwaysDisabled);
#else
            if (isActive)
            {
                evt.menu.AppendAction("Run converter for this asset",
                    e =>
                    {
                        ConvertIndex(coreConverterIndex, (int)ve.userData);
                        // Refreshing the list to show the new state
#if UNITY_2021_1_OR_NEWER
                    m_ConverterSelectedVE.Q<ListView>("converterItems").Rebuild();
#endif
                    }, DropdownMenuAction.AlwaysEnabled);
            }
            else
            {
                evt.menu.AppendAction("Run converter for this asset",
                e =>
                {
                    ConvertIndex(coreConverterIndex, (int)ve.userData);
                    // Refreshing the list to show the new state
#if UNITY_2021_1_OR_NEWER
                                m_ConverterSelectedVE.Q<ListView>("converterItems").Rebuild();
#endif
                }, DropdownMenuAction.AlwaysDisabled);
            }
#endif
        }

        void UpdateInfo(int stateIndex, RunItemContext ctx)
        {
            if (ctx.didFail)
            {
                m_ConverterStates[stateIndex].items[ctx.item.index].message = ctx.info;
                m_ConverterStates[stateIndex].items[ctx.item.index].status = Status.Error;
                m_ConverterStates[stateIndex].errors++;
            }
            else
            {
                m_ConverterStates[stateIndex].items[ctx.item.index].status = Status.Success;
                m_ConverterStates[stateIndex].success++;
            }

            m_ConverterStates[stateIndex].pending--;

            // Making sure that this is set here so that if user is clicking Convert again it will not run again.
            ctx.hasConverted = true;

            VisualElement child = m_ScrollView[stateIndex];
#if UNITY_2021_1_OR_NEWER
            child.Q<ListView>("converterItems").Rebuild();
#endif
        }

        void InitializeAllActiveConverters(ClickEvent evt)
        {
            if (!SaveCurrentSceneAndContinue()) return;
#if false
            // If we use search index, go async
            if (ShouldCreateSearchIndex())
            {
                // Save the current worker count. So it can be reset after the index file has been created.
                m_WorkerCount = AssetDatabase.DesiredWorkerCount;
                AssetDatabase.ForceToDesiredWorkerCount();

                AssetDatabase.DesiredWorkerCount = System.Convert.ToInt32(Math.Ceiling(Environment.ProcessorCount * 0.8));
                CreateSearchIndex(m_URPConverterIndex);
            }
            // Otherwise do everything directly
            else
            {
                ConverterCollectData(() => { EditorUtility.ClearProgressBar(); });
            }

            void CreateSearchIndex(string name)
            {
                // Create <guid>.index in the project
                var title = $"Building {name} search index";
                EditorUtility.DisplayProgressBar(title, "Creating search index...", -1f);

                Search.SearchService.CreateIndex(name, IndexingOptions.Temporary | IndexingOptions.Extended,
                    new[] { "Assets" },
                    new[] { ".prefab", ".unity", ".asset" },
                    null, OnSearchIndexCreated);
            }

            void OnSearchIndexCreated(string name, string path, Action onComplete)
            {
                EditorUtility.ClearProgressBar();
                ConverterCollectData(() =>
                {
                    if (m_InitAndConvert)
                    {
                        Convert(null);
                        m_InitAndConvert = false;
                    }
                    EditorUtility.ClearProgressBar();
                    AssetDatabase.DesiredWorkerCount = m_WorkerCount;
                    AssetDatabase.ForceToDesiredWorkerCount();

                    RecreateUI();
                    onComplete();
                });
            }

            void ConverterCollectData(Action onConverterDataCollectionComplete)
            {
                EditorUtility.DisplayProgressBar($"Initializing converters", $"Initializing converters...", -1f);

                int convertersToConvert = 0;
                for (int i = 0; i < m_ConverterStates.Count; ++i)
                {
                    if (m_ConverterStates[i].requiresInitialization)
                    {
                        convertersToConvert++;
                        GetAndSetData(i, onConverterDataCollectionComplete);
                    }
                }

                // If we did not kick off any converter initialization
                // We can complete everything immediately
                if (convertersToConvert == 0)
                {
                    onConverterDataCollectionComplete?.Invoke();
                }
            }
#endif
            RecreateUI();
        }

        void BackToConverters(ClickEvent evt)
        {
            HideConverterLayout(m_ConverterSelectedVE);
        }

        void RecreateUI()
        {
            m_SerializedObject.Update();
            // This is temp now to get the information filled in
#if false
            rootVisualElement.Q<DropdownField>("conversionsDropDown").RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                m_ContainerChoiceIndex = rootVisualElement.Q<DropdownField>("conversionsDropDown").index;
                rootVisualElement.Q<TextElement>("conversionInfo").text = currentContainer.info;
                HideUnhideConverters();
            });
#else
            rootVisualElement.Q<PopupField<string>>("conversionsDropDown").RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                m_ContainerChoiceIndex = rootVisualElement.Q<PopupField<string>>("conversionsDropDown").index;
                rootVisualElement.Q<TextElement>("conversionInfo").text = currentContainer.info;
                HideUnhideConverters();
            });
#endif


            rootVisualElement.Q<TextElement>("conversionInfo").text = currentContainer.info;
            m_ScrollView.Clear();

            for (int i = 0; i < m_CoreConvertersList.Count; ++i)
            {
                // Making an item using the converterListAsset as a template.
                // Then adding the information needed for each converter
                VisualElement item = new VisualElement();
                converterWidgetMainAsset.CloneTree(item);
                // Adding the VE so that we can hide it and unhide it when needed
                m_VEList.Add(item);

                RenderPipelineConverter conv = m_CoreConvertersList[i];
                item.name = $"{conv.name}#{conv.container.AssemblyQualifiedName}";
                item.SetEnabled(conv.isEnabled);
                item.Q<Label>("converterName").text = conv.name;
                item.Q<Label>("converterInfo").text = conv.info;

                int id = i;
                var converterEnabledToggle = item.Q<Toggle>("converterEnabled");
                converterEnabledToggle.RegisterCallback<ClickEvent>((evt) =>
                {
                    ConverterStatusInfo(id, item);
                    InitOrConvert();
                    // This toggle needs to stop propagation since it is inside another clickable element
                    evt.StopPropagation();
                });
                var topElement = item.Q<VisualElement>("converterTopVisualElement");
                topElement.RegisterCallback<ClickEvent>((evt) =>
                {
                    ShowConverterLayout(item);
                    item.Q<VisualElement>("allNoneVE").style.display = DisplayStyle.Flex;
                    item.Q<Label>("all").RegisterCallback<ClickEvent>(evt =>
                    {
                        ToggleAllNone(evt, id, true, item);
                    });
                    item.Q<Label>("none").RegisterCallback<ClickEvent>(evt =>
                    {
                        ToggleAllNone(evt, id, false, item);
                    });
                });

                // setup the images
                item.Q<Image>("pendingImage").image = iconPending;
                item.Q<Image>("pendingImage").tooltip = "Pending";
                var pendingLabel = item.Q<Label>("pendingLabel");
                item.Q<Image>("warningImage").image = iconWarn;
                item.Q<Image>("warningImage").tooltip = "Warnings";
                var warningLabel = item.Q<Label>("warningLabel");
                item.Q<Image>("errorImage").image = iconFail;
                item.Q<Image>("errorImage").tooltip = "Failed";
                var errorLabel = item.Q<Label>("errorLabel");
                item.Q<Image>("successImage").image = iconComplete;
                item.Q<Image>("successImage").tooltip = "Success";
                var successLabel = item.Q<Label>("successLabel");

                converterEnabledToggle.bindingPath =
                    $"{nameof(m_ConverterStates)}.Array.data[{i}].{nameof(ConverterState.isActive)}";
                pendingLabel.bindingPath =
                    $"{nameof(m_ConverterStates)}.Array.data[{i}].{nameof(ConverterState.pending)}";
                warningLabel.bindingPath =
                    $"{nameof(m_ConverterStates)}.Array.data[{i}].{nameof(ConverterState.warnings)}";
                errorLabel.bindingPath =
                    $"{nameof(m_ConverterStates)}.Array.data[{i}].{nameof(ConverterState.errors)}";
                successLabel.bindingPath =
                    $"{nameof(m_ConverterStates)}.Array.data[{i}].{nameof(ConverterState.success)}";

                ConverterStatusInfo(id, item);

                VisualElement child = item;
                ListView listView = child.Q<ListView>("converterItems");

                listView.showBoundCollectionSize = false;
                listView.bindingPath = $"{nameof(m_ConverterStates)}.Array.data[{i}].{nameof(ConverterState.items)}";

                // Update the amount of things to convert
                UpdateSelectedConverterItems(id, child);

                listView.makeItem = () =>
                {
                    var convertItem = converterItem.CloneTree();
                    // Adding the contextual menu for each item
                    convertItem.AddManipulator(new ContextualMenuManipulator(evt => AddToContextMenu(evt, id)));
                    return convertItem;
                };

                listView.bindItem = (element, index) =>
                {
                    m_SerializedObject.Update();
                    var property = m_SerializedObject.FindProperty($"{listView.bindingPath}.Array.data[{index}]");

                    // ListView doesn't bind the child elements for us properly, so we do that for it
                    // In the UXML our root is a BindableElement, as we can't bind otherwise.
                    var bindable = (BindableElement)element;
                    bindable.BindProperty(property);

                    // Adding index here to userData so it can be retrieved later
                    element.userData = index;

                    Status status = (Status)property.FindPropertyRelative("status").enumValueIndex;
                    string info = property.FindPropertyRelative("message").stringValue;

                    element.Q<Toggle>("converterItemActive").RegisterCallback<ClickEvent>((evt) =>
                    {
                        UpdateSelectedConverterItems(id, child);
                        DeselectAllNoneLabels(item);
                    });

                    ConverterItemDescriptor convItemDesc = m_ItemsToConvert[id].itemDescriptors[index];

                    element.Q<Label>("converterItemName").text = convItemDesc.name;
                    element.Q<Label>("converterItemPath").text = convItemDesc.info;

                    if (!string.IsNullOrEmpty(convItemDesc.helpLink))
                    {
                        element.Q<Image>("converterItemHelpIcon").image = iconHelp;
                        element.Q<Image>("converterItemHelpIcon").tooltip = convItemDesc.helpLink;
                    }

                    // Changing the icon here depending on the status.
                    Texture2D icon = null;

                    switch (status)
                    {
                        case Status.Pending:
                            icon = iconPending;
                            break;
                        case Status.Error:
                            icon = iconFail;
                            break;
                        case Status.Warning:
                            icon = iconWarn;
                            break;
                        case Status.Success:
                            icon = iconComplete;
                            break;
                    }

                    element.Q<Image>("converterItemStatusIcon").image = icon;
                    element.Q<Image>("converterItemStatusIcon").tooltip = info;
                };
#if UNITY_2022_2_OR_NEWER
                listView.selectionChanged += obj => { m_CoreConvertersList[id].OnClicked(listView.selectedIndex); };
#else
                listView.onSelectionChange += obj => { m_CoreConvertersList[id].OnClicked(listView.selectedIndex); };
#endif
                listView.unbindItem = (element, index) =>
                {
                    var bindable = (BindableElement)element;
                    bindable.Unbind();
                };

                m_ScrollView.Add(item);
            }

            InitOrConvert();
            HideUnhideConverters();
            rootVisualElement.Bind(m_SerializedObject);
        }

        private void HideUnhideConverters()
        {
            var type = currentContainer.GetType();
#if false //UNITY_2021_1_OR_NEWER
            if (DocumentationUtils.TryGetHelpURL(type, out var url))
            {
                m_ContainerHelpButton.style.display = DisplayStyle.Flex;
            }
            else
            {
                m_ContainerHelpButton.style.display = DisplayStyle.None;
            }
#endif
            foreach (VisualElement childElement in m_ScrollView.Q<VisualElement>().Children())
            {
                var container = Type.GetType(childElement.name.Split('#').Last());
                if (container == type)
                {
                    childElement.style.display = DisplayStyle.Flex;
                }
                else
                {
                    childElement.style.display = DisplayStyle.None;
                }
            }
        }

        void DeselectAllNoneLabels(VisualElement item)
        {
            item.Q<Label>("all").AddToClassList("not_selected");
            item.Q<Label>("all").RemoveFromClassList("selected");

            item.Q<Label>("none").AddToClassList("not_selected");
            item.Q<Label>("none").RemoveFromClassList("selected");
        }

        void ConverterStatusInfo(int index, VisualElement item)
        {
            Tuple<string, Texture2D> info = converterStateInfoDisabled; ;
            // Check if it is active
            if (m_ConverterStates[index].isActive)
            {
                info = converterStateInfoPendingInitialization;
            }
            if (m_ConverterStates[index].isInitialized)
            {
                info = converterStateInfoPendingConversion;
            }
            if (m_ConverterStates[index].warnings > 0)
            {
                info = converterStateInfoPendingConversionWarning;
            }
            if (m_ConverterStates[index].errors > 0)
            {
                info = converterStateInfoCompleteErrors;
            }
            if (m_ConverterStates[index].errors == 0 && m_ConverterStates[index].warnings == 0 && m_ConverterStates[index].success > 0)
            {
                info = converterStateInfoComplete;
            }
            if (!m_ConverterStates[index].isActive)
            {
                info = converterStateInfoDisabled;
            }
            item.Q<Label>("converterStateInfoL").text = info.Item1;
            item.Q<Image>("converterStateInfoIcon").image = info.Item2;
        }

        void ShowConverterLayout(VisualElement element)
        {
            m_ConverterSelectedVE = element;
            rootVisualElement.Q<VisualElement>("converterEditorMainVE").style.display = DisplayStyle.None;
            rootVisualElement.Q<VisualElement>("singleConverterVE").style.display = DisplayStyle.Flex;
            rootVisualElement.Q<VisualElement>("singleConverterVE").Add(element);
            element.Q<VisualElement>("converterItems").style.display = DisplayStyle.Flex;
            element.Q<VisualElement>("informationVE").style.display = DisplayStyle.Flex;

            rootVisualElement.Q<Button>("backButton").RegisterCallback<ClickEvent>(BackToConverters);
        }

        void HideConverterLayout(VisualElement element)
        {
            rootVisualElement.Q<VisualElement>("converterEditorMainVE").style.display = DisplayStyle.Flex;
            rootVisualElement.Q<VisualElement>("singleConverterVE").style.display = DisplayStyle.None;
            rootVisualElement.Q<VisualElement>("singleConverterVE").Remove(element);

            element.Q<VisualElement>("converterItems").style.display = DisplayStyle.None;
            element.Q<VisualElement>("informationVE").style.display = DisplayStyle.None;

            RecreateUI();
            m_ConverterSelectedVE = null;
        }

        void ToggleAllNone(ClickEvent evt, int index, bool value, VisualElement item)
        {
            var conv = m_ConverterStates[index];
            if (conv.items.Count > 0)
            {
                foreach (var convItem in conv.items)
                {
                    convItem.isActive = value;
                }
                UpdateSelectedConverterItems(index, item);
                // Changing the look of the labels
                if (value)
                {
                    item.Q<Label>("all").AddToClassList("selected");
                    item.Q<Label>("all").RemoveFromClassList("not_selected");

                    item.Q<Label>("none").AddToClassList("not_selected");
                    item.Q<Label>("none").RemoveFromClassList("selected");
                }
                else
                {
                    item.Q<Label>("none").AddToClassList("selected");
                    item.Q<Label>("none").RemoveFromClassList("not_selected");

                    item.Q<Label>("all").AddToClassList("not_selected");
                    item.Q<Label>("all").RemoveFromClassList("selected");
                }
            }
        }

        void UpdateSelectedConverterItems(int index, VisualElement element)
        {
            int count = 0;
            foreach (ConverterItemState state in m_ConverterStates[index].items)
            {
                if (state.isActive)
                {
                    count++;
                }
            }

            element.Q<Label>("converterStats").text = $"{count}/{m_ItemsToConvert[index].itemDescriptors.Count} selected";
        }

        void InitOrConvert()
        {
            bool allSelectedHasInitialized = true;
            // Check if all ticked ones have been initialized.
            // If not then Init Button should be active
            // Get all active converters

            if (m_ConverterStates.Any())
            {
                foreach (ConverterState state in m_ConverterStates)
                {
                    if (state.isActiveAndEnabled && !state.isInitialized)
                    {
                        allSelectedHasInitialized = false;
                        break;
                    }
                }
            }
            else
            {
                // If no converters is active.
                // we should make the text somewhat disabled
                allSelectedHasInitialized = false;
            }

            if (allSelectedHasInitialized)
            {
                m_ConvertButton.style.display = DisplayStyle.Flex;
                m_InitButton.style.display = DisplayStyle.None;
            }
            else
            {
                m_ConvertButton.style.display = DisplayStyle.None;
                m_InitButton.style.display = DisplayStyle.Flex;
            }
        }

        internal static void DontSaveToLayout(EditorWindow wnd)
        {
#if true
            return;
#else
            // Making sure that the window is not saved in layouts.
            Assembly assembly = typeof(EditorWindow).Assembly;
            var editorWindowType = typeof(EditorWindow);
            var hostViewType = assembly.GetType("UnityEditor.HostView");
            var containerWindowType = assembly.GetType("UnityEditor.ContainerWindow");
            var parentViewField = editorWindowType.GetField("m_Parent", BindingFlags.Instance | BindingFlags.NonPublic);
            var parentViewValue = parentViewField.GetValue(wnd);
            // window should not be saved to layout
            var containerWindowProperty =
                hostViewType.GetProperty("window", BindingFlags.Instance | BindingFlags.Public);
            var parentContainerWindowValue = containerWindowProperty.GetValue(parentViewValue);
            var dontSaveToLayoutField =
                containerWindowType.GetField("m_DontSaveToLayout", BindingFlags.Instance | BindingFlags.NonPublic);
            dontSaveToLayoutField.SetValue(parentContainerWindowValue, true);
#endif
        }

        static bool CheckUTS2VersionError()
        {
            s_guids = AssetDatabase.FindAssets("t:Material", null);
            int materialCount = 0;
 
            for (int ii = 0; ii < s_guids.Length; ii++)
            {
                var guid = s_guids[ii];


                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material material = AssetDatabase.LoadAssetAtPath<Material>(path);

                var shaderName = material.shader.ToString();
                if (!shaderName.StartsWith(legacyShaderPrefix))
                {
                    continue;

                }
                const string utsVersionProp = "_utsVersion";
                if (material.HasProperty(utsVersionProp))
                {
                    float utsVersion = material.GetFloat(utsVersionProp);
                    if (utsVersion < 2.07)
                    {
                        s_versionErrorCount++;
                        continue;
                    }
                }
                else
                {
                    s_versionErrorCount++;
                    continue;
                }
                materialCount++;
            }
            s_materialCount = materialCount;
            if ( s_versionErrorCount > 0)
            {
                return true;
            }
            return false;
        }
        static bool  CheckUTS2isInstalled()
        {
            var shaders = AssetDatabase.FindAssets("t:Shader", new string[] { "Assets" });
            foreach ( var guid in shaders)
            {
                foreach ( var shader in stdShaders)
                {
                    if (guid == shader.m_Guid)
                    {
                        /*
                                            var filename = AssetDatabase.GUIDToAssetPath(guid);

                                            if (!filename.EndsWith(kLegacyShaderFileName + kShaderFileNameExtention))
                                            {
                                                return true;
                                            }
                        */
                        return true;

                    }
                }
                foreach (var shader in tessShaders)
                {
                    if (guid == shader.m_Guid)
                    {
                        /*
                                            var filename = AssetDatabase.GUIDToAssetPath(guid);

                                            if (!filename.EndsWith(kLegacyShaderFileName + kShaderFileNameExtention))
                                            {
                                                return true;
                                            }
                        */
                        return true;

                    }
                }
            }
            return false;
        }
        static UTS2GUID FindUTS2GUID(string guid)
        {
            var ret = Array.Find<UTS2GUID>(stdShaders, element => element.m_Guid == guid);
            foreach( var shader in stdShaders)
            {
                if ( shader.m_Guid == guid )
                {
                    return shader;
                }
            }
            foreach ( var shader in tessShaders)
            {
                if ( shader.m_Guid == guid )
                {
                    return shader;
                }
            }
            return null;
        }
        void Error(string path)
        {
            Debug.LogErrorFormat("File: {0} is corrupted.", path);

        }

#if false
        private void OnGUI()
        {
            if (!m_initialzed)
            {
                s_guids = AssetDatabase.FindAssets("t:Material", null);
            }
            m_initialzed = true;
            //if (!m_errorIsChecked)
            {
                s_versionErrorCount = 0;
                CheckUTS2VersionError();
                m_uts2isInstalled = CheckUTS2isInstalled();
            }
//            m_errorIsChecked = true;
            m_ConvertingMaterials.Clear();
            m_Material2GUID_Dictionary.Clear();
            m_GuidToUTSID_Dictionary.Clear();

            int labelHeight = 80;
            int buttonHeight = 20;
            Rect rect = new Rect(0, labelHeight, position.width, position.height - buttonHeight ); // GUILayoutUtility.GetRect(position.width, position.height - buttonHeight);
            Rect rect2 = new Rect(2, labelHeight, position.width - 4, position.height - 4 - buttonHeight );
            // scroll view background
            EditorGUI.DrawRect(rect, Color.gray);
            EditorGUI.DrawRect(rect2, new Color(0.3f, 0.3f, 0.3f));
            var colorStore = GUI.color;
            GUI.color = Color.yellow;
            EditorGUILayout.LabelField("Unity-Chan Toon Shader 2 to Unity Toon Shader.");
            EditorGUILayout.LabelField("Please make sure that the project is backed up.");
            GUI.color = colorStore;

            if (m_uts2isInstalled)
            {
                colorStore = GUI.color;
                GUI.color = Color.red;

                EditorGUILayout.LabelField("Error: Unity-Chan Toon Shader 2 files are installed.");
                EditorGUILayout.LabelField("Please remove them before using Unity Toon Shader and run this converter.");
                GUI.color = colorStore;
            }
            else if (s_versionErrorCount > 0)
            {
                colorStore = GUI.color;
                GUI.color = Color.red;
                EditorGUILayout.LabelField("Error: Unity-Chan Toon Shader 2 version in the project is too old.");
                EditorGUILayout.LabelField("This conver is compatible with ones newer than 2.0.7.");
                GUI.color = colorStore;
            }
            else if (s_materialCount != 0)
            {
                EditorGUILayout.LabelField("No Unity-Chan Toon Shader 2 materials are found in the project.");
                EditorGUILayout.LabelField("Nothing to be done for this converter.");
            }
            else
            {
                EditorGUILayout.LabelField("Following materials will be coverted.");
                EditorGUILayout.LabelField("");
            }
            using (new EditorGUI.DisabledScope(m_uts2isInstalled))
            {

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Convert to ");
                m_selectedRenderPipeline = (UTS3GUI.RenderPipeline)EditorGUILayout.Popup((int)m_selectedRenderPipeline, m_RendderPipelineNames);
                EditorGUILayout.EndHorizontal();
            }


            // scroll view ical
            m_scrollPos =
                 EditorGUILayout.BeginScrollView(m_scrollPos, GUILayout.Width(position.width - 4));
            EditorGUILayout.BeginVertical();

            int materialCount = 0;

            for (int ii = 0; ii < s_guids.Length; ii++)
            {
                var guid = s_guids[ii];

                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
                var shaderName = material.shader.ToString();
                if (!shaderName.StartsWith("Hidden/InternalErrorShader"))
                {
                    continue;
                }
                string content = File.ReadAllText(path);
                string[] lines = content.Split(lineSeparators, StringSplitOptions.None);
                // always two spaces before m_Shader?
                var targetLine = Array.Find<string>(lines,line => line.StartsWith("  m_Shader:"));
                var shaderMetadata = targetLine.Split(targetSepeartors, StringSplitOptions.None);
                if (shaderMetadata.Length < 4 )
                {
                    Error(path);
                    continue;
                }
                var shaderGUID = shaderMetadata[4];
                while (shaderGUID.StartsWith(" "))
                {
                    shaderGUID = shaderGUID.TrimStart(' ');
                }
                var foundUTS2GUID = FindUTS2GUID(shaderGUID);
                if ( foundUTS2GUID == null )
                {
                    continue;
                }

                var targetLine2 = Array.Find<string>(lines, line => line.StartsWith("    - _utsVersion"));
                if (targetLine2 == null )
                {
                    Error(path);
                    continue;
                }
                string[] lines2 = targetLine2.Split(targetSepeartors2, StringSplitOptions.None);
                if ( lines2 == null || lines2.Length < 2 )
                {
                    Error(path);
                    s_versionErrorCount++;
                    continue;
                }
                var utsVersionString = lines2[1];
                while (utsVersionString.StartsWith(" "))
                {
                    utsVersionString = utsVersionString.TrimStart(' ');
                }
                float utsVersion = float.Parse(utsVersionString);
                if (utsVersion < 2.07f)
                {
                    s_versionErrorCount++;
                    continue;
                }
                m_ConvertingMaterials.Add(material);
                if (!m_Material2GUID_Dictionary.ContainsKey(material))
                {
                    m_Material2GUID_Dictionary.Add(material, shaderGUID);
                }
                if (!m_GuidToUTSID_Dictionary.ContainsKey(shaderGUID))
                {
                    m_GuidToUTSID_Dictionary.Add(shaderGUID, foundUTS2GUID);
                }
                materialCount++;
                s_materialCount = materialCount;
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(16);
                string str = "" + materialCount + ":";

                EditorGUILayout.LabelField(str, GUILayout.Width(40));
                EditorGUILayout.LabelField(path, GUILayout.Width(Screen.width - 130));
                GUILayout.Space(1);
                EditorGUILayout.EndHorizontal();
            }
            if (s_materialCount == 0)
            {
                GUILayout.Space(16);
                if (s_versionErrorCount > 0 )
                {
                    EditorGUILayout.LabelField("   Error: Unity-Chan Toon Shader 2 version must be newer than 2.0.7");
                }

            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();


            // buttons 
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            using (new EditorGUI.DisabledScope(s_materialCount == 0 || m_uts2isInstalled))
            {
                if (GUILayout.Button(new GUIContent("Convert")))
                {
                    RestoreShaderGUID(m_selectedRenderPipeline);
                    ConvertMaterials(m_selectedRenderPipeline, s_guids);
                }
            }
            if ( GUILayout.Button(new GUIContent("Close")) )
            {
                Close();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();
            UnityToonShaderSettings.instance.m_ShowConverter = EditorGUILayout.Toggle("Show on start", UnityToonShaderSettings.instance.m_ShowConverter);
            if (EditorGUI.EndChangeCheck())
            {
                UnityToonShaderSettings.Save();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

        }
#endif

        private static string GetPackageFullPath()
        {
            const string kUtsPackageName = "com.unity.toonshader";
            // Check for potential UPM package
            string packagePath = Path.GetFullPath("Packages/" + kUtsPackageName);
            if (Directory.Exists(packagePath))
            {
                return packagePath;
            }
            return null;
        }
        void RestoreShaderGUID(UTS3GUI.RenderPipeline renderPipeline)
        {
            var packagePath = packageFullPath;
            const string kGuid = "guid: ";
            AssetDatabase.StartAssetEditing();
            if ( renderPipeline == UTS3GUI.RenderPipeline.Legacy)
            {
                var filePath = packagePath + "/Runtime/Legacy/Shaders/" + kLegacyShaderFileName + kShaderFileNameExtention + ".meta";
                string content = File.ReadAllText(filePath);
                string[] lines = content.Split(lineSeparators, StringSplitOptions.None);
                string oldGuid = null;
                foreach ( var line in lines)
                {
                    if ( line.Contains(kGuid))
                    {
                        var splitted = line.Split(targetSepeartors2, StringSplitOptions.None);
                        oldGuid = splitted[1];
                        while (oldGuid.StartsWith(" "))
                        {
                            oldGuid = oldGuid.TrimStart(' ');
                        }
                        break;
                    }
                }
                content = content.Replace(kGuid + oldGuid, kGuid + stdShaders[0].m_Guid);
                using (FileStream fs = new FileStream(filePath, FileMode.Open)) { using (TextWriter tw = new StreamWriter(fs, Encoding.UTF8, 1024, true)) { tw.Write(content); } fs.SetLength(fs.Position); }
            }
            else if (renderPipeline == UTS3GUI.RenderPipeline.Universal)
            {

            }
            AssetDatabase.StopAssetEditing();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        void ConvertMaterials(UTS3GUI.RenderPipeline renderPipeline, string[] guids)
        {
            foreach (var material in m_ConvertingMaterials)
            {
                switch (renderPipeline)
                {
                    case UTS3GUI.RenderPipeline.Legacy: // built in
                        material.shader = Shader.Find("Toon (Built-in)"); 
                        break;
                    case UTS3GUI.RenderPipeline.Universal: // Universal
                        material.shader = Shader.Find("Universal Render Pipeline/Toon");
                        break;
                    case UTS3GUI.RenderPipeline.HDRP: // HDRP
                        material.shader = Shader.Find("HDRP/Toon");
                        break;
                }
                var shaderGUID = m_Material2GUID_Dictionary[material];
                var UTS2GUID = m_GuidToUTSID_Dictionary[shaderGUID];

                //                _Transparent_Setting = (UTS3GUI.UTS_TransparentMode)UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropTransparentEnabled);
                _Transparent_Setting = UTS3GUI.UTS_TransparentMode.Off;
                if ( UTS2GUID.m_ShaderName.Contains("Trans") || UTS2GUID.m_ShaderName.Contains("trans"))
                {
                    _Transparent_Setting = UTS3GUI.UTS_TransparentMode.On;
                }
                _StencilNo_Setting = UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropStencilNo);
                _autoRenderQueue = UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropAutoRenderQueue);
                _renderQueue = material.renderQueue;
                UTS3GUI.UTS_Mode technique = (UTS3GUI.UTS_Mode)UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropUtsTechniqe);

                switch (technique)
                {
                    case UTS3GUI.UTS_Mode.ThreeColorToon:
                        material.DisableKeyword(UTS3GUI.ShaderDefineSHADINGGRADEMAP);
                        break;
                    case UTS3GUI.UTS_Mode.ShadingGradeMap:
                        material.EnableKeyword(UTS3GUI.ShaderDefineSHADINGGRADEMAP);
                        break;
                }
                if (_Transparent_Setting == UTS3GUI.UTS_TransparentMode.On)
                {
                    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropTransparentEnabled, 1);
                }
                if (_Transparent_Setting != UTS3GUI.UTS_TransparentMode.On)
                {
                    UTS3GUI.SetupOutline(material);
                }
                else
                {
                    UTS3GUI.SetupOverDrawTransparentObject(material);
                }
                SetCullingMode(material);
                SetRenderQueue(material);
                SetTranparent(material);

                BasicLookdevs(material);
                SetGameRecommendation(material);
                ApplyClippingMode(material);
                ApplyStencilMode(material);
                ApplyAngelRing(material);
                ApplyMatCapMode(material);
                ApplyQueueAndRenderType(technique, material);


            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }

        void SetCullingMode(Material material)
        {
            const string _CullMode = "_CullMode";
            int _CullMode_Setting = UTS3GUI.MaterialGetInt(material, _CullMode);
            //Convert it to Enum format and store it in the offlineMode variable.
            if ((int)UTS3GUI.CullingMode.Off == _CullMode_Setting)
            {
                m_cullingMode = UTS3GUI.CullingMode.Off;
            }
            else if ((int)UTS3GUI.CullingMode.Frontface == _CullMode_Setting)
            {
                m_cullingMode = UTS3GUI.CullingMode.Frontface;
            }
            else
            {
                m_cullingMode = UTS3GUI.CullingMode.Backface;
            }
            //If the value changes, write to the material.
            if (_CullMode_Setting != (int)m_cullingMode)
            {
                switch (m_cullingMode)
                {
                    case UTS3GUI.CullingMode.Off:
                        UTS3GUI.MaterialSetInt(material, _CullMode, 0);
                        break;
                    case UTS3GUI.CullingMode.Frontface:
                        UTS3GUI.MaterialSetInt(material, _CullMode, 1);
                        break;
                    default:
                        UTS3GUI.MaterialSetInt(material, _CullMode, 2);
                        break;
                }

            }
        }
        void SetRenderQueue(Material material)
        {
            UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropAutoRenderQueue, _autoRenderQueue);
            // material.renderQueue
        }

        void SetTranparent(Material material)
        {
            const string _ZWriteMode = "_ZWriteMode";
            const string _ZOverDrawMode = "_ZOverDrawMode";


            if (_Transparent_Setting == UTS3GUI.UTS_TransparentMode.On)
            {
                if (UTS3GUI.MaterialGetInt(material, UTS3GUI.ShaderPropUtsTechniqe) == (int)UTS3GUI.UTS_Mode.ThreeColorToon)
                {
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropClippingMode, (int)UTS3GUI.UTS_ClippingMode.TransClippingMode);
                }
                else
                {
                    // ShadingGradeMap
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropClippingMode, (int)UTS3GUI.UTS_TransClippingMode.On);
                }
                UTS3GUI.MaterialSetInt(material, _ZWriteMode, 0);
                material.SetFloat(_ZOverDrawMode, 1);
            }
            else
            {
                UTS3GUI.MaterialSetInt(material, _ZWriteMode, 1);
                material.SetFloat(_ZOverDrawMode, 0);
            }

        }

        void BasicLookdevs(Material material)
        {
            if (material.HasProperty( UTS3GUI.ShaderPropUtsTechniqe))//ThreeColorToon or ShadingGradeMap
            {
                if (UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropUtsTechniqe) == (int)UTS3GUI.UTS_Mode.ThreeColorToon)   //DWF
                {

                    //Sharing variables with ShadingGradeMap method.

                    material.SetFloat( UTS3GUI.ShaderProp1st_ShadeColor_Step, material.GetFloat( UTS3GUI.ShaderPropBaseColor_Step));
                    material.SetFloat( UTS3GUI.ShaderProp1st_ShadeColor_Feather, material.GetFloat( UTS3GUI.ShaderPropBaseShade_Feather));
                    material.SetFloat( UTS3GUI.ShaderProp2nd_ShadeColor_Step, material.GetFloat( UTS3GUI.ShaderPropShadeColor_Step));
                    material.SetFloat( UTS3GUI.ShaderProp2nd_ShadeColor_Feather, material.GetFloat( UTS3GUI.ShaderProp1st2nd_Shades_Feather));
                }
                else if (UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropUtsTechniqe) == (int)UTS3GUI.UTS_Mode.ShadingGradeMap)
                {    //SGM

                    //Share variables with DoubleWithFeather method.
                    material.SetFloat( UTS3GUI.ShaderPropBaseColor_Step, material.GetFloat( UTS3GUI.ShaderProp1st_ShadeColor_Step));
                    material.SetFloat( UTS3GUI.ShaderPropBaseShade_Feather, material.GetFloat( UTS3GUI.ShaderProp1st_ShadeColor_Feather));
                    material.SetFloat( UTS3GUI.ShaderPropShadeColor_Step, material.GetFloat( UTS3GUI.ShaderProp2nd_ShadeColor_Step));
                    material.SetFloat( UTS3GUI.ShaderProp1st2nd_Shades_Feather, material.GetFloat( UTS3GUI.ShaderProp2nd_ShadeColor_Feather));
                }
                else
                {
                    // OutlineObj.
                    return;
                }
            }
            EditorGUILayout.Space();
        }
        private bool IsShadingGrademap(Material material)
        {
            return UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropUtsTechniqe) == (int)UTS3GUI.UTS_Mode.ShadingGradeMap;
        }

        void ApplyQueueAndRenderType(UTS3GUI.UTS_Mode technique, Material material)
        {
            var stencilMode = (UTS3GUI.UTS_StencilMode)UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropStencilMode);
            if (_autoRenderQueue == 1)
            {
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
            }

            const string OPAQUE = "Opaque";
            const string TRANSPARENTCUTOUT = "TransparentCutOut";
            const string TRANSPARENT = "Transparent";
            const string RENDERTYPE = "RenderType";
            const string IGNOREPROJECTION = "IgnoreProjection";
            const string DO_IGNOREPROJECTION = "True";
            const string DONT_IGNOREPROJECTION = "False";
            var renderType = OPAQUE;
            var ignoreProjection = DONT_IGNOREPROJECTION;

            if (_Transparent_Setting == UTS3GUI.UTS_TransparentMode.On)
            {
                renderType = TRANSPARENT;
                ignoreProjection = DO_IGNOREPROJECTION;
            }
            else
            {
                switch (technique)
                {
                    case UTS3GUI.UTS_Mode.ThreeColorToon:
                        {
                            UTS3GUI.UTS_ClippingMode clippingMode = (UTS3GUI.UTS_ClippingMode)UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropClippingMode);
                            if (clippingMode == UTS3GUI.UTS_ClippingMode.Off)
                            {

                            }
                            else
                            {
                                renderType = TRANSPARENTCUTOUT;

                            }

                            break;
                        }
                    case UTS3GUI.UTS_Mode.ShadingGradeMap:
                        {
                            UTS3GUI.UTS_TransClippingMode transClippingMode = (UTS3GUI.UTS_TransClippingMode)UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropClippingMode);
                            if (transClippingMode == UTS3GUI.UTS_TransClippingMode.Off)
                            {
                            }
                            else
                            {
                                renderType = TRANSPARENTCUTOUT;

                            }

                            break;
                        }
                }

            }
            if (_autoRenderQueue == 1)
            {
                if (_Transparent_Setting == UTS3GUI.UTS_TransparentMode.On)
                {
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                }
                else if (stencilMode == UTS3GUI.UTS_StencilMode.StencilMask)
                {
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest - 1;
                }
                else if (stencilMode == UTS3GUI.UTS_StencilMode.StencilOut)
                {
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                }
            }
            else
            {
                material.renderQueue = _renderQueue;
            }

            material.SetOverrideTag(RENDERTYPE, renderType);
            material.SetOverrideTag(IGNOREPROJECTION, ignoreProjection);
        }
        void ApplyMatCapMode(Material material)
        {
            if (UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropClippingMode) == 0)
            {
                if (material.GetFloat( UTS3GUI.ShaderPropMatCap) == 1)
                    material.EnableKeyword( UTS3GUI.ShaderPropMatCap);
                else
                    material.DisableKeyword( UTS3GUI.ShaderPropMatCap);
            }
            else
            {
                material.DisableKeyword( UTS3GUI.ShaderPropMatCap);
            }
        }

        void ApplyAngelRing(Material material)
        {
            int angelRingEnabled = UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropAngelRing);
            if (angelRingEnabled == 0)
            {
                material.DisableKeyword(UTS3GUI.ShaderDefineANGELRING_ON);
                material.EnableKeyword(UTS3GUI.ShaderDefineANGELRING_OFF);
            }
            else
            {
                material.EnableKeyword(UTS3GUI.ShaderDefineANGELRING_ON);
                material.DisableKeyword(UTS3GUI.ShaderDefineANGELRING_OFF);

            }
        }

        void ApplyStencilMode(Material material)
        {
            UTS3GUI.UTS_StencilMode mode = (UTS3GUI.UTS_StencilMode)(UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropStencilMode));
            switch (mode)
            {
                case UTS3GUI.UTS_StencilMode.Off:
                    //    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilNo,0);
                    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilComp, (int)UTS3GUI.StencilCompFunction.Disabled);
                    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilOpPass, (int)UTS3GUI.StencilOperation.Keep);
                    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilOpFail, (int)UTS3GUI.StencilOperation.Keep);
                    break;
                case UTS3GUI.UTS_StencilMode.StencilMask:
                    //    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilNo,0);
                    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilComp, (int)UTS3GUI.StencilCompFunction.Always);
                    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilOpPass, (int)UTS3GUI.StencilOperation.Replace);
                    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilOpFail, (int)UTS3GUI.StencilOperation.Replace);
                    break;
                case UTS3GUI.UTS_StencilMode.StencilOut:
                    //    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilNo,0);
                    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilComp, (int)UTS3GUI.StencilCompFunction.NotEqual);
                    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilOpPass, (int)UTS3GUI.StencilOperation.Keep);
                    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilOpFail, (int)UTS3GUI.StencilOperation.Keep);

                    break;
            }



        }
        void ApplyClippingMode(Material material)
        {

            if (!IsShadingGrademap(material))
            {


                material.DisableKeyword(UTS3GUI.ShaderDefineIS_TRANSCLIPPING_OFF);
                material.DisableKeyword(UTS3GUI.ShaderDefineIS_TRANSCLIPPING_ON);

                switch (UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropClippingMode))
                {
                    case 0:
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_OFF);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_MODE);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_TRANSMODE);
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_OUTLINE_CLIPPING_NO);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_OUTLINE_CLIPPING_YES);
                        break;
                    case 1:
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_OFF);
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_MODE);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_TRANSMODE);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_OUTLINE_CLIPPING_NO);
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_OUTLINE_CLIPPING_YES);
                        break;
                    default:
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_OFF);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_MODE);
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_TRANSMODE);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_OUTLINE_CLIPPING_NO);
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_OUTLINE_CLIPPING_YES);
                        break;
                }
            }
            else
            {


                material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_OFF);
                material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_MODE);
                material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_TRANSMODE);
                switch (UTS3GUI.MaterialGetInt( material, UTS3GUI.ShaderPropClippingMode))
                {
                    case 0:
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_TRANSCLIPPING_OFF);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_TRANSCLIPPING_ON);
                        break;
                    default:
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_TRANSCLIPPING_OFF);
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_TRANSCLIPPING_ON);
                        break;

                }

            }

        }

        const string srpDefaultColorMask = "_SPRDefaultUnlitColorMask";
        const string srpDefaultCullMode = "_SRPDefaultUnlitColMode";



        void SetGameRecommendation(Material material)
        {


            material.SetFloat( UTS3GUI.ShaderPropIsLightColor_Base, 1);
            material.SetFloat( UTS3GUI.ShaderPropIs_LightColor_1st_Shade, 1);
            material.SetFloat( UTS3GUI.ShaderPropIs_LightColor_2nd_Shade, 1);
            material.SetFloat( UTS3GUI.ShaderPropIs_LightColor_HighColor, 1);
            material.SetFloat( UTS3GUI.ShaderPropIs_LightColor_RimLight, 1);
            material.SetFloat( UTS3GUI.ShaderPropIs_LightColor_Ap_RimLight, 1);
            material.SetFloat( UTS3GUI.ShaderPropIs_LightColor_MatCap, 1);
            if (material.HasProperty( UTS3GUI.ShaderPropAngelRing))
            {//When AngelRing is available
                material.SetFloat( UTS3GUI.ShaderPropIs_LightColor_AR, 1);
            }
            if (material.HasProperty( UTS3GUI.ShaderPropOutline))//OUTLINEがある場合.
            {
                material.SetFloat( UTS3GUI.ShaderPropIs_LightColor_Outline, 1);
            }
            material.SetFloat( UTS3GUI.ShaderPropSetSystemShadowsToBase, 1);
            material.SetFloat( UTS3GUI.ShaderPropIsFilterHiCutPointLightColor, 1);
            material.SetFloat( UTS3GUI.ShaderPropCameraRolling_Stabilizer, 1);
            material.SetFloat( UTS3GUI.ShaderPropIs_Ortho, 0);
            material.SetFloat( UTS3GUI.ShaderPropGI_Intensity, 0);
            material.SetFloat( UTS3GUI.ShaderPropUnlit_Intensity, 1);
            material.SetFloat( UTS3GUI.ShaderPropIs_Filter_LightColor, 1);
        }

        void ConvertIndex(int coreConverterIndex, int index)
        {
            if (!m_ConverterStates[coreConverterIndex].items[index].hasConverted)
            {
                m_ConverterStates[coreConverterIndex].items[index].hasConverted = true;
                var item = new ConverterItemInfo()
                {
                    index = index,
                    descriptor = m_ItemsToConvert[coreConverterIndex].itemDescriptors[index],
                };
                var ctx = new RunItemContext(item);
                m_CoreConvertersList[coreConverterIndex].OnRun(ref ctx);
                UpdateInfo(coreConverterIndex, ctx);
            }
        }

        void Convert(ClickEvent evt)
        {
        }

    }
}
#endif