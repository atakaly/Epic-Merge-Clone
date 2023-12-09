using UnityEngine;

namespace EpicMergeClone.UI
{
    public class PanelBase : MonoBehaviour
    {
        [SerializeField] protected RectTransform target;

        public virtual void Start()
        {
            Hide();
        }

        public virtual void Show()
        {
            target.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            target.gameObject.SetActive(false);
        }
    }

}