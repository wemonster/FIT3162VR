﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showArcInfo : MonoBehaviour
{
    private Text origin;
    private Text destination;
    private Text vol;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void showInfo(string oslic,string dslic,float volume)
    {
        origin = transform.Find("Origin").GetComponent<Text>();
        Debug.Log(origin);
        origin.text = oslic;
        destination = transform.Find("Destination").GetComponent<Text>();
        destination.text = dslic;

        vol = transform.Find("Volume").GetComponent<Text>();
        string txt = string.Format("{0}", volume);
        vol.text = txt;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.tag == "Arc")
                {
                }
            }
            wait(1);
        }
    }

    private IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}