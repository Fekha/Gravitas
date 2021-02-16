using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{
    public Guid FleetId = Guid.NewGuid();

    public HexCoordinates coordinates;

    public Color teamColor;

    public List<Ship> ships;
}
