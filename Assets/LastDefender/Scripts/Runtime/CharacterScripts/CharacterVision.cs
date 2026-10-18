using Assets.LastDefender.Scripts.Core.Interfaces;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Runtime.CharacterScripts
{
    internal class CharacterVision : MonoBehaviour
    {
        [SerializeField]
        private float _visionRange = 3f;
        private float _visionHeight = 1.6f;

        private IInteraction _currentInteraction;
        public IInteraction CurrentInteraction => _currentInteraction;
        public bool HasInteraction => _currentInteraction != null;

        private void Update()
        {
            Vector3 direction = Camera.main.transform.forward;

            if (Physics.Raycast(transform.position + Vector3.up * _visionHeight, direction, out RaycastHit hit, _visionRange))
            {
                if(hit.collider.TryGetComponent(out IInteraction _interaction))
                {
                    if(_currentInteraction != null && _currentInteraction != _interaction)
                        _currentInteraction.HideHint();
                    
                    _currentInteraction = _interaction;
                    _currentInteraction.ShowHint();
                }
                else
                {
                    if (_currentInteraction != null)
                    {
                        _currentInteraction.HideHint();
                        _currentInteraction = null;
                    }
                }
                Debug.DrawLine(transform.position + Vector3.up * _visionHeight, hit.point, Color.green);
            }
            else
            {
                if (_currentInteraction != null)
                {
                    _currentInteraction.HideHint();
                    _currentInteraction = null;
                }
                Debug.DrawRay(transform.position + Vector3.up * _visionHeight, direction * _visionRange, Color.red);
            }
        }
    }
}
