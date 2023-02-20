using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    public GameObject redirectionTechnique_rightHand;
    private HR_Toolkit.Zenner_BodyWarping Zenner_BodyWarping;

    public GameObject collisionTarget;
    private collisionDetector collisionDetector;

    //public float alpha;

    // Start is called before the first frame update
    void Start()
    {

        Zenner_BodyWarping = redirectionTechnique_rightHand.GetComponent<HR_Toolkit.Zenner_BodyWarping>();
        collisionDetector = collisionTarget.GetComponent<collisionDetector>();

        //Zenner_BodyWarping.redirectionAngleAlpha = alpha;
        
        Debug.Log("Start!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            Zenner_BodyWarping.redirectionAngleAlpha = Random.Range(-1, 1) * 30.0f ;
            Debug.Log("current alpha = " + (Zenner_BodyWarping.redirectionAngleAlpha));
        }

        if(collisionDetector.CollisionDetected){
            Invoke("GotoNextScene", 1f);
        }
    }

    void GotoNextScene()
    {
        //SceneManager.LoadScene(ExperimentData.EvaluationScene);
    }
}
