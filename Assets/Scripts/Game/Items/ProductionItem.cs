using EpicMergeClone.Game.Mechanics.Grid;
using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    public class ProductionItem : ItemBase
    {
        public const string PRODUCTION_PREF_PREFIX = "Producer_";
        public const string PRODUCTION_PREF_SUFFIX = "_CurrentEnergy";
        public ProductionItemSO ItemData => ItemDataSO as ProductionItemSO;

        private int m_CurrentEnergy;

        public override void LateInitialize()
        {
            base.LateInitialize();

            m_CurrentEnergy = LoadEnergyState();
        }

        protected override void OnClick()
        {
            base.OnClick();
            Clear();
        }

        public void Clear()
        {
            //if(m_GameStateManager.CurrentEnergy )

            if(m_GameStateManager.CurrentEnergy < CurrentRequiredPlayerEnergy())
            {
                Debug.Log("You don't have sufficient energy");
                return;
            }

            if(m_GameStateManager.CurrentWorkers <= 0)
            {
                Debug.Log("You don't have available worker.");
                return;
            }

            m_GameStateManager.CurrentEnergy -= CurrentRequiredPlayerEnergy();
            m_CurrentEnergy--;

            CreateItems();
            SaveState();
            TryDespawn();
        }

        private void CreateItems()
        {
            var itemsToCreate = ItemData.ItemsToCreate;
            int randomNumberOfItems = Random.Range(4, 6);

            List<Cell> visitedCells = new List<Cell>();

            for (int i = 0; i < randomNumberOfItems; i++)
            {
                int randomItemIndex = Random.Range(0, itemsToCreate.Length);
                var newItem = m_ItemPoolManager.SpawnItem(itemsToCreate[randomItemIndex]);
                var cell = CurrentCell.GetFirstAvailableNeighbour(visitedCells);

                cell.AddItem(newItem);
            }
        }

        private void TryDespawn()
        {
            if (m_CurrentEnergy != 0) return;

            PlayerPrefs.DeleteKey(PRODUCTION_PREF_PREFIX + ItemData.ItemId + CurrentCell.ToString() + PRODUCTION_PREF_SUFFIX);
            m_ItemPoolManager.DespawnItem(this);
        }

        private int CurrentRequiredPlayerEnergy()
        {
            Debug.Log(ItemData.PlayerEnergyRequiresForClear.Length - m_CurrentEnergy);
            return ItemData.PlayerEnergyRequiresForClear[ItemData.PlayerEnergyRequiresForClear.Length - m_CurrentEnergy];
        }

        private void SaveState()
        {
            PlayerPrefs.SetInt(PRODUCTION_PREF_PREFIX + ItemData.ItemId + CurrentCell.ToString() + PRODUCTION_PREF_SUFFIX, m_CurrentEnergy);
        }

        private int LoadEnergyState()
        {
            return PlayerPrefs.GetInt(PRODUCTION_PREF_PREFIX + ItemData.ItemId + CurrentCell.ToString() + PRODUCTION_PREF_SUFFIX, ItemData.ItemEnergy);
        }
    }
}