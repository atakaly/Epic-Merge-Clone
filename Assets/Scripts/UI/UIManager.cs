using EpicMergeClone.UI.OrderUI;
using UnityEngine;

namespace EpicMergeClone.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UpperBarUIController upperBarUIController;
        [SerializeField] private OrderPanel orderPanel;

        public UpperBarUIController UpperBarUIController => upperBarUIController;
        public OrderPanel OrderPanel => orderPanel;
    }
}