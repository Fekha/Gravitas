using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShips : MonoBehaviour
{
    public Ship shipPrefab;

    private void Awake()
    {
        Ship ship = Instantiate<Ship>(shipPrefab);
        ship.transform.localPosition = new Vector2(0,0);
        ship.coordinates = HexCoordinates.FromOffsetCoordinates(0, 0);
    }
}
