using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor
{
    [CustomGridBrush(true, false, false, "Elevator")]
    public class ElevatorBrush : GridBrush
    {
        public GameObject ElevatorPrefab;
        Transform ElevatorTilemap;

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var obj = PrefabUtility.InstantiatePrefab(ElevatorPrefab) as GameObject;
            obj.transform.position = gridLayout.LocalToWorld(gridLayout.CellToLocal(position));
            obj.transform.position = new Vector3(obj.transform.position.x + 0.5f, obj.transform.position.y, obj.transform.position.z); // Technique de sioux

            ElevatorTilemap = GameObject.Find("Elevators").transform;
            obj.transform.SetParent(ElevatorTilemap.transform);

            Undo.RegisterCreatedObjectUndo(obj, "Create");
        }

        public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            base.BoxFill(gridLayout, brushTarget, position);
        }

        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var ElevatorsPosList = brushTarget.GetComponentsInChildren<Transform>();
            foreach (var elevator in ElevatorsPosList)
            {

                if (elevator.transform.position == gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(position)))
                {
                    DestroyImmediate(elevator.gameObject);
                    break;
                }
            }
        }
    }
}
