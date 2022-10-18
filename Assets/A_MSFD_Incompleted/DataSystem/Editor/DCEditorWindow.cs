using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using System.Linq;
namespace MSFD
{
    public class DCEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Data System/Data Display")]
        private static void OpenWindow()
        {
            GetWindow<DCEditorWindow>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.DefaultMenuStyle = OdinMenuStyle.TreeViewStyle;

            tree.Add("Menu Style", tree.DefaultMenuStyle);
            
            var allAssets = AssetDatabase.GetAllAssetPaths()
                .Where(x => x.StartsWith("Assets/A_Release/Data/"))
                .OrderBy(x => x);

            foreach (var path in allAssets)
            {
                tree.AddAssetAtPath(path.Substring("Assets/A_Release/Data/".Length), path, typeof(DisplayDataBase));
            }

            tree.EnumerateTree().AddThumbnailIcons();

            return tree;
        }
    }
}