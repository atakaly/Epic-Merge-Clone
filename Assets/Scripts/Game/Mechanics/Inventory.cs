using EpicMergeClone.Game.Items;
using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.Game.Mechanics.Inventory
{
    public class Inventory
    {
        public const string INVENTORY_STATE_PREF_NAME = "inventory_state";

        private InventoryData inventoryData;

        public Inventory()
        {
            inventoryData = LoadInventoryState();
        }

        public void AddItem(CollectibleItem item)
        {
            GameState.ItemData itemData = new GameState.ItemData()
            {
                itemId = item.ItemDataSO.UniqueId
            };

            inventoryData.Items.Add(itemData);

            SaveInventoryState();
        }

        public void RemoveItem(CollectibleItem item)
        {
            var itemData = inventoryData.Items.Find(data => data.itemId == item.ItemDataSO.UniqueId);
            inventoryData.Items.Remove(itemData);

            SaveInventoryState();
        }

        public bool IsContainItem(string itemUniqueId)
        {
            for (int i = 0; i < inventoryData.Items.Count; i++)
            {
                if (inventoryData.Items[i].itemId == itemUniqueId)
                {
                    return true;
                }
            }

            return false;
        }

        public void SaveInventoryState()
        {
            string jsonString = JsonUtility.ToJson(inventoryData);

            PlayerPrefs.SetString(INVENTORY_STATE_PREF_NAME, jsonString);
            PlayerPrefs.Save();
        }

        public InventoryData LoadInventoryState()
        {
            string jsonString = PlayerPrefs.GetString(INVENTORY_STATE_PREF_NAME, "{}");

            return JsonUtility.FromJson<InventoryData>(jsonString);
        }

        public class InventoryData
        {
            public List<GameState.ItemData> Items;

            public InventoryData()
            {
                Items = new List<GameState.ItemData>();
            }
        }
    }
}