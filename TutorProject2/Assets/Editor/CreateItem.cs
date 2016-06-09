using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateItem : Editor
{
    [MenuItem("Adventure Framework/Create Item")]
    static void CreateSOItem()
    {
        //Create an item asset

        //Save File Panel to find path/name
        string path = EditorUtility.SaveFilePanelInProject("Create Item", "NewItem.asset", "asset", "test");

        //error checking path
        if(path == null)
        {
            return;
        }

        //makes sure the path is in the directory

        //new instance of item
        Item newItem = CreateInstance<Item>();
        //new asset from item
        AssetDatabase.CreateAsset(newItem, path);
        //save the asset
        AssetDatabase.SaveAssets();
    }
}
