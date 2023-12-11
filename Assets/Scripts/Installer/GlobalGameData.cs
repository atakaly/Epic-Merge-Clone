using EpicMergeClone.Game.Items;
using EpicMergeClone.Game.Mechanics.OrderSystem;
using EpicMergeClone.Game.Player;
using UnityEngine;
using Zenject;

namespace EpicMergeClone.Installers
{
    [CreateAssetMenu(menuName = "Global Game Data")]
    public class GlobalGameData : ScriptableObjectInstaller<GlobalGameData>
    {
        public AllItemDatas allItemDatas;
        public OrdersDataSO allOrderDatas;
        public AllPlayerLevelsData allPlayerLevelsData;

        public string InitialGridState = "";

        public override void InstallBindings()
        {
            Container.BindInstance(this);
        }
    }
}