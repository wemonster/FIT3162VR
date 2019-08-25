using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using F;
public class FacInfo : MonoBehaviour
{
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
