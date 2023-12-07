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

        public void AddItem(CollectibleItem item, int count = 1)
        {
            InventoryItemData itemData = new InventoryItemData()
            {
                itemId = item.ItemDataSO.UniqueId,
                count = count
            };

            var existingItemData = inventoryData.Items.Find(data => data.itemId == item.ItemDataSO.UniqueId);
            if (existingItemData != null)
            {
                existingItemData.count += count;
            }
            else
            {
                inventoryData.Items.Add(itemData);
            }

            SaveInventoryState();
        }

        public void RemoveItem(CollectibleItem item, int count = 1)
        {
            var itemData = inventoryData.Items.Find(data => data.itemId == item.ItemDataSO.UniqueId);
            if (itemData != null)
            {
                itemData.count -= count;

                if (itemData.count <= 0)
                {
                    inventoryData.Items.Remove(itemData);
                }

                SaveInventoryState();
            }
        }

        public bool IsContainItem(string itemUniqueId)
        {
            return inventoryData.Items.Exists(data => data.itemId == itemUniqueId);
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

            return JsonUtility.FromJson<InventoryData>(jsonString) ?? new InventoryData();
        }

        public class InventoryData
        {
            public List<InventoryItemData> Items;

            public InventoryData()
            {
                Items = new List<InventoryItemData>();
            }
        }

        [System.Serializable]
        public class InventoryItemData
        {
            public string itemId;
            public int count;
        }
    }
}
