using UnityEngine;
using System.Collections;

public class DataLoader : MonoBehaviour
{
    public DataVisualizer Visualizer;
    private SeriesLocArray state;
    private SeriesLocArray fac;
    private SeriesLocArray arc;
    // Use this for initialization
    void Start()
    {

        TextAsset stateData = Resources.Load<TextAsset>("fullstate");
        string stateJson = stateData.text;
        state = JsonUtility.FromJson<SeriesLocArray>(stateJson);
        Visualizer.initialiseState(state.AllData);

        TextAsset facData = Resources.Load<TextAsset>("fac_state");
        string facJson = facData.text;
        fac = JsonUtility.FromJson<SeriesLocArray>(facJson);
        Visualizer.CreateMeshes(fac.AllData);

        TextAsset arcData = Resources.Load<TextAsset>("vol_data");
        string arcJson = arcData.text;
        arc = JsonUtility.FromJson<SeriesLocArray>(arcJson);
        Visualizer.createArcs(arc.AllData);


    }

    void Update()
    {

    }

    public void Initialise()
    {
        Visualizer.initialise();
        //Visualizer.CreateMeshes(fac.AllData);
        //Visualizer.createArcs(arc.AllData);
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