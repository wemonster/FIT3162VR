using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] states = GameObject.FindGameObjectsWithTag("State");
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;
        foreach (GameObject state in states)
        {
            float r = Random.Range(0.0f, 1.0f);
            float g = Random.Range(0.0f, 1.0f);
            float b = Random.Range(0.0f, 1.0f);
            renderer = state.GetComponent<MeshRenderer>();
            props.SetColor("_Color", new Color(r, g, b));
            renderer.SetPropertyBlock(props);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
