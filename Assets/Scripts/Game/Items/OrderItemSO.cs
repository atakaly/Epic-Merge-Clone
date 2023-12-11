using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    [CreateAssetMenu(menuName = "Item/New Order Item")]
    public class OrderItemSO : ItemDataSO
    {
        public int Experience;
        public int Coin;
    }
}