using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showArcInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void showInfo(string oslic,string dslic,float volume)
    {
        Text origin = transform.Find("Origin").GetComponent<Text>();
        Debug.Log(origin);
        origin.text = oslic;

        Text destination = transform.Find("Destination").GetComponent<Text>();
        destination.text = dslic;

        Text vol = transform.Find("Volume").GetComponent<Text>();
        string txt = string.Format("{0}", volume);
        vol.text = txt;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
