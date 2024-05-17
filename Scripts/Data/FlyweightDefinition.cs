using System;
using UnityEngine;

namespace Kosmos6
{
    public enum FlyweightDefType
    {
        ShotGun, ShotPlasma
    }
    public abstract class FlyweightDefinition : ScriptableObject
    {

        [SerializeField] public string Id = "";
        [TextArea]
        [SerializeField] public string Name = "";

        public FlyweightDefType DefinitionType;
        public GameObject DefinitionPrefab;

        public abstract Flyweight Create(); // метод фабрики

        public virtual void OnGet(Flyweight flyweight)
        {
            flyweight.gameObject.SetActive(true);
        }
        public virtual void OnRelease(Flyweight flyweight)
        {
            flyweight.gameObject.SetActive(false);
        }
        public virtual void OnDestroyPoolObject(Flyweight flyweight)
        {
            Destroy(flyweight.gameObject);
        }


        protected void OnValidate()
        {
            if (Id == "")
            {
                Id = Guid.NewGuid().ToString();
            }
        }
    }
}
