using System;
using UnityEngine;

namespace Kosmos6
{
    public class Ammo : MonoBehaviour, IAmmo
    {
        public static Action OnGetAmmo;


        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.GetComponent<SpaceShip>())
            {


                OnGetAmmo?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}