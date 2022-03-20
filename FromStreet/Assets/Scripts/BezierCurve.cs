using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BezierCurve
{
    public Vector3 OnePointBezierCurve(Vector3 point1, Vector3 point2, Vector3 point3, float value)
    {
        Vector3 a = Vector3.Lerp(point1, point2, value);
        Vector3 b = Vector3.Lerp(point2, point3, value);

        Vector3 c = Vector3.Lerp(a, b, value);

        return c;
    }
}

