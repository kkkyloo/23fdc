using UnityEngine;

namespace Kosmos6
{
    public class TransformRandomRotationScale : MonoBehaviour
    {
        [Header("Scale")]
        [SerializeField] private bool _randomScaleZ;
        [SerializeField] private float _minScaleZ, _maxScaleZ;

        [Header("Rotation")]
        [SerializeField] private bool _randomRotationZ;
        [SerializeField] private float _minRotationZ, _maxRotationZ;

        private Vector3 _originalScale; // Set by game/level designer
        private Vector3 _originalRotation; // Set by game/level designer


        private void Awake()
        {
            _originalScale = transform.localScale;
            _originalRotation = transform.localEulerAngles;
        }

        private void OnEnable()
        {
            if (_randomScaleZ)
                transform.localScale = new Vector3(_originalScale.x, _originalScale.y, Random.Range(_minScaleZ, _maxScaleZ));

            if (_randomRotationZ)
                transform.rotation *= Quaternion.Euler(_originalRotation.x, _originalRotation.y, Random.Range(_minRotationZ, _maxRotationZ));
        }
    }
}