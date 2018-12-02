﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor
{
    [CustomGridBrush(true, false, false, "Door")]
    public class DoorBrush : GridBrush
    {
        public GameObject doorPrefab;
        public Transform interactivObjectTilemap;

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var door = PrefabUtility.InstantiatePrefab(doorPrefab) as GameObject;
            door.transform.position = gridLayout.LocalToWorld(gridLayout.CellToLocal(position));

            interactivObjectTilemap = GameObject.Find("EnemyTilemap").transform;
            door.transform.SetParent(interactivObjectTilemap.transform);

            Undo.RegisterCreatedObjectUndo(door, "Create");
        }

        public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            base.BoxFill(gridLayout, brushTarget, position);
        }

        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var doorPosList = brushTarget.GetComponentsInChildren<Transform>();
            foreach (var door in doorPosList)
            {

                if (door.transform.position == gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(position)))
                {
                    DestroyImmediate(door.gameObject);
                    break;
                }
            }
        }
    }
}