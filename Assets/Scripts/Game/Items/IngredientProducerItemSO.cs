using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    [CreateAssetMenu(menuName = "Item/New Ingredient Producer Item")]
    public class IngredientProducerItemSO : ItemDataSO
    {
        public int ProduceTimeSeconds = 10;
        public int ProduceAmount = 4;

        public ItemDataSO ItemToProduce;
    }
}