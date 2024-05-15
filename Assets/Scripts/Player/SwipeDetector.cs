using Fusion;
using UnityEngine;

namespace Player
{
    public class SwipeDetector : NetworkBehaviour
    {
        [SerializeField] private float _swipeResistance = 100;
        [SerializeField] private int _rightBorderPosition = 4;
        [SerializeField] private int _leftBorderPosition = -4;
        
        private int _currentBorderPosition;
        
        private Movement _movementInputAction;
        private bool _isSwipeDetected;
        private Vector2 _initialPosition;
        
        private Vector2 _currentPosition => _movementInputAction.Input.Position.ReadValue<Vector2>();
        
        public override void Spawned()
        {
            _isSwipeDetected = false;
            
            _currentBorderPosition = int.Parse(transform.position.x.ToString());
            
            _movementInputAction = new Movement();
            _movementInputAction.Input.Enable();

            _movementInputAction.Input.Press.performed += _ => { _initialPosition = _currentPosition; };
            _movementInputAction.Input.Press.canceled += _ => DetectSwipeBool();

        }

        private void DetectSwipeBool()
        {
            _isSwipeDetected = true;
        }
        
        private void DetectSwipe()
        {
            if(!_isSwipeDetected) return;
            
            Vector2 delta = _currentPosition - _initialPosition;
            
            if (Mathf.Abs(delta.x) > _swipeResistance)
            {
                Move(delta.x);
            }
            
        }

        public override void FixedUpdateNetwork()
        {
            if (HasStateAuthority == false) return;
            
            DetectSwipe();
        }

        private void Move(float deltaX)
        {
            if (deltaX > 0 && _currentBorderPosition != _rightBorderPosition)
            {
                _currentBorderPosition += _rightBorderPosition;

                transform.position = new Vector3(_currentBorderPosition, 0, transform.position.z);
            }
            else if (deltaX < 0 && _currentBorderPosition != _leftBorderPosition)
            {
                _currentBorderPosition += _leftBorderPosition;

                transform.position = new Vector3(_currentBorderPosition, 0, transform.position.z);
            }
            
            _isSwipeDetected = false;
        }
    }
}