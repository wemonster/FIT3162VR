using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using F;
using A;
public class StateClick : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] arcs;
    GameObject[] reference;
    Dictionary<string, List<GameObject>> Arcs = new Dictionary<string, List<GameObject>>();
    bool created = false;
    void Start()
    {
        

        //Debug.Log(facilities.Length);
        //reference = (GameObject[])facilities.Clone();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!created)
        {
            arcs = GameObject.FindGameObjectsWithTag("Arc");
            if (arcs.Length != 0)
            {
                created = true;
                reference = (GameObject[])arcs.Clone();
                foreach (GameObject arc in arcs)
                {
                    Arc a = arc.GetComponent<ArcInfo>().GetArc();
                    if (!Arcs.ContainsKey(a.OriginState))
                    {
                        List<GameObject> f = new List<GameObject>();
                        f.Add(arc);
                        Arcs.Add(a.OriginState, f);
                    }
                    else
                    {
                        Arcs[a.OriginState].Add(arc);
                    }
                }
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                //Debug.Log(obj);
                if (obj.tag == "State")
                {
                    foreach (GameObject fac in reference)
                    {
                        if (fac.activeSelf == false)
                        {
                            fac.SetActive(true);
                        }
                        else
                        {
                            fac.SetActive(false);
                        }
                    }
                }
            }

        }
        wait(3);
    }

    private IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
