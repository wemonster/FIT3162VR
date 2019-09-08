using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using F;


public class DataVisualizer : MonoBehaviour
{
    public Material PointMaterial;
    public Gradient Colors;
    public GameObject Earth;
    public GameObject PointPrefab;
    public float ValueScaleMultiplier = 1;
    GameObject seriesFac, seriesArc;
    List<GameObject> points = new List<GameObject>();
    int currentSeries = 0;
    public ArcDrawer Drawer;
    Dictionary<string, string> states = new Dictionary<string, string>();
    Dictionary<string, List<Facility>> locations = new Dictionary<string, List<Facility>>();
    Dictionary<Vector3, List<Facility>> buildings = new Dictionary<Vector3, List<Facility>>();

    public void initialiseState(SeriesLocData[] allSeries)
    {
        SeriesLocData seriesData = allSeries[0];
        for (int i = 0; i < seriesData.Data.Length; i += 2)
        {
            string state = seriesData.Data[i + 1];
            if (state == " DC")
            {
                state = " WA";
            }
            states[seriesData.Data[i]] = state;
        }
    }
    public void CreateMeshes(SeriesLocData[] allSeries)
    {
        //seriesFac = new GameObject();
        //GameObject seriesObj = new GameObject(allSeries[0].Name);
        //seriesObj.transform.parent = Earth.transform;
        //seriesFac = seriesObj;
        SeriesLocData seriesData = allSeries[0];
        //Debug.Log(seriesData.Data.Length);
        for (int j = 0; j < seriesData.Data.Length; j += 9)
        {
            string SLIC = seriesData.Data[j].ToString();
            float latitude = float.Parse(seriesData.Data[j + 1]);
            float longitude = float.Parse(seriesData.Data[j + 2]);
            string CC = seriesData.Data[j + 3].ToString();
            string SORT = seriesData.Data[j + 4].ToString();
            string CAP = seriesData.Data[j + 5].ToString();
            string SPAN = seriesData.Data[j + 6].ToString();
            string STRT = seriesData.Data[j + 7].ToString();
            string STATE = seriesData.Data[j + 8].ToString();
            if (STATE == " DC")
            {
                STATE = " WA";
            }
            Facility fac = getFacility(SLIC, latitude, longitude, CC, SORT, CAP, SPAN, STRT, STATE);

            if (!locations.ContainsKey(fac.SLIC))
            {
                List<Facility> loc = new List<Facility>();
                loc.Add(fac);
                locations.Add(fac.SLIC, loc);
            }
            else
            {
                locations[fac.SLIC].Add(fac);
            }

            //draw facility only once
            if (!buildings.ContainsKey(fac.pos))
            {
                //Debug.Log(fac.pos);
                List<Facility> building = new List<Facility>();
                building.Add(fac);
                buildings.Add(fac.pos, building);
                GameObject p = Instantiate<GameObject>(PointPrefab, transform.localPosition, transform.localRotation);
                p.transform.parent = Earth.transform;
                p.transform.localScale = new Vector3(1, 1, 0.001f);
                p.transform.localPosition = fac.pos;
                p.GetComponent<MeshRenderer>().material = PointMaterial;
                p.AddComponent<BoxCollider>();
                p.AddComponent<FacInfo>().SetFac(fac);
                //p.AddComponent<HelloWorld>();
                p.tag = "Location";
                points.Add(p);
            }
            else
            {
                buildings[fac.pos].Add(fac);
            }
        }
    }

    public void createArcs(SeriesLocData[] allSeries)
    {
        //seriesArc = new GameObject();
        //GameObject seriesObj = new GameObject(allSeries[0].Name);
        //seriesObj.transform.parent = Earth.transform;
        //seriesArc = seriesObj;
        SeriesLocData seriesData = allSeries[0];
        //Debug.Log(seriesData.Data.Length / 3500);
        for (int j = 0; j < seriesData.Data.Length / 500; j += 7)
        {
            string OSLIC = seriesData.Data[j];
            float latstart = float.Parse(seriesData.Data[j + 1]);
            float lngstart = float.Parse(seriesData.Data[j + 2]);
            string DSLIC = seriesData.Data[j + 3].ToString();
            float latend = float.Parse(seriesData.Data[j + 4]);
            float lngend = float.Parse(seriesData.Data[j + 5]);
            float volume = float.Parse(seriesData.Data[j + 6]);
            //Debug.Log(OSLIC);
            Vector3 startpos, endpos;
            startpos.x = 0.5f * Mathf.Cos(lngstart * Mathf.Deg2Rad) * Mathf.Cos(latstart * Mathf.Deg2Rad);
            startpos.y = 0.5f * Mathf.Sin(latstart * Mathf.Deg2Rad);
            startpos.z = 0.5f * Mathf.Sin(lngstart * Mathf.Deg2Rad) * Mathf.Cos(latstart * Mathf.Deg2Rad);
            endpos.x = 0.5f * Mathf.Cos(lngend * Mathf.Deg2Rad) * Mathf.Cos(latend * Mathf.Deg2Rad);
            endpos.y = 0.5f * Mathf.Sin(latend * Mathf.Deg2Rad);
            endpos.z = 0.5f * Mathf.Sin(lngend * Mathf.Deg2Rad) * Mathf.Cos(latend * Mathf.Deg2Rad);
            string ostate = "WA", dstate = "WA";
            //Debug.LogFormat("OSLIC:{0},DSLIC:{1},OSTATE:{2}", OSLIC, DSLIC, states[OSLIC]);
            if (states.ContainsKey(OSLIC))
            {
                ostate = states[OSLIC];
            }
            if (states.ContainsKey(DSLIC))
            {
                dstate = states[DSLIC];
            }
            Drawer.AddPosition(startpos, endpos, volume, ostate, dstate);
        }
        Drawer.DrawArcs(Earth);
    }
    public void ClearAll()
    {
        //clear all nodes
        foreach (GameObject p in points)
        {
            p.SetActive(false);
        }
        //points = new List<GameObject>();
        //locations = new Dictionary<string, List<Facility>>();
        //buildings = new Dictionary<Vector3, List<Facility>>();
        //clear all arcs
        Drawer.DestroyArcs();
    }

    public void initialise()
    {
        foreach (GameObject p in points)
        {
            p.SetActive(true);
        }
        Drawer.InitialiseArcs();
    }

    public void ActivateSeries(int seriesIndex)
    {
        //if (seriesIndex >= 0 && seriesIndex < seriesObjects.Length)
        //{
        //    seriesObjects[currentSeries].SetActive(false);
        //    currentSeries = seriesIndex;
        //    seriesObjects[currentSeries].SetActive(true);
        //}
    }
    private Facility getFacility(string SLIC, float latstart, float lngstart, string CC = null, string SORT = null, string CAP = null, string SPAN = null, string START = null, string STATE = null)
    {
        Vector3 pos;
        pos.x = 0.5f * Mathf.Cos((lngstart) * Mathf.Deg2Rad) * Mathf.Cos(latstart * Mathf.Deg2Rad);
        pos.y = 0.5f * Mathf.Sin(latstart * Mathf.Deg2Rad);
        pos.z = 0.5f * Mathf.Sin((lngstart) * Mathf.Deg2Rad) * Mathf.Cos(latstart * Mathf.Deg2Rad);
        return new Facility(pos, SLIC, CC, SORT, CAP, SPAN, START, STATE);
    }
}
[System.Serializable]
public class SeriesLocData
{
    public string Name;
    public string[] Data;
}

[System.Serializable]
public class SeriesData
{
    public string Name;
    public float[] Data;
}
