namespace EpicMergeClone.Game.Items
{
    public class OrderItem : ItemBase
    {
        public OrderItemSO ItemData => ItemDataSO as OrderItemSO;

        protected override void OnClick()
        {
            base.OnClick();

            m_GameStateManager.Experience += ItemData.Experience;
            m_GameStateManager.Coin += ItemData.Coin;

            m_ItemPoolManager.DespawnItem(this);
        }
    }
}