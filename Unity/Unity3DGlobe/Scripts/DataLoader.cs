using UnityEngine;
using System.Collections;

public class DataLoader : MonoBehaviour {
    public DataVisualizer Visualizer;
    private SeriesLocArray fac;
    private SeriesArray arc;
	// Use this for initialization
	void Start () {
        TextAsset facData = Resources.Load<TextAsset>("fac_state");
        string facJson = facData.text;
        
        fac = JsonUtility.FromJson<SeriesLocArray>(facJson);
        Visualizer.CreateMeshes(fac.AllData);

        TextAsset arcData = Resources.Load<TextAsset>("vol_data");
        string arcJson = arcData.text;
        arc = JsonUtility.FromJson<SeriesArray>(arcJson);
        Visualizer.createArcs(arc.AllData);


    }

    void Update () {
	
	}

    public void Initialise()
    {
        Visualizer.CreateMeshes(fac.AllData);
        Visualizer.createArcs(arc.AllData);
    }
}

[System.Serializable]
public class SeriesLocArray
{
    public SeriesLocData[] AllData;
}
[System.Serializable]
public class SeriesArray
{
    public SeriesData[] AllData;
    
}