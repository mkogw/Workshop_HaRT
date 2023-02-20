using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ExperimentManager : MonoBehaviour
{
    //experiment settings
    public string fileName;
    public string axis; //yet to be used
    public bool isPractice;
    public bool useElectricalStimulation;

    //private string ExperimentScene;

    // Start is called before the first frame update
    void Start()
    {
        ExperimentData.fileName = fileName;
        ExperimentData.axis = axis;
        ExperimentData.useElectricalStimulation = useElectricalStimulation;

        System.DateTime centuryBegin = new System.DateTime(2001,1,1);
        ExperimentData.timeStamp = System.DateTime.Now.Ticks - centuryBegin.Ticks;

        //ExperimentData.alphaList = new List<float> {-14,-12,-10,-8,-6,-4,-2,0,2,4,6,8,10,12,14, -14,-12,-10,-8,-6,-4,-2,0,2,4,6,8,10,12,14};
        //ExperimentData.trialNumLeft = 30; //length

        ExperimentData.alphaList = new List<float> {
            -15,-12,-9,-6,-3,0,3,6,9,12,15,
            -15,-12,-9,-6,-3,0,3,6,9,12,15,
            -15,-12,-9,-6,-3,0,3,6,9,12,15,
            -15,-12,-9,-6,-3,0,3,6,9,12,15,
            -15,-12,-9,-6,-3,0,3,6,9,12,15,
            -15,-12,-9,-6,-3,0,3,6,9,12,15,
            -15,-12,-9,-6,-3,0,3,6,9,12,15,
            -15,-12,-9,-6,-3,0,3,6,9,12,15,
            -15,-12,-9,-6,-3,0,3,6,9,12,15,
            -15,-12,-9,-6,-3,0,3,6,9,12,15
        };
        ExperimentData.trialNumLeft = 110; //length
        ExperimentData.trialNumAll = 110;

        if(isPractice){
            ExperimentData.alphaList = new List<float> {-15,-3,3,15};
            ExperimentData.trialNumLeft = 4; //length
            ExperimentData.trialNumAll = 4;
        }
        
        ExperimentData.ExperimentScene = "Reaching";
        ExperimentData.EvaluationScene = "Evaluation";
        ExperimentData.EndScene = "End";


        //record setting
        if (!Directory.Exists(Application.dataPath + "/Data/" + ExperimentData.fileName))
        {
            Directory.CreateDirectory(Application.dataPath + "/Data/" + ExperimentData.fileName);
        }

        if (!Directory.Exists(Application.dataPath + "/Data/" + ExperimentData.fileName +"/motion"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Data/" + ExperimentData.fileName +"/motion");
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            SceneManager.LoadScene(ExperimentData.ExperimentScene);
        }
    }

}

public static class ExperimentData
{
    public static string fileName;
    public static string axis;
    public static bool useElectricalStimulation;

    public static string ExperimentScene;
    public static string EvaluationScene;
    public static string EndScene;

    public static List<float> alphaList;
    public static int trialNumLeft;
    public static int trialNumAll;

    public static float currentAlpha;

    public static long timeStamp;
}