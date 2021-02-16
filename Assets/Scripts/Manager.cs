using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	public HexGrid grid;

	public ShipManager ships;

	private Ship selectedShip;
	private HexCell selectedCell;


	void Start()
	{
		grid.CreateGrid();

		ships.Setup(grid);
	}
	void Update()
	{
		if (Input.GetMouseButton(0))
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
		//forgive me for this exo, ill clean it all up tomorrow :) 

		position = transform.TransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.toCellIndex(grid.width);
		HexCell cell = grid.cells[index];

		if (selectedShip != null)
		{
			selectedShip.transform.localPosition = cell.transform.localPosition;

			selectedCell.Occupiedby = null;
			selectedCell.color = Color.black;
			cell.Occupiedby = selectedShip;

			selectedCell = null;
			selectedShip = null;		
		}
		else
		{
			if (cell.Occupiedby != null)
			{
				cell.color = Color.blue;
				selectedShip = cell.Occupiedby;
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
