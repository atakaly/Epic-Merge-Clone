using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    [CreateAssetMenu(menuName = "Item/New Item")]
    public class ItemDataSO : ScriptableObject
    {
        public string UniqueId;
        public Sprite itemSprite;
    }
}