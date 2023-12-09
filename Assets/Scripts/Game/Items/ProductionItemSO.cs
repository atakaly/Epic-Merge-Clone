using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    [CreateAssetMenu(menuName = "Item/New Production Item")]
    public class ProductionItemSO : ItemDataSO
    {
        public int ItemEnergy = 4;
        public int[] PlayerEnergyRequiresForClear;

        public ItemDataSO[] ItemsToCreate;
    }
}