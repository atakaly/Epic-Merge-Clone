using EpicMergeClone.Game.Items;
using EpicMergeClone.Game.Mechanics.Order;
using UnityEngine;
using Zenject;

namespace EpicMergeClone.Installers
{
    [CreateAssetMenu(menuName = "Global Game Data")]
    public class GlobalGameData : ScriptableObjectInstaller<GlobalGameData>
    {
        public AllItemDatas allItemDatas;
        public OrdersDataSO allOrderDatas;

        public override void InstallBindings()
        {
            Container.BindInstance(this);
        }
    }
}