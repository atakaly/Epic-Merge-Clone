using System.Text;
using TMPro;
using UnityEngine;

namespace EpicMergeClone.UI
{
    public class UpperBarUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private ProgressBar experienceProgressBar;

        [SerializeField] private TextMeshProUGUI workerText;
        [SerializeField] private TextMeshProUGUI energyText;
        [SerializeField] private TextMeshProUGUI coinText;

        private StringBuilder stringBuilder;

        private void Awake()
        {
            stringBuilder = new StringBuilder();
        }

        public void UpdateLevelText(int level)
        {
            stringBuilder.Clear();
            stringBuilder.Append(level);

            levelText.text = stringBuilder.ToString();
        }

        public void UpdateCoinText(int coin)
        {
            stringBuilder.Clear();
            stringBuilder.Append(coin);

            coinText.text = stringBuilder.ToString();
        }

        public void UpdateExperience(float exp)
        {
            experienceProgressBar.UpdateFill(exp);
        }

        public void UpdateWorkerText(int currentWorker, int maxWorker)
        {
            stringBuilder.Clear();
            stringBuilder.Append(currentWorker);
            stringBuilder.Append("/");
            stringBuilder.Append(maxWorker);

            workerText.text = stringBuilder.ToString();
        }

        public void UpdateEnergyText(int currentEnergy, int maxEnergy)
        {
            stringBuilder.Clear();
            stringBuilder.Append(currentEnergy);
            stringBuilder.Append("/");
            stringBuilder.Append(maxEnergy);

            energyText.text = stringBuilder.ToString();
        }
    }
}