using UnityEngine;
using UnityEngine.UI;

namespace UI.AuthMenu
{
    public class StartMenuPage : MonoBehaviour
    {
        [Header("Start page")]
        [SerializeField] private Button _startToLoginPageButton;
        [SerializeField] private Button _startToRegistrationPageButton;

        [Space] 
        [Header("Login page")] 
        [SerializeField] private Canvas _loginCanvas;

        [Space] 
        [Header("Registration page")] 
        [SerializeField] private Canvas _registrationCanvas;

        private void TransitionToRegistrationPage()
        {
            _registrationCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void TransitionToLoginPage()
        {
            _loginCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            _startToRegistrationPageButton.onClick.AddListener(TransitionToRegistrationPage);
            _startToLoginPageButton.onClick.AddListener(TransitionToLoginPage);
        }

        private void OnDisable()
        {
            _startToRegistrationPageButton.onClick.RemoveListener(TransitionToRegistrationPage);
            _startToLoginPageButton.onClick.RemoveListener(TransitionToLoginPage);
        }
    }
}
