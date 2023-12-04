using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    [CreateAssetMenu(menuName = "Item/Item Tree")]
    public class ItemTree : ScriptableObject
    {
        public ItemDataSO[] items;
    }
}