using UnityEngine;

namespace Kosmos6
{
    public class GameAgent : MonoBehaviour
    {
        public enum Faction
        {
            Player,
            Allies,
            SeventhStar
        }

        public Faction ShipFaction;
    }

}
