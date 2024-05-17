using UnityEngine;

namespace Kosmos6
{
    public class Projectile : Flyweight, IWeaponSpawnable
    {
        private new ProjectileDefinition Definition => (ProjectileDefinition)base.Definition;
        // Data Extrinsic
        [SerializeField] private DataWeaponExtrinsic _dataWeaponExtrinsic;

        // Serialized for Debug
        [Header("Inner Workings")]
        [SerializeField] private float _lifetimeLeft = 0f; // How long it exists after being shot
        [SerializeField] private float _timerAfterHit = 0f; // Timer for delay

        private bool _afterHit;
        private RaycastHit _objectHit; // Because of extra delay

        private Vector3 _spawnerVelocity = Vector3.zero;
        private GameAgent _gameAgent;

        [Header("Links")]
        [SerializeField] private Rigidbody _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void OnEnable()
        {
            // Set default state
            _objectHit = default;
            _afterHit = false;
            _lifetimeLeft = Definition.LifetimeTotal;
            _timerAfterHit = Definition.DelayAfterHit;
        }

        public void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic)
        {
            _dataWeaponExtrinsic = dataWeaponExtrinsic;
            _spawnerVelocity = _dataWeaponExtrinsic.ShipRigidbody.velocity;

            _gameAgent = _dataWeaponExtrinsic.GameAgent;
        }

        private void FixedUpdate()
        {
            // Simply destroy projectile if it's time has run out and it didn't hit anything
            _lifetimeLeft -= Time.fixedDeltaTime;
            if (_lifetimeLeft <= 0)
            {
                DestroyProjectile();
                return;
            }

            _rigidbody.velocity = _spawnerVelocity + transform.forward * Definition.ProjectileVelocityDefinition.Velocity;

            // If projectile registered hitting something
            if (_afterHit && _objectHit.transform != null)
            {
                _timerAfterHit += Time.fixedDeltaTime;

                // ...and some time has passed after it hit something 
                if (_timerAfterHit > Definition.DelayAfterHit)
                {
                    // Visual FXs
                    if (Definition.ImpactPrefab != null)
                        CreateImpact(Definition.ImpactPrefab, _objectHit.point);

                    if (_objectHit.transform.TryGetComponent<IDamageable>(out IDamageable damageableHit))
                        Damage(damageableHit, Definition.Damage, _objectHit.point, _gameAgent);

                    ApplyForce(_objectHit, 2.5f);
                    DestroyProjectile();
                }

                return;
            }

            // While flying in active state.
            // Raycast is the last because it's much more costly to do than if() checks
            if (Physics.Raycast(transform.position, transform.forward, out _objectHit, Definition.RaycastDistance, Definition.LayerMask))
            {
                // Register a hit
                _afterHit = true;
            }
        }
        private void Damage(IDamageable target, float damageAmount, Vector3 targetHitPosition, GameAgent attacker)
        {
            if (target != null)
                target?.ReceiveDamage(damageAmount, targetHitPosition, attacker);
        }
        private void ApplyForce(RaycastHit hitPoint, float force)
        {
            if (hitPoint.rigidbody != null)
                hitPoint.rigidbody.AddForceAtPosition(transform.forward * force, hitPoint.point, ForceMode.VelocityChange);
        }
        private void CreateImpact(GameObject impactPrefab, Vector3 point)
        {
            if (impactPrefab != null)
                Instantiate(impactPrefab, point, Quaternion.identity);
        }
        private void DestroyProjectile() => FactoryFlyweight.Instance.ReturnToPool(this);
    }
}