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

    private int actionPoints;

    public int getAP()
    {
        return actionPoints;
    }
    public int setAP(int i)
    {
        return actionPoints + i;
    }
}
