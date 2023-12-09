using EpicMergeClone.Game.Mechanics.OrderSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EpicMergeClone.UI.OrderUI
{
    public class OrderItemUI : MonoBehaviour, IPointerClickHandler
    {
        public Image m_OrderImage;

        public void Initialize(Order orderData)
        {
            m_OrderImage.sprite = orderData.OrderSprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }
}