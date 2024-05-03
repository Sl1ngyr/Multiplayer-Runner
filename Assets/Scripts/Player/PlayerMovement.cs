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
        
        private float _acceleration = 0;
        private float _nitroAcceleration;

        private bool _isRaceStarted = false;
        
        private PlayerCollisionDetector _playerCollisionDetector;
        private SwipeDetector _swipeDetector;
        
        [Networked] private TickTimer _slowSpeedTime { get; set; }

        public float CurrentSpeed { get; private set; } = 0;
        public bool IsNitroPressed { get; private set; } = false;
        public bool IsNitroChargeReady { get; private set; } = false;
        public bool IsPlayerFinished { get; private set; } = false;
        
        public override void Spawned()
        {
            _playerCollisionDetector = GetComponent<PlayerCollisionDetector>();
            _swipeDetector = GetComponent<SwipeDetector>();
            
            _acceleration = _maxSpeed / _timeForAcceleration;

            _playerCollisionDetector.OnSlowObstacleDetected += SlowMovement;
            _playerCollisionDetector.OnPushBackObstacleDetected += PushBack;
            _playerCollisionDetector.OnResetSpeedObstacleDetected += ResetSpeed;
            _playerCollisionDetector.OnNitroChargeDetected += GetNitroChange;
            _playerCollisionDetector.OnPlayerFinished += PlayerFinished;
        }

        public void RaceStarted()
        {
            _isRaceStarted = true;
        }

        public void PressNitro()
        {
            if (IsNitroChargeReady)
            {
                IsNitroPressed = true;
            }
        }
        
        public override void FixedUpdateNetwork()
        {
            if (HasStateAuthority == false) return;

            Move();
        }

        private void Move()
        {
            if(!_isRaceStarted || IsPlayerFinished) return;

            if (IsNitroChargeReady && IsNitroPressed)
            {
                _nitroAcceleration = _nitroSpeed;
                IsNitroChargeReady = false;
                IsNitroPressed = false;
            }
            
            if (CurrentSpeed < (_maxSpeed * _maxSpeedCoefficient + _nitroSpeed * _nitroAcceleration))
            {
                CurrentSpeed += (_acceleration + _nitroAcceleration) * Runner.DeltaTime;
            }
            else
            {
                CurrentSpeed = _maxSpeed * _maxSpeedCoefficient + _nitroSpeed * _nitroAcceleration;
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
             
            transform.localPosition += Vector3.forward * CurrentSpeed;
        }
        
        private void PlayerFinished()
        {
            IsPlayerFinished = true;
            _swipeDetector.enabled = false;
        }
        
        private void ResetSpeed()
        {
            CurrentSpeed = 0;
        }

        private void PushBack(Vector3 pushBackPosition)
        {
            transform.localPosition += pushBackPosition;
        }
        
        private void SlowMovement(float delay, float coefficient)
        {
            _maxSpeedCoefficient = coefficient;
            
            _slowSpeedTime = TickTimer.CreateFromSeconds(Runner, delay);
        }

        private void GetNitroChange()
        {
            IsNitroChargeReady = true;
        }
        
        private void OnDisable()
        {
            _playerCollisionDetector.OnSlowObstacleDetected -= SlowMovement;
            _playerCollisionDetector.OnPushBackObstacleDetected -= PushBack;
            _playerCollisionDetector.OnResetSpeedObstacleDetected -= ResetSpeed;
            _playerCollisionDetector.OnNitroChargeDetected -= GetNitroChange;
        }

    }
}