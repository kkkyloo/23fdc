using UnityEngine;

namespace Kosmos6
{
    public class SpaceShip : MonoBehaviour
    {
        public GameAgent ShipAgent;

        public IInputShipMovement InputShipMovement;
        public IInputShipWeapons InputShipWeapons;
        public IInputShipMovement InputShipAcceleration;


        private void OnEnable()
        {
            if (ShipAgent == null)
                ShipAgent = GetComponent<GameAgent>();
  
            //input
            if (InputShipMovement == null)
                InputShipMovement = GetComponent<IInputShipMovement>();
            if (InputShipWeapons == null)
                InputShipWeapons = GetComponent<IInputShipWeapons>();
            if (InputShipAcceleration == null)
                InputShipAcceleration = GetComponent<IInputShipMovement>();

        }
    }

}
