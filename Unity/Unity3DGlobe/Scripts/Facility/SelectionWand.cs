using F;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionWand : MonoBehaviour
{
    public Material HighlightMaterial;
    public showLocationInfo locationInfo;
    public showArcInfo arcInfo;

    private FacInfo currentlySelected = null;

    public Facility GetSelectedFac()
    {
        return currentlySelected ? currentlySelected.GetFac() : null;
    }

    private void OnTriggerEnter(Collider other)
    {
        FacInfo facInfo = other.GetComponent<FacInfo>();
        if (facInfo)
        {
            if (currentlySelected)
            {
                currentlySelected.GetComponent<MeshRenderer>().material = FacInfo.PointMaterial;
            }
            locationInfo.updateLoc(facInfo.GetFac());
            other.GetComponent<MeshRenderer>().material = HighlightMaterial;
            currentlySelected = facInfo;
            arcInfo.UpdateInfo();
        }
    }
}
