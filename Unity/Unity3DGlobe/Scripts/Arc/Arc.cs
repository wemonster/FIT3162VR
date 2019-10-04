using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A;
public class Arc : MonoBehaviour
{
    private ArcData arc;
    private bool dirty;
    private LineRenderer lr;
    private readonly int SEGMENT_COUNT = 100;

    private static readonly float MaxHeight = 0.7f;

    public static float MaxVolume = 0.5f;
    private float PrevMaxHeightQuotient = 0f;

    private bool flag = false;

    public void Init()
    {
        if (!lr)
        {
            lr = GetComponent<LineRenderer>();
            dirty = false;
        }
    }

    public void SetArc(ArcData arc)
    {
        this.arc = arc;
    }

    public ArcData GetArc()
    {
        return arc;
    }

    public void Dirty()
    {
        dirty = !flag;
        //dirty = true;
        UpdateDrawing();
    }

    private void Draw(float height_multiplier)
    {
        var origin = arc.startpos;
        var dest = arc.endpos;
        //Vector3 test = 0.5f * (dest + origin);
        //Debug.Log(test.magnitude);  
        Vector3 apex = dest + origin;
        //Debug.Log(height_multiplier);
        apex.Normalize();
        apex *= height_multiplier;
        if (!lr)
        {
            Debug.Log("OH NO");
        }
        lr.positionCount = SEGMENT_COUNT + 1;
        for (int i = 0; i <= SEGMENT_COUNT; i++)
        {
            float t = i / (float)SEGMENT_COUNT;
            Vector3 pixel = GetPoint(origin, apex, dest, t);
            lr.SetPosition(i, pixel);
        }
        flag = true;
    }

    //private void Update()
    //{
       
    //}

    private void UpdateDrawing()
    {
        float NewMaxHeightQuotient = MaxVolume * transform.parent.localScale.x;
        if (PrevMaxHeightQuotient != NewMaxHeightQuotient)
        {
            PrevMaxHeightQuotient = NewMaxHeightQuotient;
            dirty = true;
            //if (flag)
            //{
            //    Debug.Log("Changed MaxVolume!");
            //}
            //flag = true;
        }
        if (dirty)
        {
            float height_multiplier = 0.55f + (arc.volume * MaxHeight) / (NewMaxHeightQuotient);
            //float height_multiplier = 1f + (float)System.Math.Log(arc.volume);
            Draw(height_multiplier);
            dirty = false;
        }  
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
