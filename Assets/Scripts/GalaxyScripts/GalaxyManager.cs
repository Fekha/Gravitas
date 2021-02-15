using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GalaxyManager : MonoBehaviour
{
	public GalaxyGrid grid;

	public ShipManager ships;

	private Fleet selectedFleet;
	private GalaxyCell selectedCell;


	void Start()
	{
		grid.CreateGrid();

		ships.Setup(grid);
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

    private IEnumerator TouchCell(Vector2 position)
	{
		//forgive me for this exo, all of this is bad practice below, ill clean all this up when im done testing 

		position = transform.TransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.toCellIndex(grid.width);
		GalaxyCell cell = grid.cells[index];

		if (selectedFleet != null)
		{
			selectedFleet.transform.localPosition = cell.transform.localPosition;

			selectedCell.Occupiedby.Remove(selectedFleet);
			selectedCell.color = Color.black;
			cell.Occupiedby.Add(selectedFleet);

			selectedCell = null;
			selectedFleet = null;		
		}
		else
		{
			if (cell.Occupiedby.Count > 0)
			{
				cell.color = Color.blue;
				selectedFleet = cell.Occupiedby[0];
				selectedCell = cell;
            }
			else if (cell.StarId != Guid.Empty)
			{
				SceneManager.LoadScene("PlanetView");
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
