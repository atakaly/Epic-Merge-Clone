using EpicMergeClone.Game.Items;
using EpicMergeClone.Game.Mechanics.OrderSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EpicMergeClone.UI.OrderUI
{
    public class OrderDetailsPanel : MonoBehaviour
    {
        public UnityEvent<Order> OnClaimClicked;
        public UnityEvent<Order> OnCookClicked;

        [SerializeField] private Image m_CharacterImage;
        [SerializeField] private Image m_OrderImage;

        [SerializeField] private OrderIngredientItemUI m_OrderIngredientItemPrefab;
        [SerializeField] private RectTransform m_OrderIngredientContainer;

        [SerializeField] private Button m_ClaimButton;
        [SerializeField] private Button m_CookButton;

        private List<OrderIngredientItemUI> m_OrderIngredientUIItems = new List<OrderIngredientItemUI>();

        private Order m_Order;

        public void Initialize(Order order, CharacterItemSO characterItem)
        {
            m_Order = order;

            m_CharacterImage.sprite = characterItem?.itemSprite;
            m_OrderImage.sprite = order.OrderSprite;

            ClearUIItems();
            PopulateIngredients(order.OrderIngredients);
        }

        public void PopulateIngredients(List<OrderIngredient> orderIngredients)
        {
            foreach (var ingredient in orderIngredients)
            {
                OrderIngredientItemUI newIngredientItem = TryGetIngredientItemUI();
                newIngredientItem.Initialize(ingredient);
                m_OrderIngredientUIItems.Add(newIngredientItem);
            }
        }

        private OrderIngredientItemUI TryGetIngredientItemUI()
        {
            OrderIngredientItemUI item = m_OrderIngredientUIItems.Find(i => !i.gameObject.activeInHierarchy);

            if (item == null)
            {
                item = Instantiate(m_OrderIngredientItemPrefab, m_OrderIngredientContainer);
                m_OrderIngredientUIItems.Add(item);
            }

            item.gameObject.SetActive(true);
            return item;
        }

        public void ClearUIItems()
        {
            foreach (var item in m_OrderIngredientUIItems)
            {
                item.gameObject.SetActive(false);
            }
        }

        public void ClaimClick()
        {
            OnClaimClicked?.Invoke(m_Order);
        }

        public void CookClick()
        {
            OnCookClicked?.Invoke(m_Order);
        }

        public void SetCookButtonInteractable(bool isOrderCookable)
        {
            m_CookButton.interactable = isOrderCookable;
        }

        public void SetClaimButtonInteractable(bool isOrderCooking)
        {
            m_ClaimButton.interactable = !isOrderCooking;
        }

        public void SetButtonsVisibility(bool isOrderCooked, bool isOrderCooking)
        {
            m_CookButton.gameObject.SetActive(!isOrderCooked && !isOrderCooking);
            m_ClaimButton.gameObject.SetActive(isOrderCooked);
        }
    }
}