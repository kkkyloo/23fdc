using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kosmos6
{
    public class ShipWeapons : MonoBehaviour
    {
        [SerializeField] private SpaceShip Spaceship;
        [SerializeField] private Rigidbody _shipRigidbody;
        [SerializeField] private List<IWeapon> Weapons = new List<IWeapon>();
        [SerializeField] private float MaxDistanceToTarget = 250f;
        [SerializeField] private GameObject prefab;

        private void Awake()
        {
            if (Spaceship == null)
                Spaceship = GetComponentInParent<SpaceShip>();
            if (_shipRigidbody == null)
                _shipRigidbody = GetComponentInParent<Rigidbody>();
        }

        [ContextMenu("InitWeapons")]
        public void InitWeapons()
        {
            Weapons = GetComponentsInChildren<IWeapon>().ToList();
            foreach (var weapon in Weapons)
            {
                weapon.Initialize(new DataWeaponExtrinsic()
                { ShipRigidbody = _shipRigidbody, GameAgent = Spaceship.ShipAgent });
            }
        }

        private void OnEnable()
        {
            InitWeapons();
            Spaceship.InputShipWeapons.OnAttackInput += FireWeapons;
        }
        private void OnDisable()
        {
            Spaceship.InputShipWeapons.OnAttackInput -= FireWeapons;
        }
        public void FireWeapons()
        {
           
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out hit, MaxDistanceToTarget))
            {
                foreach (var weapon in Weapons)
                {
                    weapon.FireWeapon(hit.point);
                    

                }
            }
            else
            {
                foreach (var weapon in Weapons)
                {
                    weapon.FireWeapon(ray.origin + ray.direction * MaxDistanceToTarget);
                }
            }

        }
    }

}
