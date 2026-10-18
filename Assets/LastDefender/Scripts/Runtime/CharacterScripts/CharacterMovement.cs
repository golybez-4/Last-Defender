using Assets.LastDefender.Scripts.Core.Common.Utils;
using Assets.LastDefender.Scripts.Core.Signals;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using CharacterInfo = Assets.LastDefender.Scripts.Core.Models.CharacterInfo;

namespace Assets.LastDefender.Scripts.Runtime.CharacterScripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _speedRun = 10f;

        [Header("Jump Settings")]
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _groundCheckDistance = 1.1f;

        [Header("Camera Settings")]
        [SerializeField] private float _sensitivity = 8f;
        [SerializeField] private Vector2 _pitchBounds = new Vector2(-80f, 80f);

        [SerializeField]
        private Animator _animator;

        [Header("Stamina Settings")]
        private float _staminaConsumption = 1f; 

        [Inject] private CharacterInputSystem _inputSystem;
        private CharacterInputSystem.CharacterActions _characterActions;
        private Vector2 _moveInput;

        private Camera _mainCamera;
        private float _cameraPitch;

        private bool _isRunning = false;

        private ActionTimer _actionTimer;
        [Inject]
        private CharacterInfo _characterInfo;
        [Inject] 
        private SignalBus _signalBus;

        private void Awake()
        {
            _mainCamera = Camera.main;
            Cursor.lockState = CursorLockMode.Locked;
            if (_mainCamera != null)
            {
                _mainCamera.transform.SetParent(transform);
                //_mainCamera.transform.localPosition = new Vector3(0, 2f, -0.5f);
                _mainCamera.transform.localPosition = new Vector3(0.5f, 2f, -1f);
                _mainCamera.transform.localRotation = Quaternion.identity;
            }

            InitInputSystem();
            _actionTimer = new ActionTimer(ConsumeStamina, 0.5f);
            _actionTimer.Start();
        }

        private void OnEnable() => _inputSystem.Enable();

        private void FixedUpdate()
        {
            Moving();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), transform.position + Vector3.down * _groundCheckDistance);
        }

        private void OnDisable()
        {
            _inputSystem.Disable();
            _actionTimer.Stop();
            UnsubscribeEvents();
        }

        #region Input System Methods
        private void InitInputSystem()
        {
            _characterActions = _inputSystem.Character;

           SubscribeEvents();
        }

        private void Look(InputAction.CallbackContext context)
        {
            var delta = context.ReadValue<Vector2>();
            var mouseX = delta.x * _sensitivity * Time.deltaTime;
            var mouseY = delta.y * _sensitivity * Time.deltaTime;

            transform.Rotate(Vector3.up * mouseX);

            _cameraPitch -= mouseY;
            _cameraPitch = Mathf.Clamp(_cameraPitch, _pitchBounds.x, _pitchBounds.y);

            _mainCamera.transform.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);
        }

        private void Run(InputAction.CallbackContext context)
        {
            _isRunning = context.ReadValue<float>() > 0;
            _animator.SetBool("IsRun", _isRunning);
        }

        private void Move(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
            _animator?.SetFloat("Speed", _moveInput.magnitude);
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Vector3.down, _groundCheckDistance))
            {
                Debug.Log("Jump");
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }
        #endregion

        private void Moving()
        {
            float speed = _isRunning ? _speedRun : _speed;
            var moveDir = new Vector3(_moveInput.x * speed, 0, _moveInput.y * speed);
            moveDir = transform.TransformDirection(moveDir);
            _rigidbody.linearVelocity = new Vector3(moveDir.x, _rigidbody.linearVelocity.y, moveDir.z);
        }

        private async void ConsumeStamina()
        {
            await UniTask.SwitchToMainThread();

            if (_rigidbody.linearVelocity == Vector3.zero)
            {
                _characterInfo.Stamina += _staminaConsumption;
                _signalBus.Fire<ChangedStaminaSignal>();
            }
            else if (_characterInfo.Stamina > 0 && _isRunning && _rigidbody.linearVelocity != Vector3.zero)
            {
                _characterInfo.Stamina -= _staminaConsumption;
                _signalBus.Fire<ChangedStaminaSignal>();
            }
            else
            {
                _isRunning = false;
                _animator.SetBool("IsRun", _isRunning);
            }
        }

        private void SubscribeEvents()
        {
            _characterActions.Move.performed += Move;
            _characterActions.Jump.performed += Jump;
            _characterActions.Run.performed += Run;
            _characterActions.Look.performed += Look;
        }

        private void UnsubscribeEvents()
        {
            _characterActions.Move.performed -= Move;
            _characterActions.Jump.performed -= Jump;
            _characterActions.Run.performed -= Run;
            _characterActions.Look.performed -= Look;
        }
    }
}