using IG;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Kosmos6
{
    public class FactoryFlyweight : SingletonManager<FactoryFlyweight>
    {
        [SerializeField] private bool _collectionCheck = true;
        [SerializeField] private int _defaultCapacity = 50;
        [SerializeField] private int _maxSize = 150;

        private Dictionary<FlyweightDefType, IObjectPool<Flyweight>> _pools = new Dictionary<FlyweightDefType, IObjectPool<Flyweight>>();

        public Flyweight Spawn(FlyweightDefinition definition)
        {
            return GetPoolForDefinition(definition)?.Get();
        }
        public Flyweight Spawn(FlyweightDefinition definition, Vector3 position, Quaternion rotation)
        {
            var flyweight = GetPoolForDefinition(definition)?.Get();
            flyweight.transform.position = position;
            flyweight.transform.rotation = rotation;
            return flyweight;
        }

        public void ReturnToPool(Flyweight flyweight)
        {
            GetPoolForDefinition(flyweight.Definition)?.Release(flyweight);
        }

        private IObjectPool<Flyweight> GetPoolForDefinition(FlyweightDefinition defintion)
        {
            IObjectPool<Flyweight> pool;
            if (_pools.TryGetValue(defintion.DefinitionType, out pool))
                return pool;

            pool = new ObjectPool<Flyweight>(
                defintion.Create,
                defintion.OnGet,
                defintion.OnRelease,
                defintion.OnDestroyPoolObject,
                _collectionCheck,
                _defaultCapacity,
                _maxSize);


            return pool;
        }
    }
}
