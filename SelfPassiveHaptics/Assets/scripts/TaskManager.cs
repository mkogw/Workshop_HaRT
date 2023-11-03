using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    public GameObject redirectionTechnique_rightHand;
    private HR_Toolkit.Zenner_BodyWarping Zenner_BodyWarping;

    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject WarpOrigin;
    private collisionDetector reset_CD;

    private int currentMode = 1;
    private bool flag_change = false;

    //public float alpha;

    // Start is called before the first frame update
    void Start()
    {
        Zenner_BodyWarping = redirectionTechnique_rightHand.GetComponent<HR_Toolkit.Zenner_BodyWarping>();
        reset_CD = WarpOrigin.GetComponent<collisionDetector>();

        //Zenner_BodyWarping.redirectionAngleAlpha = alpha;
        
        Debug.Log("Start!");
    }

    // Update is called once per frame
    void Update()
    {
        if(reset_CD.CollisionDetected && !flag_change){
            flag_change = true;
            changeMode();
        }
        switch(currentMode){
            case 1:
                if(target1.GetComponent<collisionDetector>().CollisionDetected){
                    flag_change = false;
                }
                break;
            case 2:
                if(target2.GetComponent<collisionDetector>().CollisionDetected){
                    flag_change = false;
                }
                break;
            case 3:
                if(target3.GetComponent<collisionDetector>().CollisionDetected){
                    flag_change = false;
                }
                break;
            default:
                break;
        }
 
    }

    void changeMode()
    {
       switch(currentMode){
            case 3:
                Zenner_BodyWarping.redirectionAngleAlpha = 0;
                target1.GetComponent<Renderer>().material.color = Color.yellow;
                currentMode = 1;
                break;
            case 1:
                Zenner_BodyWarping.redirectionAngleAlpha = 30;
                target2.GetComponent<Renderer>().material.color = Color.yellow;
                currentMode = 2;
                break;
            case 2:
                Zenner_BodyWarping.redirectionAngleAlpha = -30;
                target3.GetComponent<Renderer>().material.color = Color.yellow;
                currentMode = 3;
                break;
            default:
                break;
        }
    }


}
