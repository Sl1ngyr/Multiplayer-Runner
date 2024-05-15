using UnityEngine;
using UnityEngine.UI;

namespace UI.AuthMenu
{
    public class PageMenu : MonoBehaviour
    {
        [SerializeField] private Canvas _transitionCanvas;
        [SerializeField] private Button _transitionButton;

        private void TransitionToPage()
        {
            _transitionCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _transitionButton.onClick.AddListener(TransitionToPage);
        }

        private void OnDisable()
        {
            _transitionButton.onClick.RemoveListener(TransitionToPage);
        }
    }
}