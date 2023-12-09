using DG.Tweening;
using UnityEngine;

namespace EpicMergeClone.UI
{
    public class PopUpPanel : PanelBase
    {
        [SerializeField] protected Transform blackBg;

        public override void Show()
        {
            base.Show();
            blackBg.gameObject.SetActive(true);
            target.DOKill();
            blackBg.DOKill();

            target.DOPunchScale(new Vector2(0.05f, 0.05f), 0.15f, 1, 0.5f);
        }

        public override void Hide()
        {
            target.DOKill();
            blackBg.DOKill();

            base.Hide();
            blackBg.gameObject.SetActive(false);
        }
    }
}