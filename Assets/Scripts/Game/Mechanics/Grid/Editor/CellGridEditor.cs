using UnityEditor;
using UnityEngine;

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
                t.CreateGrid();
            }
        }
    }
}