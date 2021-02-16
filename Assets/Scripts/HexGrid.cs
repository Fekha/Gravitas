using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	public int width;
	public int height;

	private Color defaultColor = Color.black;

	public HexCell cellPrefab;
	public Text cellLabelPrefab;

	public List<Sprite> sprites;

	public HexCell[] cells;

	Canvas gridCanvas;
	public void CreateGrid()
    {
		gridCanvas = GetComponentInChildren<Canvas>();
		
		width = Random.Range(2, 8);
		height = Random.Range(2, 8);
		cells = new HexCell[height * width];

		for (int y = 0, i = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				CreateCell(x, y, i++);
			}
		}

		UpdateBoard();
	}

	void CreateCell (int x, int y, int i) {
		Vector2 position;
		position.x = (x + y * 0.5f - y / 2) * (HexMetrics.innerRadius * 2f);
		position.y = y * (HexMetrics.outerRadius * 1.5f);
		
		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, y);
        cell.color = defaultColor;
		if(Random.Range(0, 2) > 0)
			cell.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 6)];

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