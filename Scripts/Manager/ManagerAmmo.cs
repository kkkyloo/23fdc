using IG;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kosmos6
{
    public class ManagerAmmo : SingletonManager<ManagerAmmo>
    {
        public Action<float> OnUseAmmo;
        private float _maxAmmo;
        private float _ammo;

        private bool _getMaxAmmo = false;

        [SerializeField] private Image _staminaBar;
        private void OnEnable()
        {

                _staminaBar = GameObject.Find("FireStamina").GetComponent<Image>();
            

            OnUseAmmo += Stamina;
        }
        private void OnDisable() => OnUseAmmo -= Stamina;


        public void UseStamina(float countAmmo) => OnUseAmmo?.Invoke(countAmmo);

        private void Stamina(float countAmmo)
        {
            if (!_getMaxAmmo)
            {
                _maxAmmo = countAmmo + 1;
                _getMaxAmmo = true;
            }

            _ammo = countAmmo;
            _staminaBar.fillAmount = _ammo / _maxAmmo;
        }
    }
}
