using Fusion;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class SpeedHandlerUI : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI _speedText;
        [SerializeField] private Slider _nitroSlider;
        [SerializeField] private Button _nitroButton;
        
        [SerializeField] private float _speedCoefficient = 15;

        [SerializeField] private float _sliderMaxValue = 1.5f;
        [SerializeField] private float _sliderMinValue = 0;
        
        private bool _isNitroUsed = false;

        private PlayerMovement _localPlayer;
        
        public override void Spawned()
        {
            _nitroButton.onClick.AddListener(NitroPressed);
        }

        public void Init()
        {
            _speedText.text = "0";
            _nitroSlider.value = _sliderMinValue;
            
            BindNitroButtonToPlayer();
        }
        
        public override void FixedUpdateNetwork()
        {
            if(HasStateAuthority == false) return;
            
            if (_isNitroUsed)
            {
                if (_nitroSlider.value > _sliderMinValue)
                {
                    _nitroSlider.value -= Runner.DeltaTime;
                    
                }
                else
                {
                    _nitroSlider.value = _sliderMinValue;
                    _isNitroUsed = false;
                }
                
            }
        }

        public void SetSpeed(float speed)
        {
            float calculationSpeed = speed * _speedCoefficient;
            
            int speedParce = Mathf.FloorToInt(calculationSpeed);
            
            _speedText.text = speedParce.ToString();
        }
        
        public void SetNitro(bool nitroCollected)
        {
            if (nitroCollected)
            {
                _nitroSlider.value = _sliderMaxValue;
            }
        }

        private void BindNitroButtonToPlayer()
        {
            if (Runner.TryGetPlayerObject(Runner.LocalPlayer, out var networkPlayer))
            {
                _localPlayer = networkPlayer.GetComponent<PlayerMovement>();
                
                _nitroButton.onClick.AddListener(_localPlayer.PressNitro);
            }
        }
        
        private void NitroPressed()
        {
            _isNitroUsed = true;
        }
        
        private void OnDisable()
        {
            _nitroButton.onClick.RemoveListener(NitroPressed);
            _nitroButton.onClick.RemoveListener(_localPlayer.PressNitro);
        }
    }
}