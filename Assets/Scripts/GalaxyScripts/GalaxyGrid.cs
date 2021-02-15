using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalaxyGrid : MonoBehaviour {

<<<<<<< Updated upstream:Assets/Scripts/HexGrid.cs
	int width;
	int height;
=======
	public int width;
	public int height;
	public int size;
>>>>>>> Stashed changes:Assets/Scripts/GalaxyScripts/GalaxyGrid.cs

	public Color defaultColor = Color.white;
	public Color touchedColor = Color.magenta;

	public GalaxyCell cellPrefab;
	public Text cellLabelPrefab;

	public List<Sprite> sprites;

<<<<<<< Updated upstream:Assets/Scripts/HexGrid.cs
	HexCell[] cells;
=======
	public GalaxyCell[] cells;
>>>>>>> Stashed changes:Assets/Scripts/GalaxyScripts/GalaxyGrid.cs

	Canvas gridCanvas;
	HexMesh hexMesh;

	void Awake () {
		gridCanvas = GetComponentInChildren<Canvas>();
<<<<<<< Updated upstream:Assets/Scripts/HexGrid.cs
		hexMesh = GetComponentInChildren<HexMesh>();
		width = Random.Range(2, 8);
		height = Random.Range(2, 8);
		cells = new HexCell[height * width];

		for (int y = 0, i = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
=======
		//var size = UnityEngine.Random.Range(2, 8);
		//size always needs to be odd
		size = 5;
		width = size;
		height = size;
		cells = new GalaxyCell[height * width];

		for (int y = 0, i = 0; y < height; y++)
		{
			//var range = width - Mathf.Max((size / 2) - y, y - size / 2);
			for (int x = 0; x < width; x++)
			{
>>>>>>> Stashed changes:Assets/Scripts/GalaxyScripts/GalaxyGrid.cs
				CreateCell(x, y, i++);
				//range - (int)Math.Ceiling((double)(size / 2))
			}
		}
	}

	void Start () {
		hexMesh.Triangulate(cells);
	}

	void Update () {
		if (Input.GetMouseButton(0)) {
			HandleInput();
		}
	}

	void HandleInput () {
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit)) {
			TouchCell(hit.point);
		}
	}

	void TouchCell (Vector3 position) {
		position = transform.TransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.X + coordinates.Y * width + coordinates.Y / 2;
		HexCell cell = cells[index];
		cell.color = touchedColor;
		hexMesh.Triangulate(cells);
	}

	void CreateCell (int x, int y, int i) {
		Vector2 position;
		position.x = (x + y * 0.5f - y / 2) * (HexMetrics.innerRadius * 2f);
		position.y = y * (HexMetrics.outerRadius * 1.5f);

		GalaxyCell cell = cells[i] = Instantiate<GalaxyCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x,y);
        cell.color = defaultColor;
		if (UnityEngine.Random.Range(0, 2) > 0)
		{
			cell.GetComponent<SpriteRenderer>().sprite = sprites[UnityEngine.Random.Range(0, 6)];
			cell.StarId = Guid.NewGuid(); //to be added
		}

        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
        label.text = cell.coordinates.ToStringOnSeparateLines();
    }
}