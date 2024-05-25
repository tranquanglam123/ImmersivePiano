using System.Linq;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class FolderMerger : EditorWindow
{
   [MenuItem("Asset/Merge Folders/Script to Scripts")]
   public static void MergeScriptToScripts()
    {
        string sourcePath = AssetDatabase.GetAssetPath(Selection.activeObject); // "Srcipt" folder
        string targetPath = "Assets/Scripts"; // "Scripts" folder

        // Ensure source and target folders exist
        if (!AssetDatabase.IsValidFolder(sourcePath) || !AssetDatabase.IsValidFolder(targetPath))
        {
            Debug.LogError("Invalid source or target folder path!");
            return;
        }

        // Get all files and folders within the source path
        string[] allAssets = AssetDatabase.GetAllAssetPaths().Where(path => path.StartsWith(sourcePath)).ToArray();

        foreach (string assetPath in allAssets)
        {
            // Move the asset to the target path
            string newPath = assetPath.Replace(sourcePath, targetPath);
            AssetDatabase.MoveAsset(assetPath, newPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Scripts folder merged successfully!");
    }
}
