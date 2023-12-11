using EpicMergeClone.Game.Player;
using EpicMergeClone.Installers;
using EpicMergeClone.UI;
using EpicMergeClone.Utils;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace EpicMergeClone.Game.Mechanics
{
    public class GameStateManager : MonoBehaviour
    {
        public const string LAST_ENERGY_UPDATE_TIME_PREF_NAME = "LastEnergyUpdateTime";
        private const float ENERGY_INCREMENT_INTERVAL_SECONDS = 2f;

        public event Action OnStateUpdate;

        private PlayerData m_PlayerData;

        private DateTime lastEnergyUpdateTime;
        private WaitForSeconds waitTime;

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
                TryAddLevel();
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

        public int CurrentWorkers
        {
            get
            {
                return m_PlayerData.CurrentWorkerCount;
            }
            set
            {
                m_PlayerData.CurrentWorkerCount = value;
                OnPlayerDataUpdate();
            }
        }

        public int MaxWorkers
        {
            get
            {
                return m_PlayerData.MaxWorkerCount;
            }
            set
            {
                m_PlayerData.MaxWorkerCount = value;
                OnPlayerDataUpdate();
            }
        }

        public float NeedExperience
        {
            get
            {
                return m_GlobalGameData.allPlayerLevelsData.PlayerLevels[Level].NeedExperience;
            }
        }

        private UIManager m_UIManager;
        private GlobalGameData m_GlobalGameData;

        [Inject]
        public void Construct(UIManager uiManager, GlobalGameData globalGameData)
        {
            m_UIManager = uiManager;
            m_GlobalGameData = globalGameData;
        }

        public void Awake()
        {
            m_PlayerData = GameState.LoadPlayerData();

            TimeSpan timePassed = DateTime.UtcNow - GetLastEnergyUpdateTime();
            int increments = Mathf.FloorToInt((float)timePassed.TotalSeconds / ENERGY_INCREMENT_INTERVAL_SECONDS);
            m_PlayerData.CurrentEnergy += increments;
            m_PlayerData.CurrentEnergy = Mathf.Min(m_PlayerData.CurrentEnergy, m_PlayerData.MaxEnergy);

            SetLastEnergyUpdateTime(DateTime.UtcNow);
            waitTime = new WaitForSeconds(ENERGY_INCREMENT_INTERVAL_SECONDS);
            StartCoroutine(IncrementEnergyCoroutine());

            UpdateUI();
        }

        private DateTime GetLastEnergyUpdateTime()
        {
            return PlayerPrefsStorage.GetDateTime(LAST_ENERGY_UPDATE_TIME_PREF_NAME, DateTime.UtcNow);
        }

        private void SetLastEnergyUpdateTime(DateTime time)
        {
            PlayerPrefsStorage.SetDateTime(LAST_ENERGY_UPDATE_TIME_PREF_NAME, time);
        }

        private IEnumerator IncrementEnergyCoroutine()
        {
            while (true)
            {
                yield return waitTime;

                if (m_PlayerData.CurrentEnergy < m_PlayerData.MaxEnergy)
                {
                    if (DateTime.UtcNow - lastEnergyUpdateTime >= TimeSpan.FromSeconds(ENERGY_INCREMENT_INTERVAL_SECONDS))
                    {
                        m_PlayerData.CurrentEnergy++;
                        OnPlayerDataUpdate();
                        lastEnergyUpdateTime = DateTime.UtcNow;
                        SetLastEnergyUpdateTime(lastEnergyUpdateTime);
                    }
                }
            }
        }

        private void TryAddLevel()
        {
            if (Experience >= NeedExperience)
            {
                var modExp = Experience % NeedExperience;
                Level++;
                Debug.Log(modExp);
                Experience = modExp;
            }
        }

        private void OnPlayerDataUpdate()
        {
            GameState.SavePlayerData(m_PlayerData);
            OnStateUpdate?.Invoke();
            UpdateUI();
        }

        private void UpdateUI()
        {
            m_UIManager.UpperBarUIController.UpdateEnergyText(CurrentEnergy, MaxEnergy);
            m_UIManager.UpperBarUIController.UpdateExperience(Experience, NeedExperience);
            m_UIManager.UpperBarUIController.UpdateLevelText(Level);
            m_UIManager.UpperBarUIController.UpdateWorkerText(CurrentWorkers, MaxWorkers);
            m_UIManager.UpperBarUIController.UpdateCoinText(Coin);
        }
    }
}