using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    [CreateAssetMenu(menuName = "All Item Data")]
    public class AllItemDatas : ScriptableObject
    {
        public ItemTreeSO[] itemTrees;
        public ItemDataSO[] itemDatas;

        public ItemDataSO GetItemData(string uniqueId)
        {
            for (int i = 0; i < itemDatas.Length; i++)
            {
                if (itemDatas[i].ItemId == uniqueId)
                    return itemDatas[i];
            }

            return null;
        }

        public ItemTreeSO GetItemTreeOf(ItemDataSO itemData)
        {
            for (int i = 0; i < itemTrees.Length; i++)
            {
                for (int j = 0; j < itemTrees[i].items.Length; j++)
                {
                    if (itemTrees[i].GetItemDataAt(j).ItemId == itemData.ItemId)
                        return itemTrees[i];
                }
            }

            return null;
        }
    }
}