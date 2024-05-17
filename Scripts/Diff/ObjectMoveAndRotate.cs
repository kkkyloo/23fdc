using System.Collections;
using UnityEngine;

namespace Kosmos6
{
    public class ObjectMoveAndRotate : MonoBehaviour
    {
        public Transform target;
        public float _speedRotation = 10;
        public float speedMoving = 0.5f;
        [SerializeField] public float waitForMoveSunDelay = 0.1f;

        private IEnumerator coroutineMoveSun;
        private WaitForSeconds waitForMoveSun;

        private void Start()
        {
            if (target == null)
            {
                target = transform;
            }
            waitForMoveSun = new WaitForSeconds(waitForMoveSunDelay);
            StartCoroutineMoveSun();
        }
        private void Update()
        {
            RotateAround();
        }
        private void RotateAround()
        {
            transform.RotateAround(target.transform.position, target.transform.up, _speedRotation * Time.deltaTime);
        }
        private void MoveSun()
        {
            if (gameObject.name == "sun")
                transform.Translate(Vector3.forward * speedMoving * Time.deltaTime);
        }

        [ContextMenu("StartCoroutineMoveSun")]
        public void StartCoroutineMoveSun()
        {
            coroutineMoveSun = MoveSunCor();
            StartCoroutine(coroutineMoveSun);
        }
        [ContextMenu("StopCoroutineMoveSun")]
        public void StopCoroutineMoveSun()
        {
            StopCoroutine(coroutineMoveSun);
        }
        private IEnumerator MoveSunCor()
        {
            while (true)
            {
                yield return waitForMoveSun;
                MoveSun();
            }
        }
    }
}
