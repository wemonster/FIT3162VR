using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = new GameObject("text");
        Text txt = obj.AddComponent<Text>();
        txt.text = "hellworld";
        txt.color = Color.white;
        txt.font = Resources.GetBuiltinResource(typeof(Font),"Arial.ttf") as Font;
        txt.transform.parent = gameObject.transform;
        txt.transform.localPosition = new Vector3(0,0,0);
        txt.transform.localScale = new Vector3(1, 1, 1);
       
        Debug.Log(txt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
