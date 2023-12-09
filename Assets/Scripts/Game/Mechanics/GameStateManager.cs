using EpicMergeClone.Game.Player;
using System;

namespace EpicMergeClone.Game.Mechanics
{
    public class GameStateManager
    {
        public event Action OnStateUpdate;

        private PlayerData m_PlayerData;

        public int Level
        {
            get
            {
                 return m_PlayerData.Level;
            }
            set
            {
                m_PlayerData.Level = value;
                OnPlayerDataUpdate();
            }
        }

        public float Experience
        {
            get
            {
                return m_PlayerData.Experience;
            }
            set
            {
                m_PlayerData.Experience = value;
                OnPlayerDataUpdate();
            }
        }

        public int Coin
        {
            get
            {
                return m_PlayerData.Coin;
            }
            set
            {
                m_PlayerData.Coin = value;
                OnPlayerDataUpdate();
            }
        }

        public int CurrentEnergy
        {
            get
            {
                return m_PlayerData.CurrentEnergy;
            }
            set
            {
                m_PlayerData.CurrentEnergy = value;
                OnPlayerDataUpdate();
            }
        }

        public int MaxEnergy
        {
            get
            {
                return m_PlayerData.MaxEnergy;
            }
            set
            {
                m_PlayerData.MaxEnergy = value;
                OnPlayerDataUpdate();
            }
        }

        public GameStateManager()
        {
            m_PlayerData = GameState.LoadPlayerData();
        }

        private void OnPlayerDataUpdate()
        {
            GameState.SavePlayerData(m_PlayerData);
            OnStateUpdate?.Invoke();
        }
    }
}