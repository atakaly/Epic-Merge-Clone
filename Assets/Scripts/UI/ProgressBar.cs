using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace EpicMergeClone.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        public void UpdateFill(float amount)
        {
            fillImage.DOFillAmount(amount, 0.3f);
        }

        public void UpdateImmediate(float amount)
        {
            fillImage.fillAmount = amount;
        }
    }
}