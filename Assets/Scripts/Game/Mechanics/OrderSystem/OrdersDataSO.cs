using UnityEngine;

namespace EpicMergeClone.Game.Mechanics.Order
{
    [CreateAssetMenu(menuName = "Orders/Orders Data")]
    public class OrdersDataSO : ScriptableObject
    {
        public OrderGroup[] orderGroups;

        public OrderGroup GetOrderGroup(string id)
        {
            for (int i = 0; i < orderGroups.Length; i++)
            {
                if (orderGroups[i].OrderId == id)
                    return orderGroups[i];
            }

            return null;
        }
    }
}