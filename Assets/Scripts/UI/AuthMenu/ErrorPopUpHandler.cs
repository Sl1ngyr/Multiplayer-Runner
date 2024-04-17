using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AuthMenu
{
    public class ErrorPopUpHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _errorText;
        [SerializeField] private Button _closePopUpButton;


        public void SetUpErrorToPopUp(string error)
        {
            Debug.Log(error);
            gameObject.SetActive(true);
            
            _errorText.text = error;
        }
        
        private void OnEnable()
        {
            _closePopUpButton.onClick.AddListener(ClosePopUp);
        }

        private void OnDisable()
        {
            _closePopUpButton.onClick.RemoveListener(ClosePopUp);
        }

        private void ClosePopUp()
        {
            gameObject.SetActive(false);
        }
    }
}