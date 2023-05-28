using Core;
using Services;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Zenject
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private DialogHelper _dialogHelper;

        public override void InstallBindings()
        {
            Container.Bind<DialogHelper>().FromInstance(_dialogHelper).AsSingle();
            Container.BindInterfacesAndSelfTo<SessionDataController>().AsSingle();
            Container.BindInterfacesAndSelfTo<DataHelper>().AsSingle();
        }

        [Button]
        public void RenameIcons()
        {
            //  var icons = AssetDatabase.LoadAllAssetsAtPath($"Assets/Sprites/IconsElement");

            string folderPath = "Assets/Sprites/IconsElement"; // Укажите путь к папке, где находятся объекты

            string[] guids = AssetDatabase.FindAssets("", new[] { folderPath });

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                Sprite obj = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);

                if (obj != null)
                {
                    string objectName = obj.name;

                    if (char.IsLower(objectName[0]))
                    {
                        EditorUtility.SetDirty(obj);
                        string convertedName = char.ToUpper(objectName[0]) + objectName.Substring(1);
                        obj.name = convertedName;
                        Debug.Log($"Converted object name: {objectName} -> {convertedName}");
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
    }
}