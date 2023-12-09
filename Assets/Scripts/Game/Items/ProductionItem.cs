using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    public class ProductionItem : ItemBase
    {
        public const string PRODUCTION_PREF_PREFIX = "Producer_";
        public const string PRODUCTION_PREF_SUFFIX = "_CurrentEnergy";
        public ProductionItemSO ItemData => ItemDataSO as ProductionItemSO;

        private int m_CurrentEnergy;

        public override void InitializeItem(ItemDataSO itemData)
        {
            base.InitializeItem(itemData);

            m_CurrentEnergy = LoadEnergyState();
        }

        public void Clear()
        {
            //Check for current energy && worker, if sufficient, clear and attach the worker.
            m_CurrentEnergy--;

            SaveState();
        }

        private void SaveState()
        {
            PlayerPrefs.SetInt(PRODUCTION_PREF_PREFIX + ItemData.ItemId + CurrentCell.ToString() + PRODUCTION_PREF_SUFFIX, m_CurrentEnergy);
        }

        private int LoadEnergyState()
        {
            return PlayerPrefs.GetInt(PRODUCTION_PREF_PREFIX + ItemData.ItemId + CurrentCell.ToString() + PRODUCTION_PREF_SUFFIX);
        }
    }
}