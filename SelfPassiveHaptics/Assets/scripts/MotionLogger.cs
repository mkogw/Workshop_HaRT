using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using System.IO;
using System;

public class MotionLogger : MonoBehaviour
{
    public GameObject target_real;
    private Transform tf_target_real;

    public GameObject target_virtual;
    private Transform tf_target_virtual;

    private long time_log;

    // Start is called before the first frame update
    void Start()
    {
        tf_target_real = target_real.GetComponent<Transform>();
        tf_target_virtual = target_virtual.GetComponent<Transform>();

        
        //var path = Path.Combine(folder, "experiment_data/" + ExperimentData.fileName + "_" + (ExperimentData.timeStamp) + ".csv");

        System.DateTime centuryBegin = new System.DateTime(2001,1,1);
        time_log = System.DateTime.Now.Ticks - centuryBegin.Ticks;
    }

    // Update is called once per frame
    void Update()
    {
        Record();
    }

    void Record()
    {
        var folder = Application.dataPath;
        var path = Path.Combine(folder, "Data/"+ExperimentData.fileName+"/motion/"  + ExperimentData.fileName + "_" +(time_log) +"_"+ ExperimentData.axis+(ExperimentData.currentAlpha) +(ExperimentData.useElectricalStimulation) +".csv");
        using (var writer = new StreamWriter(path, true))
        {
            writer.Write(Time.time + ", " + WritePosRot(tf_target_real)+","+WritePosRot(tf_target_virtual)+"\n");
        }
        //Debug.Log("Recorded");
    }

    String WritePosRot(Transform trans)
    {
        return WriteVector3(trans.position) + ", " + WriteVector3(trans.eulerAngles) + ", " + WriteVector3(trans.localPosition) + ", " + WriteVector3(trans.localEulerAngles);
    }

    String WriteVector3(Vector3 vec3)
    {
        return vec3.x + ", " + vec3.y + ", " + vec3.z;
    }
}
