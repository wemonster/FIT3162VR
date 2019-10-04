using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using F;
using A;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

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
            " AR"," AL"," AK", " PR"};
        foreach (string s in states)
        {
            chosen[s] = false;
        }

        foreach (Hand hand in Player.instance.hands)
        {
            var lp = hand.GetComponent<SteamVR_LaserPointer>();
            if (lp) lp.PointerClick += OnPointerClick;
        }
        //Debug.Log(facilities.Length);
        //reference = (GameObject[])facilities.Clone();
    }
    // Update is called once per frame
    //void Update()
    //{

    //    //if (!created)
    //    //{
    //    //    arcs = GameObject.FindGameObjectsWithTag("Arc");
    //    //    if (arcs.Length != 0)
    //    //    {
    //    //        created = true;
    //    //        reference = (GameObject[])arcs.Clone();
    //    //        Debug.Log(reference.Length);
    //    //        foreach (GameObject arc in arcs)
    //    //        {
    //    //            ArcData a = arc.GetComponent<Arc>().GetArc();
    //    //            if (!Arcs.ContainsKey(a.OriginState))
    //    //            {
    //    //                List<GameObject> f = new List<GameObject>();
    //    //                f.Add(arc);
    //    //                Arcs.Add(a.OriginState, f);
    //    //            }
    //    //            else
    //    //            {
    //    //                Arcs[a.OriginState].Add(arc);
    //    //            }
    //    //        }
    //    //    }
    //    //    UpdateArcs();
    //    //}

    //    if (Input.GetMouseButton(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            GameObject obj = hit.collider.gameObject;
    //            //Debug.Log(obj);

    //            if (obj.tag == "State")
    //            {
    //                //Debug.Log(Input.mousePosition);
    //                if (chosen[obj.name] == false)
    //                {
    //                    chosen[obj.name] = true;
    //                }
    //                else
    //                {
    //                    chosen[obj.name] = false;
    //                }

    //            }

    //        }
    //    }
    //    //foreach (GameObject arc in reference)
    //    //{
    //    //    ArcData a = arc.GetComponent<Arc>().GetArc();
    //    //    //Debug.Log(a.DestState + " " + a.OriginState);
    //    //    if (!chosen.ContainsKey(a.OriginState) || !chosen.ContainsKey(a.DestState))
    //    //    {
    
    //    //        continue;
    //    //    }
    //    //    if (chosen[a.OriginState] && chosen[a.DestState])
    //    //    {
    //    //        //Debug.Log("activated arc!");
    //    //        arc.SetActive(true);
    //    //    }
    //    //    else
    //    //    {
    //    //        arc.SetActive(false);
    //    //    }
    //    //}
    //    //Debug.Log(chosen.ContainsValue(true));
    //    wait(3);
    //}

    public void Initialise()
    {
        arcs = GameObject.FindGameObjectsWithTag("Arc");
        if (arcs.Length != 0)
        {
            created = true;
            reference = (GameObject[])arcs.Clone();
            Debug.Log(reference.Length);
            foreach (GameObject arc in arcs)
            {
                ArcData a = arc.GetComponent<Arc>().GetArc();
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
        UpdateArcs();
    }

    public void UpdateArcs()
    {
        var activated_arcs = new List<Arc>();
        float max_volume = 0f;
        foreach (GameObject arc in reference)
        {
            Arc ai = arc.GetComponent<Arc>();
            ArcData a = ai.GetArc();
            //Debug.Log(a.DestState + " " + a.OriginState);
            if (!chosen.ContainsKey(a.OriginState) || !chosen.ContainsKey(a.DestState))
            {
                Debug.Log(a.OriginState + " " + a.DestState);
                continue;
            }
            if (chosen[a.OriginState] && chosen[a.DestState])
            {
                //Debug.Log("activated arc!");
                arc.SetActive(true);
                activated_arcs.Add(ai);
                ai.Init();
                max_volume = max_volume < a.volume ? a.volume : max_volume;
            }
            else
            {
                arc.SetActive(false);
            }
        }
        Arc.MaxVolume = max_volume;
        foreach (Arc arc in activated_arcs)
        {
            arc.Dirty();
        }
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        GameObject obj = e.target.transform.gameObject;
        Debug.Log(obj.name);
        if (obj.tag == "State")
        {
            if (chosen[obj.name] == false)
            {
                chosen[obj.name] = true;
            }
            else
            {
                chosen[obj.name] = false;
            }

            UpdateArcs();
        }
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
