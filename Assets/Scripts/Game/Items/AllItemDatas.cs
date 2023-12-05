using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    [CreateAssetMenu(menuName = "All Item Data")]
    public class AllItemDatas : ScriptableObject
    {
        public ItemDataSO[] itemDatas;

        public ItemDataSO GetItemData(string uniqueId)
        {
            for (int i = 0; i < itemDatas.Length; i++)
            {
                if (itemDatas[i].UniqueId == uniqueId)
                    return itemDatas[i];
            }

            return null;
        }
    }
}