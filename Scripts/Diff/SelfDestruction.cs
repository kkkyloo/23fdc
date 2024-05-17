using System.Collections;
using UnityEngine;

namespace Kosmos6
{
    public class SelfDestruction : MonoBehaviour
    {
        public float TimeBeforeDestruction = 2f;


        private void Start()
        {
            StartCoroutine(SelfDestruct());
        }


        private IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(TimeBeforeDestruction); //через 2 сек разрушает объект

            Destroy(gameObject);
        }
    }
}

