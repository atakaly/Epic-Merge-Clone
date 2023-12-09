using EpicMergeClone.Game.Mechanics.Grid;
using EpicMergeClone.Game.Player;
using EpicMergeClone.Utils;
using UnityEngine;

namespace EpicMergeClone
{
    public class GameState
    {
        public const string GRID_STATE_PREF_NAME = "grid_state";
        public const string PLAYER_STATE_PREF_NAME = "player_state";

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
            public ItemData itemData;
        }

        [System.Serializable]
        public class ItemData
        {
            public string itemId;
        }

        public static void SaveGridState(GridData gridData)
        {
            string jsonString = JsonUtility.ToJson(gridData);

            PlayerPrefsStorage.SetString(GRID_STATE_PREF_NAME, jsonString);
        }

        public static GridData LoadGridData()
        {
            string jsonString = PlayerPrefsStorage.GetString(GRID_STATE_PREF_NAME);

            if (string.IsNullOrEmpty(jsonString))
                return new GridData();

            return JsonUtility.FromJson<GridData>(jsonString);
        }

        public static void SavePlayerData(PlayerData playerState)
        {
            string jsonString = JsonUtility.ToJson(playerState);

            PlayerPrefsStorage.SetString(PLAYER_STATE_PREF_NAME, jsonString);
        }

        public static PlayerData LoadPlayerData()
        {
            string jsonString = PlayerPrefsStorage.GetString(PLAYER_STATE_PREF_NAME);

            if (string.IsNullOrEmpty(jsonString))
                return new PlayerData();

            return JsonUtility.FromJson<PlayerData>(jsonString);
        }

    }
}