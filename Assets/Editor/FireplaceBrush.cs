using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor
{
    [CustomGridBrush(true, false, false, "Fireplace")]
    public class FireplaceBrush : GridBrush
    {
        public GameObject FirePrefab;
        Transform FireplaceTilemap;

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var obj = PrefabUtility.InstantiatePrefab(FirePrefab) as GameObject;
            obj.transform.position = gridLayout.LocalToWorld(gridLayout.CellToLocal(position));
            obj.transform.position = new Vector3(obj.transform.position.x + 0.5f, obj.transform.position.y, obj.transform.position.z); // Technique de sioux

            FireplaceTilemap = GameObject.Find("Fireplaces").transform;
            obj.transform.SetParent(FireplaceTilemap.transform);

            Undo.RegisterCreatedObjectUndo(obj, "Create");
        }

        public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            base.BoxFill(gridLayout, brushTarget, position);
        }

        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var FireplacePosList = brushTarget.GetComponentsInChildren<Transform>();
            foreach (var fireplace in FireplacePosList)
            {

                if (fireplace.transform.position == gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(position)))
                {
                    DestroyImmediate(fireplace.gameObject);
                    break;
                }
            }
        }
    }
}
