using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class RememberFixedObstaclePosition : MonoBehaviour,IRememberFixedObatclePosition
{
    private int _rememberPosition = 0;

    public int RememberPosition { get { return _rememberPosition; } }

    public void RememberPoint(int remember)
    {
        _rememberPosition = remember;
    }
}