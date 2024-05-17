using UnityEngine;

namespace Kosmos6
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private float _minScale = 0.6f;
        [SerializeField] private float _maxScale = 1.4f;

        [SerializeField] private float _rotationOffset = 100f;
        private Vector3 _randomRotation;

        private void Start()
        {
            Vector3 originalScale = transform.localScale;
            Vector3 newScale = new Vector3(Random.Range(_minScale, _maxScale), Random.Range(_minScale, _maxScale), Random.Range(_minScale, _maxScale));
            transform.localScale = new Vector3(originalScale.x * newScale.x, originalScale.y * newScale.y, originalScale.z * newScale.z);

            _randomRotation = new Vector3(Random.Range(-_rotationOffset, _rotationOffset), Random.Range(-_rotationOffset, _rotationOffset), Random.Range(-_rotationOffset, _rotationOffset));
        }
        private void Update() => transform.Rotate(_randomRotation * Time.deltaTime);     
    }
}

