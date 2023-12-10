using EpicMergeClone.Game.Items;
using EpicMergeClone.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.Game.Mechanics.InventorySystem
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
                itemId = item.ItemDataSO.ItemId,
                count = count
            };

            var existingItemData = inventoryData.Items.Find(data => data.itemId == item.ItemDataSO.ItemId);
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

        public void RemoveItem(CollectibleItemSO item, int count = 1)
        {
            var itemData = inventoryData.Items.Find(data => data.itemId == item.ItemId);
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

        public int GetItemCount(string itemUniqueId)
        {
            if (!IsContainItem(itemUniqueId))
                return 0;

            for (int i = 0; i < inventoryData.Items.Count; i++)
            {
                if (inventoryData.Items[i].itemId == itemUniqueId)
                {
                    return inventoryData.Items[i].count;
                }
            }

            return 0;
        }

        public void SaveInventoryState()
        {
            string jsonString = JsonUtility.ToJson(inventoryData);

            PlayerPrefsStorage.SetString(INVENTORY_STATE_PREF_NAME, jsonString);
        }

        public InventoryData LoadInventoryState()
        {
            string jsonString = PlayerPrefsStorage.GetString(INVENTORY_STATE_PREF_NAME, "{}");

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
