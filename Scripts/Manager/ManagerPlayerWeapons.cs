using Kosmos6;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class ManagerPlayerWeapons : MonoBehaviour
    {
        [SerializeField] private int CurrentWeaponId;
        [SerializeField] private GameObject Weapons;
        [SerializeField] private ShipWeapons ShipWeapons;
        [SerializeField] private GameObject CurrentWeapon;
        [SerializeField] private List<GameObject> WeaponsList = new();

        private void Start() => ShipWeapons = Weapons.GetComponent<ShipWeapons>();

        private void Update()
        {
            if (Input.GetButtonDown("GunChange"))
                ChangeShipToNext();
        }
        private void ChangeShipToNext()
        {
            CurrentWeaponId++;
            if (CurrentWeaponId == WeaponsList.Count)
                CurrentWeaponId = 0;

            ChangeShip();
        }
        private void ChangeShip()
        {
            CurrentWeapon.SetActive(false);
            CurrentWeapon = WeaponsList[CurrentWeaponId];
            CurrentWeapon.SetActive(true);
            ShipWeapons.enabled = false;
            StartCoroutine(Delay());
        }
        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.2f);
            ShipWeapons.enabled = true;
        }
    }
}