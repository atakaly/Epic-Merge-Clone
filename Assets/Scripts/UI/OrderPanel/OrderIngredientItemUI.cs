using EpicMergeClone.Game.Mechanics.OrderSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EpicMergeClone.UI.OrderUI
{
    public class OrderIngredientItemUI : MonoBehaviour
    {
        [SerializeField] private Image m_IngredientImage;
        [SerializeField] private TextMeshProUGUI m_NeedAmount;

        public void Initialize(OrderIngredient ingredient)
        {
            m_IngredientImage.sprite = ingredient.Item.itemSprite;
            m_NeedAmount.text = ingredient.Count.ToString();
        }
    }
}