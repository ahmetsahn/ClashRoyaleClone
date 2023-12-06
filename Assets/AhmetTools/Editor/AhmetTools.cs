using UnityEditor;
using UnityEngine;
using static System.IO.Path;
using static System.IO.Directory;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;

namespace AhmetTools.Editor
{
    public static class AhmetTools
    {
        [MenuItem("AhmetTools/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            Dir("Scripts/Runtime", "Signals", "Installers", "UISystem", "Enums", "Interfaces", "AudioSystem");
            Dir("Prefabs","UIPanels");
            Dir("Plugins");
            Dir("Data");
            Dir("Audio");
            Dir("Arts", "Animations", "Materials", "Models", "Textures");
            Refresh();
        }

        private static void Dir(string path)
        {
            var fullPath = Combine(dataPath, path);
            CreateDirectory(fullPath);
        }

        private static void Dir(string path, params string[] dir)
        {
            var fullPath = Combine(dataPath, path);
            foreach (var d in dir)
            {
                
                CreateDirectory(Combine(fullPath, d));
            }
        }
        
        [MenuItem("AhmetTools/Create Hierarchy")]
        
        public static void CreateHierarchy()
        {
            var camera = GameObject.Find("Main Camera");
            var light = GameObject.Find("Directional Light");
            var globalVolume = GameObject.Find("Global Volume");

            new GameObject("Setup");
            new GameObject("GameRoot");
            new GameObject("UIRoot");
            new GameObject("Systems");
            var levelRoot = new GameObject("LevelRoot");
            
            camera?.transform.SetParent(GameObject.Find("Setup").transform);
            light?.transform.SetParent(GameObject.Find("Setup").transform);
            globalVolume?.transform.SetParent(GameObject.Find("Setup").transform);
            levelRoot.transform.SetParent(GameObject.Find("GameRoot").transform);
        }
        
        [MenuItem("AhmetTools/Assets/General/Zenject")]
        public static void ImportZenjectAssets()
        {
            OpenURL(@"https://github.com/modesttree/Zenject/archive/refs/heads/master.zip");
        }
    }
}