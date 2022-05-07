using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private double _x;
    private double _z;

    public double X => _x;
    public double Z => _z;

    public SpawnPoint(double x, double z)
    {
        _x = x;
        _z = z;
    }
}