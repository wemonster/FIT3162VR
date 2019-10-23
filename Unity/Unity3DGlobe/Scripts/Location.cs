using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using F;
public class Location : MonoBehaviour
{
    private Facility fac;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetFac(Facility fac)
    {
        this.fac = fac;
    }

    public Facility GetFac()
    {
        return fac;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
