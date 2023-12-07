using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    [CreateAssetMenu(menuName = "Item/New Item")]
    public class ItemDataSO : ScriptableObject
    {
        public string ItemId;
        public Sprite itemSprite;

        public bool IsSameType(ItemDataSO other)
        {
            return ItemId == other.ItemId;
        }
    }
}