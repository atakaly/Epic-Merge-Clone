using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    [CreateAssetMenu(menuName = "Item/Item Tree")]
    public class ItemTreeSO : ScriptableObject
    {
        public ItemDataSO[] items;

        public ItemDataSO GetItemDataAt(int index)
        {
            return items[index];
        }

        public ItemDataSO GetNextItemData(ItemDataSO item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == item)
                {
                    if (i+1 >= items.Length)
                        return null;

                    int index = i;
                    index++;
                    return items[index];
                }
            }
            return null;
        }
    }
}