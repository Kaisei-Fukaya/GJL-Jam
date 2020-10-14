using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaMountExit : AreaMountPoint
{
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        base.OnDrawGizmos();
    }
}
