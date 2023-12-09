using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.Game.Mechanics.OrderSystem
{
    [CreateAssetMenu(menuName = "Orders/Orders Data")]
    public class OrdersDataSO : ScriptableObject
    {
        public Order[] orderGroups;

        public Order GetOrder(string id)
        {
            for (int i = 0; i < orderGroups.Length; i++)
            {
                if (orderGroups[i].OrderId == id)
                    return orderGroups[i];
            }

            return null;
        }

        public List<Order> GetCurrentOrders() 
        {
            List<Order> orders = new List<Order>();

            return orders;
        }
    }
}