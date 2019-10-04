using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using F;
public class FacInfo : MonoBehaviour
{
    public static Material PointMaterial;
    //public static Material HighlightMaterial;

    private Facility fac;

    public void SetFac(Facility fac)
    {
        this.fac = fac;
    }

    public Facility GetFac()
    {
        return fac;
    }
    
}
