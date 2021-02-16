using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public HexGrid grid;

	public FleetManager fleetManager;

	private Fleet selectedFleet;
	private HexCell selectedCell;


	void Start()
	{
		grid.CreateGrid();

		fleetManager.Setup(grid);
	}
	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			HandleInput();
		}
	}

	void HandleInput()
	{
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit))
		{
			StartCoroutine(TouchCell(hit.point));
		}
	}

    private IEnumerator TouchCell(Vector3 position)
	{
		position = transform.TransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.toCellIndex(grid.width);
		HexCell cell = grid.cells[index];

		if (selectedFleet != null)
		{
			selectedFleet.transform.localPosition = cell.transform.localPosition;

			
			selectedCell.ObjectsOnHex.RemoveAt(0);//temp
			selectedCell.color = Color.black;
			cell.ObjectsOnHex.Add(selectedFleet.FleetId);

			selectedCell = null;
			selectedFleet = null;
		}
		else
		{
			if (cell.ObjectsOnHex.Count > 0)
			{
				cell.color = Color.blue;
				selectedFleet = fleetManager.GetFleetbyId(cell.ObjectsOnHex[0]);//temp
				selectedCell = cell;
            }
            else
            {
				if (selectedCell != null)
				{
					selectedCell.color = Color.black;
					selectedCell = null;
				}
				
			}
		}
		grid.UpdateBoard();
		yield return new WaitForSeconds(2);
	}
}
