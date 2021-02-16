using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public Ship shipPrefab;

    private List<Ship> allShips;

    public void Setup(HexGrid grid)
    {
        //eventually spawning all players ships in range
        Ship ship = Instantiate<Ship>(shipPrefab);
        ship.transform.SetParent(grid.transform);
        ship.transform.localPosition = new Vector2(0, 0);
        ship.coordinates = HexCoordinates.FromOffsetCoordinates(0, 0);
        int index = ship.coordinates.toCellIndex(grid.width);
        HexCell cell = grid.cells[index];
        cell.Occupiedby = ship;
        allShips.Add(ship);
    }
}
