using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class ManagerPlayerShips : MonoBehaviour
    {
        [SerializeField] private int CurrentShipId;
        [SerializeField] private Transform ShipVisuals;
        [SerializeField] private GameObject CurrentShip;
        [SerializeField] private List<GameObject> ShipsPrefabs = new List<GameObject>();

        private void Update()
        {
            if (Input.GetButtonDown("ShipChange"))
                ChangeShipToNext();
        }
        private void ChangeShipToNext()
        {
            CurrentShipId++;
            if (CurrentShipId == ShipsPrefabs.Count)
                CurrentShipId = 0;

            ChangeShip();
        }
        private void ChangeShip()
        {
            Destroy(CurrentShip);
            CurrentShip = Instantiate(ShipsPrefabs[CurrentShipId], ShipVisuals);
        }
    }
}
