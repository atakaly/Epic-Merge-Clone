using EpicMergeClone.Game.Items;
using System;

namespace EpicMergeClone.Game.Mechanics.OrderSystem
{
    [Serializable]
    public class OrderIngredient
    {
        public ItemDataSO Item;
        public int Count;
    }
}