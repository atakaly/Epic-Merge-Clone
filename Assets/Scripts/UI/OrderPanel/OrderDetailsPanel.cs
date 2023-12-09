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

        public void Initialize(Order order, CharacterItemSO characterItem)
        {
            m_CharacterImage.sprite = characterItem?.itemSprite;
            m_OrderImage.sprite = order.OrderSprite;

            PopulateIngredients(order.OrderIngredients);
        }

        public void PopulateIngredients(List<OrderIngredient> OrderIngredients)
        {
            foreach (var ingredient in OrderIngredients)
            {
                var newIngredientItem = Instantiate(m_OrderIngredientItemPrefab, m_OrderIngredientContainer);
                newIngredientItem.Initialize(ingredient);
            }
        }
    }
}