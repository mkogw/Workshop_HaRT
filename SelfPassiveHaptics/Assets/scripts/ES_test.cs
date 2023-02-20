using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_test : MonoBehaviour
{
    private GameObject Object_ES_control;
    private ES_control ES_control;
    // Start is called before the first frame update
    void Start()
    {
        Object_ES_control = GameObject.Find("ES_control");
        ES_control = Object_ES_control.GetComponent<ES_control>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            ES_control.StartElectricalStimulation();
        }
        if (Input.GetKeyDown("q"))
        {
            ES_control.StopElectricalStimulation();
        }
    }
}
