using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor
{
    [CustomGridBrush(true, false, false, "InteractibleObject")]
    public class InteractibleObjectBrush : GridBrush
    {
        Transform tilemap;

        string prefabPath = "Assets/Prefabs/Table.prefab";
        string parentGameObjectName = "InteractibleObjects";

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            GameObject prefab = (GameObject) AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));

            var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            obj.transform.position = gridLayout.LocalToWorld(gridLayout.CellToLocal(position));
            obj.transform.position = new Vector3(obj.transform.position.x + 0.5f, obj.transform.position.y, obj.transform.position.z); // Technique de sioux

            tilemap = GameObject.Find(parentGameObjectName).transform;
            obj.transform.SetParent(tilemap.transform);

            Undo.RegisterCreatedObjectUndo(obj, "Create");
        }

        public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            base.BoxFill(gridLayout, brushTarget, position);
        }

        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var gameObjectsPosList = brushTarget.GetComponentsInChildren<Transform>();
            foreach (var gameobject in gameObjectsPosList)
            {
                if (gameobject.transform.position == gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(position)))
                {
                    DestroyImmediate(gameobject.gameObject);
                    break;
                }
            }
        }
    }
}
