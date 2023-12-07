using EpicMergeClone.Game.Items;
using System;

namespace EpicMergeClone.Game.Mechanics.Order
{
    [Serializable]
    public class Order
    {
        public ItemDataSO Item;
        public int Count;
    }
}