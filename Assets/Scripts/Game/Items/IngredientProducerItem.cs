using EpicMergeClone.Utils;
using System;
using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    public class IngredientProducerItem : ItemBase
    {
        public const string PRODUCE_ITEM_START_TIME_PREFIX = "Producing_";

        public IngredientProducerItemSO ItemData => ItemDataSO as IngredientProducerItemSO;

        protected override void Start()
        {
            base.Start();

            if (!PlayerPrefs.HasKey(PRODUCE_ITEM_START_TIME_PREFIX + ItemData.ItemId + CurrentCell))
            {
                PlayerPrefsStorage.SetDateTime(PRODUCE_ITEM_START_TIME_PREFIX + ItemData.ItemId + CurrentCell, DateTime.MinValue);
                return;
            }
        }

        protected override void OnClick()
        {
            base.OnClick();

            if (!IsProducedItems())
                return;

            Collect();
        }

        private void Collect()
        {
            ProduceCollectibles();
            StartProduce();
        }

        private void ProduceCollectibles()
        {
            for (int i = 0; i < ItemData.ProduceAmount; i++)
            {
                var newCollectible = m_ItemPoolManager.SpawnItem(ItemData.ItemToProduce);
                var cell = CurrentCell.GetFirstAvailableNeighbour();

                cell.AddItem(newCollectible, transform.position, cell.transform.position);
            }
        }

        private TimeSpan GetLeftTime()
        {
            return TimeSpan.FromTicks(DateTime.UtcNow.Ticks - PlayerPrefsStorage.GetDateTime(PRODUCE_ITEM_START_TIME_PREFIX + ItemData.ItemId + CurrentCell, DateTime.MinValue).Ticks);
        }

        private void StartProduce()
        {
            PlayerPrefsStorage.SetDateTime(PRODUCE_ITEM_START_TIME_PREFIX + ItemData.ItemId + CurrentCell, DateTime.UtcNow);
        }

        private bool IsProducedItems()
        {
            return PlayerPrefsStorage.GetDateTime(PRODUCE_ITEM_START_TIME_PREFIX + ItemData.ItemId + CurrentCell, DateTime.MinValue).Ticks + TimeSpan.FromSeconds(ItemData.ProduceTimeSeconds).Ticks <= DateTime.UtcNow.Ticks;
        }
    }
}
