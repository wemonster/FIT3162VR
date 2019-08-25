using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using F;
public class showLocationInfo : MonoBehaviour
{
    // Start is called before the first frame update
    private Text SLIC;
    private Text CC;
    private Text SORT;
    private Text CAP;
    private Text SPAN;
    private Text START;
    private Text STATE;
    void Start()
    {
        int children = transform.childCount;
        string[] attributes = { "SLIC", "CC", "SORT", "CAP", "SPAN", "START" };
        SLIC = transform.Find("SLIC").GetComponent<Text>();
        //SLIC.transform.position = new Vector3(0, 0, 0);

        CC = transform.Find("CC").GetComponent<Text>();

        SORT = transform.Find("SORT").GetComponent<Text>();

        CAP = transform.Find("CAP").GetComponent<Text>();

        SPAN = transform.Find("SPAN").GetComponent<Text>();

        START = transform.Find("START").GetComponent<Text>();

        STATE = transform.Find("STATE").GetComponent<Text>();

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.GetComponent<FacInfo>())
                {
                    Facility fac = obj.GetComponent<FacInfo>().GetFac();
                    SLIC.text = fac.SLIC;
                    
                    CC.text = fac.CC;
                    CAP.text = fac.CAP;
                    //Debug.Log(fac.CC);
                    SORT.text = fac.SORT;
                    SPAN.text = fac.SPAN;
                    START.text = fac.START;
                    STATE.text = fac.STATE;
                }
                

                //SLIC.text = hit.collider.gameObject.name;
            }
            wait(1);
        }
    }

    public void updateLoc(string SLIC,string CC,string SORT,string CAP,string SPAN,string START)
    {
        this.SLIC.text = SLIC;
        this.CC.text = CC;
        this.SORT.text = SORT;
        this.CAP.text = CAP;
        this.SPAN.text = SPAN;
        this.START.text = START;
}

    private IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
