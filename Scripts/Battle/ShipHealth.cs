using UnityEngine;

namespace Kosmos6
{
    public class ShipHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _health;
        public float Health => _health;
        [SerializeField] private GameObject PrefabEffectDestr;

        public void ReceiveDamage(float damageAmount, Vector3 hitPosition, GameAgent sender)
        {
            _health -= damageAmount;

            if (_health <= 0)
            {
                if (PrefabEffectDestr)
                    Instantiate(PrefabEffectDestr, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }

        public void ReceiveHeal(float healAmount, Vector3 hitPosition, GameAgent sender)
        {

        }
    }
}

