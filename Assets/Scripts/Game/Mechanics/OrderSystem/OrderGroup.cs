using System;
using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.Game.Mechanics.Order
{
    [CreateAssetMenu(menuName = "Orders/New Order")]
    public class OrderGroup : ScriptableObject
    {
        public string OrderId;
        public string OrderName;
        public List<Order> Orders;
    }
}