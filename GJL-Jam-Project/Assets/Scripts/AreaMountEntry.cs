using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaMountEntry : AreaMountPoint
{
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        base.OnDrawGizmos();
    }
}
