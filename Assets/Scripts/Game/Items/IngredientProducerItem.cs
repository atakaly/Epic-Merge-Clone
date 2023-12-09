using EpicMergeClone.Game.Mechanics.Grid;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    public class IngredientProducerItem : ItemBase
    {
        public const string PRODUCE_ITEM_START_TIME_PREFIX = "Producing_";

        public IngredientProducerItemSO ItemData => ItemDataSO as IngredientProducerItemSO;

        private bool m_IsCollected;

        private void Start()
        {
            Debug.Log(Guid.NewGuid().ToString());
            //StartProduce();
        }

        protected override void OnClick()
        {
            if (IsProducing())
                return;

            if (m_IsCollected)
                return;

            Collect();
        }

        private void Collect()
        {
            ProduceCollectibles();
            m_IsCollected = true;
            StartProduce();
        }

        private void ProduceCollectibles()
        {
            List<Cell> visitedCells = new List<Cell>();

            for (int i = 0; i < ItemData.ProduceAmount; i++)
            {
                var newCollectible = m_ItemPoolManager.SpawnItem(ItemData.ItemToProduce);
                var cell = CurrentCell.GetFirstAvailableNeighbour(visitedCells);

                cell.AddItem(newCollectible);
            }
        }

        public void StartProduce()
        {
            if (IsProducing())
                return;

            long currenTimeInTicks = DateTime.Now.Ticks;
            PlayerPrefs.SetString(PRODUCE_ITEM_START_TIME_PREFIX + ItemData.ItemId, currenTimeInTicks.ToString());
            m_IsCollected = false;
        }

        public int LeftTimeToProduce()
        {
            //long startTime = long.Parse(PlayerPrefs.GetString());

            return (int)DateTime.Now.Ticks - (PlayerPrefs.GetInt(PRODUCE_ITEM_START_TIME_PREFIX + ItemData.ItemId) + ItemData.ProduceTimeSeconds);
        }

        public bool IsProducing()
        {
            return PlayerPrefs.GetInt(PRODUCE_ITEM_START_TIME_PREFIX + ItemData.ItemId) + ItemData.ProduceTimeSeconds > DateTime.Now.Ticks;
        }
    }
}