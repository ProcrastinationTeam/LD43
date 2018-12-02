using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class RoomGenerator : EditorWindow
{
    string wallPrefabPath = "Assets/Stealth/Prefabs/Wall.prefab";
    bool groupEnabled;
    bool spacingLocked = true;
    
    float spacingX = 0.32f;
    float spacingY = 0.31f;

    int width = 8;
    int height = 8;
    int doorHeight = 4;

    bool doorLeft = false;
    bool doorRight = false;
    private string newPrefabPath = "Assets/Stealth/Prefabs/Rooms/";
    private string newPrefabName = "NewPrefab";
    GameObject goo;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Custom/RoomGenerator")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(RoomGenerator));
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        wallPrefabPath = EditorGUILayout.TextField("Wall Prefab Path", wallPrefabPath);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Unlock Spacing", groupEnabled);
        //spacingLocked = EditorGUILayout.Toggle("Lock Spacing", spacingLocked);

        spacingX = EditorGUILayout.Slider("X Spacing", spacingX, 0, 1);
        spacingY = EditorGUILayout.Slider("Y Spacing", spacingY, 0, 1);
        EditorGUILayout.EndToggleGroup();

        width = EditorGUILayout.IntSlider("Width", width, 4, 20);
        height = EditorGUILayout.IntSlider("Height", height, 4, 20);

        doorHeight = EditorGUILayout.IntSlider("Door Height", doorHeight, 1, 5);

        doorLeft = EditorGUILayout.Toggle("Door Left?", doorLeft);
        doorRight = EditorGUILayout.Toggle("Door Right?", doorRight);

        newPrefabPath = EditorGUILayout.TextField("Wall Prefab Path", newPrefabPath);
        newPrefabName = EditorGUILayout.TextField("Wall Prefab Name", newPrefabName);

        if (GUILayout.Button("Create Prefab"))
        {
            Debug.Log(goo);
            Object newPrefab = PrefabUtility.CreatePrefab(newPrefabPath + newPrefabName + ".prefab", goo);
            PrefabUtility.ReplacePrefab(goo, newPrefab, ReplacePrefabOptions.ConnectToPrefab);
        }

        newPrefabName = string.Format("Room_{0}_{1}_{2}_{3}_{4}", width, height, doorLeft ? "DL" : "NDL", doorRight ? "DR" : "NDR", doorHeight);


        Object wallPrefab = AssetDatabase.LoadAssetAtPath(wallPrefabPath, typeof(GameObject));
        DestroyImmediate(goo);
        goo = new GameObject();

        List<GameObject> gameObjects = new List<GameObject>();


        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    if (doorLeft && x == 0 && y > 0 && y <= doorHeight)
                    {
                        continue;
                    }
                    if(doorRight && x == width - 1 && y > 0 && y <= doorHeight)
                    {
                        continue;
                    }

                    GameObject clone = PrefabUtility.InstantiatePrefab(wallPrefab as GameObject) as GameObject;
                    clone.transform.position = new Vector3(clone.transform.position.x + spacingX * x, clone.transform.position.y + spacingY * y, clone.transform.position.z);
                    gameObjects.Add(clone);
                    clone.transform.parent = goo.transform;
                }
            }
        }
    }
}