using UnityEngine;

[System.Serializable]
public struct HexCoordinates {

	[SerializeField]
	private int x, y;

	public int X {
		get {
			return x;
		}
	}

	public int Y {
		get {
			return y;
		}
	}

	public int Z {
		get {
			return -X - Y;
		}
	}

	public HexCoordinates (int x, int y) {
		this.x = x;
		this.y = y;
	}

	public static HexCoordinates FromOffsetCoordinates (int x, int y) {
		return new HexCoordinates(x - y / 2, y);
	}

	public static HexCoordinates FromPosition(Vector3 position)
	{
		float x = position.x / (HexMetrics.innerRadius * 2f);
		float z = -x;

		float offset = position.y / (HexMetrics.outerRadius * 3f);
		x -= offset;
		z -= offset;

		int iX = Mathf.RoundToInt(x);
		int iY = Mathf.RoundToInt(z);
		int iZ = Mathf.RoundToInt(-x -z);

		if (iX + iY + iZ != 0)
		{
			float dX = Mathf.Abs(x - iX);
			float dY = Mathf.Abs(z - iY);
			float dZ = Mathf.Abs(-x - z - iZ);

			if (dX > dY && dX > dZ)
			{
				iX = -iY - iZ;
			}
			else if (dZ > dY)
			{
				iZ = -iX - iY;
			}
		}

		return new HexCoordinates(iX, iZ);
	}
	public int toCellIndex(int width)
    {

		return X + Y * width + Y / 2;

	}
	public override string ToString () {
		return "(" + X.ToString() + ", " + Z.ToString() + ", " + Y.ToString() + ")";
	}

	public string ToStringOnSeparateLines () {
		return X.ToString() + "\n" + Z.ToString() + "\n" + Y.ToString();
	}
}