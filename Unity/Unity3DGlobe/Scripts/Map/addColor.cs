using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addColor : MonoBehaviour
{
    Dictionary<string, Color> colors = new Dictionary<string, Color>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] states = GameObject.FindGameObjectsWithTag("State");

        foreach (GameObject state in states)
        {
            float r = Random.Range(0.0f, 1.0f);
            float g = Random.Range(0.0f, 1.0f);
            float b = Random.Range(0.0f, 1.0f);
            colors[state.name] = new Color(r, g, b);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //wait(5);
        Dictionary<string, bool> chosen = GameObject.FindGameObjectWithTag("Map").GetComponent<StateClick>().getChosen();
        foreach (var item in chosen)
        {
            if (item.Key == " PR")
            {
                continue;
            }
            MeshRenderer renderer;
            MaterialPropertyBlock props = new MaterialPropertyBlock();
            Color color = Color.white;
            if (item.Value)
            {
                color = colors[item.Key];
            }
            GameObject state = GameObject.Find(item.Key);
            if (state)
            {
                renderer = state.GetComponent<MeshRenderer>();
                props.SetColor("_Color", color);
                renderer.SetPropertyBlock(props);
            }


        }

    }
    private IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
