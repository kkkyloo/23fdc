using System;
using UnityEngine;

namespace Kosmos6
{
    [CreateAssetMenu(fileName = "Def WS Default", menuName = "Definitions/Battle/WeaponSpawnerDefiniton")]
    public class WeaponSpawnerDefinition : ScriptableObject
    {

        [SerializeField] public string Id = "";
        [TextArea]
        [SerializeField] public string Name = "";
        [Range(0f, 5f)]
        public float CooldownTimeTotal = 0.25f;

        private void OnValidate()
        {
            if (Id == "")
            {
                Id = Guid.NewGuid().ToString();
            }
        }
    }
}
