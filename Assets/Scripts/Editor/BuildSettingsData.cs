using System.Linq;
using NUnit.Framework;
using Unity.Web.Stripping.Editor;
using UnityEditor;
using UnityEngine;

namespace UnityBuilderAction
{
    
    [CreateAssetMenu(menuName = "Create BuildSettingsData", fileName = "BuildSettingsData", order = 0)]
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
        
        [SerializeField]
        private SubmoduleStrippingSettings webSubmoduleStrippingSettings;
		
        public SubmoduleStrippingSettings WebSubmoduleStrippingSettings => webSubmoduleStrippingSettings;
    }
}