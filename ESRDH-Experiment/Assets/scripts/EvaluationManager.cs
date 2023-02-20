using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;

public class EvaluationManager : MonoBehaviour
{
    public GameObject arrow_yes;
    private collisionDetector collision_yes;
    public GameObject arrow_no;
    private collisionDetector collision_no;

    public GameObject redirectionTechnique;
    private HR_Toolkit.Zenner_BodyWarping Zenner_BodyWarping;

    private bool answered = false;

    // Start is called before the first frame update
    void Start()
    {
        Zenner_BodyWarping = redirectionTechnique.GetComponent<HR_Toolkit.Zenner_BodyWarping>();

        collision_yes = arrow_yes.GetComponent<collisionDetector>();
        collision_no = arrow_no.GetComponent<collisionDetector>();

        Zenner_BodyWarping.redirectionAngleAlpha = ExperimentData.currentAlpha;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("n"))
        {
            SceneManager.LoadScene(ExperimentData.ExperimentScene);
        }

        if(collision_yes.CollisionDetected && !answered){
            answered = true;
            Record("y");
            Invoke("GotoNextScene", 1f);
        }

        if(collision_no.CollisionDetected && !answered){
            answered = true;
            Record("n");
            Invoke("GotoNextScene", 1f);
        }
    }

    void Record(string s)
    {
        var folder = Application.dataPath;
        //var path = Path.Combine(folder, "experiment_data/" + ExperimentData.fileName + "_" + (ExperimentData.timeStamp) + ".csv");
        var path = Path.Combine(folder, "Data/"+ExperimentData.fileName+"/main_"  + ExperimentData.fileName + "_" + (ExperimentData.timeStamp) + ".csv");
        using (var writer = new StreamWriter(path, true))
        {
            writer.Write($"{ExperimentData.axis},{ExperimentData.useElectricalStimulation},{ExperimentData.currentAlpha},{s}\n");
        }
        Debug.Log(s + " Recorded");
    }

    void GotoNextScene()
    {
        if(ExperimentData.trialNumLeft==0){
            Debug.Log("Finish");
            SceneManager.LoadScene(ExperimentData.EndScene);
        }else{
            SceneManager.LoadScene(ExperimentData.ExperimentScene);
        }
        
    }
}
