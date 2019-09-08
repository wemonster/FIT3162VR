using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using A;
public class ArcDrawer : MonoBehaviour
{
    private List<Vector3> positions = new List<Vector3>();
    private List<float> volumes = new List<float>();
    private List<string> states = new List<string>();
    public Gradient Colors;
    private int SEGMENT_COUNT = 50;
    showArcInfo info;
    List<GameObject> arcs = new List<GameObject>();

    public void DrawArcs(GameObject Earth)
    {
        //int child_count = seriesObj.transform.childCount;
        //int child_count = points.Count;
        int child_count = positions.Count;
        for (int arc = 0; arc < volumes.Count; arc += 1)
        {
            //GameObject origin = seriesObj.transform.GetChild(i).gameObject;
            //GameObject dest = seriesObj.transform.GetChild(i + 1).gameObject;

            //GameObject origin = points[i];
            //GameObject dest = points[i + 1];
            float max_vol = volumes.Max();
            Vector3 origin = positions[arc * 2];
            Vector3 dest = positions[arc * 2 + 1];
            string ostate = states[arc * 2];
            string dstate = states[arc * 2 + 1];
            float volume = volumes[arc];
            GameObject new_arc = new GameObject();

            new_arc.transform.parent = Earth.transform;
            transform.localPosition = new Vector3(0f, 0f, 0f);
            string tag_name = string.Format("{0},{1},{2}", origin, dest, volume);
            new_arc.name = tag_name;
            new_arc.tag = "Arc";

            //add script to the object
            Arc a = new Arc(origin, dest, volume, ostate, dstate);
            new_arc.AddComponent<ArcInfo>().SetArc(a);

            new_arc.AddComponent<BoxCollider>();

            LineRenderer lineRenderer = new_arc.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
            lineRenderer.sortingLayerID = 0;
            lineRenderer.alignment = LineAlignment.TransformZ;

            lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
            lineRenderer.colorGradient = Colors;
            //Debug.Log(volume / max_vol * 10);
            lineRenderer.startColor = Colors.Evaluate(volume / max_vol * 50);
            lineRenderer.endColor = Colors.Evaluate(volume / max_vol * 50);
            //lineRenderer.startColor = Color.yellow;
            //lineRenderer.endColor = Color.red;

            lineRenderer.numCornerVertices = 5;
            lineRenderer.transform.parent = Earth.transform;
            lineRenderer.transform.localPosition = new Vector3(0f, 0f, 0f);

            //new_arc.transform.parent = seriesObj.transform;

            lineRenderer.startWidth = 0.0007f;
            lineRenderer.endWidth = 0.0007f;


            lineRenderer.positionCount = 3;


            //Vector3 apex = new Vector3(-0.2f, 0.2f, -0.2f);

            Vector3 apex = 1.06f * (origin + 0.5f * (dest - origin));
            apex.y = (float)0.5;

            lineRenderer.SetPosition(1, apex);

            //lineRenderer.SetPosition(0, origin.transform.position);
            //lineRenderer.SetPosition(2, dest.transform.position);

            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(2, dest);

            int curveCount = 1;

            for (int j = 0; j < curveCount; j++)
            {
                for (int i = 1; i <= SEGMENT_COUNT; i++)
                {
                    float t = i / (float)SEGMENT_COUNT;
                    int nodeIndex = j * 3;
                    Vector3 pixel = GetPoint(origin, apex, dest, t);
                    lineRenderer.positionCount = (j * SEGMENT_COUNT) + i;
                    lineRenderer.SetPosition((j * SEGMENT_COUNT) + (i - 1), pixel);
                }
            }

            arcs.Add(new_arc);
        }
    }
    public void DestroyArcs()
    {
        foreach (GameObject arc in arcs)
        {
            //Destroy(arc);
            arc.SetActive(false);
        }
        //positions = new List<Vector3>();
        //volumes = new List<float>();
        //states = new List<string>();
        //arcs = new List<GameObject>();
    }
    public void InitialiseArcs()
    {
        foreach (GameObject arc in arcs)
        {
            //Destroy(arc);
            arc.SetActive(true);
        }
    }
    private void Update()
    {
    }

    private IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void showArc(string oslic, string dslic, float volume)
    {

        info.showInfo(oslic, dslic, volume);
    }
    public void AddPosition(Vector3 start_pos, Vector3 end_pos, float volume, string ostate, string dstate)
    {
        positions.Add(start_pos);
        positions.Add(end_pos);
        volumes.Add(volume);
        states.Add(ostate);
        states.Add(dstate);


    }
    public void Printpoints()
    {
        Debug.Log(positions.Count / 2);
    }

    private static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * p0 +
            2f * oneMinusT * t * p1 +
            t * t * p2;
    }
}
