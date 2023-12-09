using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    [CreateAssetMenu(menuName = "Item/New Production Item")]
    public class ProductionItemSO : ItemDataSO
    {
        public int ProduceTimeSeconds = 10;
        public int ProduceAmount = 4;

        public ItemDataSO ItemToProduce;
    }
}