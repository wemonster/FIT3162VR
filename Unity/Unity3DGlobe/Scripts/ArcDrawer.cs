using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcDrawer : MonoBehaviour
{
    private List<Vector3> positions = new List<Vector3>();
    public Gradient Colors;
    private int SEGMENT_COUNT = 50;

    public void DrawArcs(GameObject Earth)
    {
        //int child_count = seriesObj.transform.childCount;
        //int child_count = points.Count;
        int child_count = positions.Count;

        Debug.Log(child_count);

        for (int arc = 0; arc < child_count - 6; arc += 7)
        {
            //GameObject origin = seriesObj.transform.GetChild(i).gameObject;
            //GameObject dest = seriesObj.transform.GetChild(i + 1).gameObject;

            //GameObject origin = points[i];
            //GameObject dest = points[i + 1];

            Vector3 origin = positions[arc];
            Vector3 dest = positions[arc + 5];

            GameObject new_arc = new GameObject();
            new_arc.transform.parent = Earth.transform;
            transform.localPosition = new Vector3(0f, 0f, 0f);
            LineRenderer lineRenderer = new_arc.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
            lineRenderer.sortingLayerID = 0;
            lineRenderer.alignment = LineAlignment.TransformZ;

            lineRenderer.colorGradient = Colors;
            lineRenderer.startColor = Colors.Evaluate(0f);
            lineRenderer.endColor = Colors.Evaluate(1f);

            lineRenderer.numCornerVertices = 5;
            lineRenderer.transform.parent = Earth.transform;
            lineRenderer.transform.localPosition = new Vector3(0f, 0f, 0f);

            //new_arc.transform.parent = seriesObj.transform;

            lineRenderer.startWidth = 0.0007f;
            lineRenderer.endWidth = 0.0007f;


            lineRenderer.positionCount = 3;

            //Vector3 apex = new Vector3(-0.2f, 0.2f, -0.2f);
            Vector3 apex = 1.06f * (origin + 0.5f * (dest - origin));


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



        }
    }
    public void AddPosition(Vector3 new_pos)
    {
        positions.Add(new_pos);
    }

    public void Print_Length()
    {
        Debug.Log(50);
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
