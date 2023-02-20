using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    public GameObject redirectionTechnique;
    private HR_Toolkit.Zenner_BodyWarping Zenner_BodyWarping;

    public GameObject VirtualObject;
    private collisionDetector collisionDetector;

    private GameObject Object_ES_control;
    private ES_control ES_control;

    // Start is called before the first frame update
    void Start()
    {
        Object_ES_control = GameObject.Find("ES_control");

        Zenner_BodyWarping = redirectionTechnique.GetComponent<HR_Toolkit.Zenner_BodyWarping>();
        collisionDetector = VirtualObject.GetComponent<collisionDetector>();
        ES_control = Object_ES_control.GetComponent<ES_control>();

        if(ExperimentData.useElectricalStimulation){
            ES_control.StartElectricalStimulation();
        }
        
        if(ExperimentData.trialNumLeft>0){
            int idx = UnityEngine.Random.Range(0,ExperimentData.trialNumLeft);
            Zenner_BodyWarping.redirectionAngleAlpha = ExperimentData.alphaList[idx];
            ExperimentData.currentAlpha = ExperimentData.alphaList[idx];
            ExperimentData.trialNumLeft -= 1;
            ExperimentData.alphaList.RemoveAt(idx);
        }
        Debug.Log((ExperimentData.trialNumAll-ExperimentData.trialNumLeft)+"/"+(ExperimentData.trialNumAll)+"  alpha="+(ExperimentData.currentAlpha));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            SceneManager.LoadScene(ExperimentData.EvaluationScene);
        }

        if(collisionDetector.CollisionDetected){
            Invoke("GotoNextScene", 1f);
        }
    }

    void GotoNextScene()
    {
        ES_control.StopElectricalStimulation();
        SceneManager.LoadScene(ExperimentData.EvaluationScene);
    }
}
