using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RubberHandManager : MonoBehaviour
{
    public GameObject RubberHand;
    public GameObject RightHandAnchor;

    public GameObject Fixed;
    private Transform FixedTransform;
    private bool isFixed = false;


    // Start is called before the first frame update
    void Start()
    {
        FixedTransform = Fixed.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if(!isFixed){
                Fix();
                isFixed = true;
            }else{
                unFix();
                isFixed = false;
            }
        }

        if(isFixed)
        {
            RubberHand.GetComponent<Transform>().position = FixedTransform.position;
            RubberHand.GetComponent<Transform>().rotation = FixedTransform.rotation;
        }

        if (Input.GetKeyDown("n"))
        {
            SceneManager.LoadScene(ExperimentData.ExperimentScene);
        }        
    }

    void Fix()
    {
        RubberHand.GetComponent<OVRSkeleton>().enabled = false;
        FixedTransform.position = RubberHand.GetComponent<Transform>().position;
        FixedTransform.rotation = RubberHand.GetComponent<Transform>().rotation;
    }

    void unFix()
    {
        RubberHand.GetComponent<OVRSkeleton>().enabled = true;
    }
}
