using EpicMergeClone.Game.Items;
using EpicMergeClone.Game.Mechanics.OrderSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EpicMergeClone.UI.OrderUI
{
    public class OrderDetailsPanel : MonoBehaviour
    {
        [SerializeField] private Image m_CharacterImage;
        [SerializeField] private Image m_OrderImage;

        [SerializeField] private OrderIngredientItemUI m_OrderIngredientItemPrefab;
        [SerializeField] private RectTransform m_OrderIngredientContainer;

        private List<OrderIngredientItemUI> m_OrderIngredientUIItems = new List<OrderIngredientItemUI>();

        private void Start()
        {
        }

        public void Initialize(Order order, CharacterItemSO characterItem)
        {
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
    }
}