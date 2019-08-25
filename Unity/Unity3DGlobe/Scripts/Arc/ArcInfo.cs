using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A;
public class ArcInfo : MonoBehaviour
{
    private Arc arc;

    public void SetArc(Arc arc)
    {
        this.arc = arc;
    }

    public Arc GetArc()
    {
        return arc;
    }

}
