using EpicMergeClone.Game.Mechanics.Grid;
using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    public class ItemBase : MonoBehaviour
    {
        public ItemDataSO ItemDataSO { get; private set; }

        [SerializeField] private SpriteRenderer m_SpriteRenderer;

        public Cell CurrentCell { get; private set; }

        public void InitializeItem(ItemDataSO itemData)
        {
            ItemDataSO = itemData;

            m_SpriteRenderer.sprite = itemData.itemSprite;
        }
    }
}