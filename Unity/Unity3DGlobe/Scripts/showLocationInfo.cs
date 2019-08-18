using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showLocationInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int children = transform.childCount;
        string[] attributes = { "SLIC", "CC", "SORT", "CAP", "SPAN", "START" };
        Text SLIC = transform.Find("SLIC").GetComponent<Text>();
        SLIC.text = "89";
        SLIC.color = Color.white;
        //SLIC.transform.position = new Vector3(0, 0, 0);

        Text CC = transform.Find("CC").GetComponent<Text>();
        CC.text = "PR";

        Text SORT = transform.Find("SORT").GetComponent<Text>();
        SORT.text = "P";

        Text CAP = transform.Find("CAP").GetComponent<Text>();
        CAP.text = "69";

        Text SPAN = transform.Find("SPAN").GetComponent<Text>();
        SPAN.text = "403";

        Text START = transform.Find("START").GetComponent<Text>();
        START.text = "400";
        //foreach (var item in attributes)
        //{
        //    Text txt = transform.Find("Text").GetComponent<Text>();
        //}
        //Text[] txts = new Text[children];

        //for (int i = 0; i < children; i++)
        //{
        //    Text txt = (Text)transform.GetChild(i);
        //    Debug.Log(transform.GetChild(i));
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
