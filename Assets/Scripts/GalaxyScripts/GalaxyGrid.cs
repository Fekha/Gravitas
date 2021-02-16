using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalaxyGrid : MonoBehaviour {

	public int width;
	public int height;
	public int size;

	private Color defaultColor = Color.black;

	public GalaxyCell cellPrefab;
	public Text cellLabelPrefab;

	public List<Sprite> sprites;

	public GalaxyCell[] cells;

	Canvas gridCanvas;
	public void CreateGrid()
    {
		gridCanvas = GetComponentInChildren<Canvas>();
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
				CreateCell(x, y, i++);
				//range - (int)Math.Ceiling((double)(size / 2))
			}
		}

		UpdateBoard();
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

    public void UpdateBoard()
    {
		HexMesh hexMesh = GetComponentInChildren<HexMesh>();
		hexMesh.Triangulate(cells);
	}
}