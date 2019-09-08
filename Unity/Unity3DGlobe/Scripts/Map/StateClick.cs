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
    Dictionary<string, bool> chosen = new Dictionary<string, bool>();
    bool created = false;
    void Start()
    {
        string[] states = {" WY"," WV"," WI"," WA"," VT"," VA"," UT"," TX"," TN"," SD"," SC"," RI"," PA"," OR",
            " OK"," OH"," NY"," NV"," NM"," NJ"," NH"," NE"," ND"," NC"," MT"," MS"," MO"," MN"," MI"," ME"," MD",
            " MA"," LA"," KY"," KS"," IN"," IL"," ID"," IA"," HI"," GA"," FL"," DE"," CT"," CO"," CA"," AZ",
            " AR"," AL"," PR"};
        foreach (string s in states)
        {
            chosen[s] = false;
        }
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
                Debug.Log(reference.Length);
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
                    //Debug.Log(Input.mousePosition);
                    if (chosen[obj.name] == false)
                    {
                        chosen[obj.name] = true;
                    }
                    else
                    {
                        chosen[obj.name] = false;
                    }

                }

            }
        }
        foreach (GameObject arc in reference)
        {
            Arc a = arc.GetComponent<ArcInfo>().GetArc();
            if (!chosen.ContainsKey(a.OriginState) || !chosen.ContainsKey(a.DestState))
            {
                //Debug.LogFormat("{0},{1}", a.OriginState, a.DestState);
                continue;
            }
            if (chosen[a.OriginState] && chosen[a.DestState])
            {
                arc.SetActive(true);
            }
            else
            {
                arc.SetActive(false);
            }
        }
        wait(3);
    }
    public Dictionary<string, bool> getChosen()
    {
        return chosen;
    }

    private IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
