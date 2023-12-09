using UnityEngine;

namespace EpicMergeClone.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UpperBarUIController upperBarUIController;

        public UpperBarUIController UpperBarUIController 
        { 
            get 
            {  
                return upperBarUIController; 
            }
        }
    }
}