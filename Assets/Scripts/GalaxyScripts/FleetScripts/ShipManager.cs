using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public Fleet fleetPrefab;

    private List<Fleet> allFleets;

    //Get all nearby fleets and display
    public void Setup(GalaxyGrid grid)
    {
        //this is bascially hardcoded random data for testing
        Fleet fleet = Instantiate<Fleet>(fleetPrefab);
        fleet.transform.SetParent(grid.transform);
        fleet.coordinates = HexCoordinates.FromOffsetCoordinates(0, 0);
        int index = fleet.coordinates.toCellIndex(grid.width);
        GalaxyCell cell = grid.cells[index];
        fleet.transform.localPosition = cell.transform.localPosition;
        cell.Occupiedby.Add(fleet);
        fleet.fleetedShips.Add(new Ship());
    }
}
