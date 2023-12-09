using UnityEngine;

namespace EpicMergeClone.Game.Player
{
    [CreateAssetMenu(menuName = "Player/All Player Levels Data")]
    public class AllPlayerLevelsData : ScriptableObject
    {
        public PlayerLevelData[] PlayerLevels;

        public PlayerLevelData GetPlayerLevelData(int currentLevel)
        {
            if(currentLevel >= PlayerLevels.Length)
            {
                return PlayerLevels[PlayerLevels.Length - 1];
            }

            return PlayerLevels[currentLevel];
        }
    }
}