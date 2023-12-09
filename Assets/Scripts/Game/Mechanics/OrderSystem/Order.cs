using EpicMergeClone.Game.Items;
using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.Game.Mechanics.OrderSystem
{
    [CreateAssetMenu(menuName = "Orders/New Order")]
    public class Order : ScriptableObject
    {
        public string OrderId;
        public string OrderName;
        public Sprite OrderSprite;

        public List<OrderIngredient> OrderIngredients;
        public List<ItemDataSO> Rewards;
    }
}