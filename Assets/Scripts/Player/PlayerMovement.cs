using Fusion;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private float _maxSpeed = 3;
        [SerializeField] private float _timeForAcceleration = 96;
        [SerializeField] private float _maxSpeedCoefficient = 1;
        [SerializeField] private float _nitroSpeed = 1.5f;
        
        private float _currentSpeed = 0;
        private float _acceleration = 0;
        private float _nitroAcceleration = 0;
        
        private Rigidbody _rigidbody;
        private PlayerCollisionDetector _playerCollisionDetector;
        
        [Networked] private TickTimer _slowSpeedTime { get; set; }
        
        public override void Spawned()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerCollisionDetector = GetComponent<PlayerCollisionDetector>();
            
            _acceleration = _maxSpeed / _timeForAcceleration;

            _playerCollisionDetector.OnSlowObstacleDetected += SlowMovement;
            _playerCollisionDetector.OnPushBackObstacleDetected += PushBack;
            _playerCollisionDetector.OnResetSpeedObstacleDetected += ResetSpeed;
        }
        
        public override void FixedUpdateNetwork()
        {
            if (HasStateAuthority == false) return;
            
            if (_currentSpeed < (_maxSpeed * _maxSpeedCoefficient + _nitroSpeed * _nitroAcceleration))
            {
                _currentSpeed += (_acceleration + _nitroAcceleration) * Runner.DeltaTime;
            }
            else
            {
                _currentSpeed = _maxSpeed * _maxSpeedCoefficient + _nitroSpeed * _nitroAcceleration;
            }

            if (_nitroAcceleration > 0)
            {
                _nitroAcceleration -= Runner.DeltaTime;
            }
            else
            {
                _nitroAcceleration = 0;
            }
            
            if (_slowSpeedTime.Expired(Runner))
            {
                _maxSpeedCoefficient = 1;
            }
            
            
            _rigidbody.MovePosition(transform.position + Vector3.forward * _currentSpeed);
        }

        private void ResetSpeed()
        {
            _currentSpeed = 0;
        }

        private void PushBack(Vector3 pushBackPosition)
        {
            _rigidbody.transform.position -= pushBackPosition;
        }
        
        private void SlowMovement(float delay, float coefficient)
        {
            _maxSpeedCoefficient = coefficient;
            
            _slowSpeedTime = TickTimer.CreateFromSeconds(Runner, delay);
        }
        
        private void OnDisable()
        {
            _playerCollisionDetector.OnSlowObstacleDetected -= SlowMovement;
            _playerCollisionDetector.OnPushBackObstacleDetected -= PushBack;
            _playerCollisionDetector.OnResetSpeedObstacleDetected -= ResetSpeed;
        }
        
    }
}