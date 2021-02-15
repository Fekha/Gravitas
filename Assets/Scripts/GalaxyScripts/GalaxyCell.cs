using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GalaxyCell : HexCell {
	//this will be list of guids later
	public List<Fleet> Occupiedby;

	public Guid StarId;
}