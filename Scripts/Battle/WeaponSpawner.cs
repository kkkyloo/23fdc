using System;
using System.Collections;
using UnityEngine;

namespace Kosmos6
{
    public class WeaponSpawner : MonoBehaviour, IWeapon
    {
        [SerializeField] private FlyweightDefinition _flyweightDefinition;

        [SerializeField] private WeaponSpawnerDefinition _weaponSpawnerDefinition;

        // Data Extrinsic
        private DataWeaponExtrinsic _dataWeaponExtrinsic;
        // Serialized for Debug
        [Header("Inner Workings")]
        [SerializeField] private bool _canFire = true;

        //private WaitForSeconds _waitForSecondsSpawning;
        private float _waitTimeSpawning => _weaponSpawnerDefinition.CooldownTimeTotal;

        private WaitForSeconds _waitForSecondsMuzzleflash;
        [SerializeField] public float _waitTimeMuzzleflash = 0.05f;

        [Header("Links")]
        [SerializeField] private Transform _muzzle;
        [SerializeField] private GameObject _muzzleflash;

        [SerializeField] private float CountAmmo = 100;

        private bool _canReload = true;
        public static Action OnUseAmmo;

        public void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic)
        {
            _dataWeaponExtrinsic = dataWeaponExtrinsic;
            _waitForSecondsMuzzleflash = new WaitForSeconds(_waitTimeMuzzleflash);
        }

        private void OnEnable()
        {
            Ammo.OnGetAmmo += GetAmmo;
            ManagerAmmo.Instance.UseStamina(CountAmmo);
            _canReload = true;
            StartCoroutine(Reload());
        }
        private void OnDisable() => Ammo.OnGetAmmo -= GetAmmo;

        public Vector3 FireWeapon(Vector3 targetPosition)
        {
            if (!_canFire) return Vector3.zero;

            if (CountAmmo <= 0)
            {
                StartCoroutine(Reload());
                return Vector3.zero;
            }

            --CountAmmo;
            OnUseAmmo?.Invoke();
            ManagerAmmo.Instance.UseStamina(CountAmmo);

            var spawned = FactoryFlyweight.Instance.Spawn(_flyweightDefinition, transform.position, Quaternion.LookRotation(targetPosition - transform.position));
            spawned.GetComponent<IWeaponSpawnable>().Initialize(_dataWeaponExtrinsic);
            VisualizeFiring(targetPosition);

            StartCoroutine(ExecuteCooldown(_waitTimeSpawning));

            return Vector3.zero;
        }

        // _muzzle VFX
        public void VisualizeFiring(Vector3 targetPosition)
        {
            if (_muzzleflash != null)
            {
                StartCoroutine(VisualizeMuzzleFlash(targetPosition));
            }
        }
        private IEnumerator VisualizeMuzzleFlash(Vector3 targetPosition)
        {
            _muzzleflash.transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);

            _muzzleflash.SetActive(true);
            yield return _waitForSecondsMuzzleflash;
            _muzzleflash.SetActive(false);
        }
        private IEnumerator ExecuteCooldown(float delay)
        {
            _canFire = false;

            yield return new WaitForSeconds(delay);
            _canFire = true;
        }

        IEnumerator Reload()
        {
            if (_canReload)
            {
                _canReload = false;
                yield return new WaitForSeconds(5);
                CountAmmo += 10;
                ManagerAmmo.Instance.UseStamina(CountAmmo);
                _canReload = true;
            }
        }
        private void GetAmmo()
        {
            if (CountAmmo + 50 > 100)
                CountAmmo = 100;
            else
                CountAmmo += 50;

            ManagerAmmo.Instance.UseStamina(CountAmmo);
        }



    }
}