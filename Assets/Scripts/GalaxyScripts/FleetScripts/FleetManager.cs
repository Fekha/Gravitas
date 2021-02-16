using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FleetManager : MonoBehaviour
{
    public Fleet fleetPrefab;

    private List<Fleet> allFleets = new List<Fleet>();

    public void Setup(HexGrid grid)
    {
        allFleets.Clear();
        //eventually spawning all players ships in range
        Fleet fleet = Instantiate<Fleet>(fleetPrefab);
        Ship ship = new Ship();
        fleet.ships.Add(ship);
        fleet.transform.SetParent(grid.transform);
        fleet.transform.localPosition = new Vector2(0, 0);
        fleet.coordinates = HexCoordinates.FromOffsetCoordinates(0, 0);
        int index = fleet.coordinates.toCellIndex(grid.width);
        HexCell cell = grid.cells[index];
        cell.ObjectsOnHex.Add(fleet.FleetId);
        allFleets.Add(fleet);
    }

    public Fleet GetFleetbyId(Guid fleetId)
    {
        return allFleets.FirstOrDefault(x => x.FleetId == fleetId);
    }
}
