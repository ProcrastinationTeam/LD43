using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor
{
    [CustomGridBrush(true, false, false, "InteractibleObject")]
    public class InteractibleObjectBrush : GridBrush
    {
        public GameObject objectPrefab;
        Transform interactivObjectTilemap;

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var obj = PrefabUtility.InstantiatePrefab(objectPrefab) as GameObject;
            obj.transform.position = gridLayout.LocalToWorld(gridLayout.CellToLocal(position));
            obj.transform.position = new Vector3(obj.transform.position.x + 0.5f, obj.transform.position.y, obj.transform.position.z); // Technique de sioux

            interactivObjectTilemap = GameObject.Find("InteractibleObjects").transform;
            obj.transform.SetParent(interactivObjectTilemap.transform);

            Undo.RegisterCreatedObjectUndo(obj, "Create");
        }

        public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            base.BoxFill(gridLayout, brushTarget, position);
        }

        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var objPosList = brushTarget.GetComponentsInChildren<Transform>();
            foreach (var obj in objPosList)
            {
                if (obj.transform.position == gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(position)))
                {
                    DestroyImmediate(obj.gameObject);
                    break;
                }
            }
        }
    }
}
