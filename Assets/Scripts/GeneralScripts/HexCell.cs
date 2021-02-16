using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexCell : MonoBehaviour {

	public Guid HexId = Guid.NewGuid();

	public HexCoordinates coordinates;

	public Color color;

	public List<Guid> ObjectsOnHex = new List<Guid>();

	public List<Guid> HexFeatures = new List<Guid>();
}