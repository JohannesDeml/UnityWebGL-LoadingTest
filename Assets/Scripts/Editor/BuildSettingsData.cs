using System.Linq;
using NUnit.Framework;
#if UNITY_6000_1_OR_NEWER
using Unity.Web.Stripping.Editor;
#endif
using UnityEditor;
using UnityEngine;

namespace UnityBuilderAction
{
    
    [CreateAssetMenu(menuName = "BuildSettingsData", fileName = "BuildSettingsData", order = 0)]
    public class BuildSettingsData : ScriptableObject
    {
        private static BuildSettingsData _instance;
        
        public static BuildSettingsData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = AssetDatabase.FindAssets($"t:{nameof(BuildSettingsData)}")
                        .Select(guid => AssetDatabase.LoadAssetAtPath<BuildSettingsData>(AssetDatabase.GUIDToAssetPath(guid)))
                        .FirstOrDefault();
                    Assert.IsNotNull(_instance, "BuildSettingsData not found, create it first.");
                }
                return _instance;
            }
        }
        
#if UNITY_6000_1_OR_NEWER
        [SerializeField]
        private SubmoduleStrippingSettings webSubmoduleStrippingSettings;
		
        public SubmoduleStrippingSettings WebSubmoduleStrippingSettings => webSubmoduleStrippingSettings;
#endif
    }
}