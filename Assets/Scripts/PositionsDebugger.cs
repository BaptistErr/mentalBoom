using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsDebugger : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Vector3 lastPos = this.transform.GetChild(0).position;
        foreach (Transform t in this.transform)
        {
            Vector3 pos = t.position;
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(pos, 2.0F);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(lastPos, pos);
            lastPos = pos;
        }
        Gizmos.DrawLine(lastPos, this.transform.GetChild(0).position);
    }
}
