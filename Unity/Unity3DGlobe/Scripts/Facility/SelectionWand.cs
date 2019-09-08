using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionWand : MonoBehaviour
{

    public showLocationInfo locationInfo;

    private void OnTriggerEnter(Collider other)
    {
        FacInfo facInfo = other.GetComponent<FacInfo>();
        if (facInfo)
        {
            locationInfo.updateLoc(facInfo.GetFac());
        }
    }
}
