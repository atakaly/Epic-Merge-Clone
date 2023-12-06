using EpicMergeClone.Game.Items;
using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.Game.Mechanics.Inventory
{
    public class Inventory
    {
        public const string INVENTORY_STATE_PREF_NAME = "inventory_state";

        List<GameState.ItemData> Items;

        public Inventory()
        {
            Items = new List<GameState.ItemData>();
        }

        public void AddItem(CollectibleItem item)
        {
            GameState.ItemData itemData = new GameState.ItemData()
            {
                itemId = item.ItemDataSO.UniqueId
            };

            Items.Add(itemData);

            SaveInventoryState();
        }

        public void RemoveItem(CollectibleItem item)
        {
            var itemData = Items.Find(data => data.itemId == item.ItemDataSO.UniqueId);
            Items.Remove(itemData);

            SaveInventoryState();
        }

        public bool IsContainItem(string itemUniqueId)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].itemId == itemUniqueId)
                {
                    return true;
                }
            }

            return false;
        }

        public void SaveInventoryState()
        {
            string jsonString = JsonUtility.ToJson(Items);

            PlayerPrefs.SetString(INVENTORY_STATE_PREF_NAME, jsonString);
            PlayerPrefs.Save();
        }

        public List<GameState.ItemData> LoadInventoryState()
        {
            string jsonString = PlayerPrefs.GetString(INVENTORY_STATE_PREF_NAME);

            return JsonUtility.FromJson<List<GameState.ItemData>>(jsonString);
        }
    }
}