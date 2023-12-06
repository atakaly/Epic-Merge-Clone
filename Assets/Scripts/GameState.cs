using EpicMergeClone.Game.Mechanics.Grid;
using UnityEngine;

namespace EpicMergeClone
{
    public class GameState
    {
        public const string GRID_STATE_PREF_NAME = "grid_state";

        [System.Serializable]
        public class GridData
        {
            public CellData[] cells;
        }

        [System.Serializable]
        public class CellData
        {
            public int x;
            public int y;
            public CellState state;
            public ItemBaseData itemData;
        }

        [System.Serializable]
        public class ItemBaseData
        {
            public string itemId;
            public int itemType; //Production etc. 
        }

        public static void SaveGridState(GridData gridData)
        {
            string jsonString = JsonUtility.ToJson(gridData);

            PlayerPrefs.SetString(GRID_STATE_PREF_NAME, jsonString);
            PlayerPrefs.Save();
        }

        public static GridData LoadGridData()
        {
            string jsonString = PlayerPrefs.GetString(GRID_STATE_PREF_NAME);

            if (string.IsNullOrEmpty(jsonString))
                return new GridData();

            return JsonUtility.FromJson<GridData>(jsonString);
        }
    }
}