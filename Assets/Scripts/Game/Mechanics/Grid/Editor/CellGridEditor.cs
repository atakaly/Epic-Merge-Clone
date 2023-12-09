using log4net.Util;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace EpicMergeClone.Game.Mechanics.Grid
{
    [CustomEditor(typeof(CellGrid))]
    public class CellGridEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CellGrid t = (CellGrid)target;

            if(GUILayout.Button("Create Grid"))
            {
                CreateGrid();
            }

            if(GUILayout.Button("Save Grid State"))
            {
                t.SaveCurrentGridState();
            }
        }

        public void CreateGrid()
        {
            ClearBoard();

            CellGrid t = (CellGrid)target;

            float cellWidth = 1.2f;
            float cellHeight = .27f;

            for (int i = 0; i < t.width; i++)
            {
                for (int j = 0; j < t.heigth; j++)
                {
                    float xPos = i * cellWidth;
                    float yPos = j * cellHeight;
                    xPos += (j % 2 == 1) ? cellWidth * 0.5f : 0;

                    var newCell = PrefabUtility.InstantiatePrefab(t.cellPrefab) as Cell;
                    newCell.transform.position = new Vector3(xPos, yPos, 0);
                    newCell.transform.SetParent(t.transform);
                    newCell.X = i;
                    newCell.Y = j;
                    newCell.State = CellState.Available;

                    t.m_Cells.Add(newCell);
                }
            }

            t.SetNeighbours();
        }

        private void ClearBoard()
        {
            CellGrid t = (CellGrid)target;

            for (int i = 0; i < t.m_Cells.Count; i++)
            {
                DestroyImmediate(t.m_Cells[i]);
            }

            t.m_Cells.Clear();
        }
    }
}