using System;
using UnityEngine;

namespace Kosmos6
{
    [CreateAssetMenu(fileName = "Def Proj Velocity Default", menuName = "Definitions/Battle/ProjectileVelocityDefinition")]
    public class ProjectileVelocityDefinition : ScriptableObject
    {

        [SerializeField] public string Id = "";
        [TextArea]
        [SerializeField] public string Name = "";

        [SerializeField] public float Velocity = 300f;




        private void OnValidate()
        {
            if (Id == "")
            {
                Id = Guid.NewGuid().ToString();
            }
        }
    }
}
