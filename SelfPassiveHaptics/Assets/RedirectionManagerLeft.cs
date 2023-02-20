using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectionManagerLeft : MonoBehaviour
{
    /// <summary>
    /// </summary>
    public GameObject realHand;
    /// <summary>
    /// The game object of the virtual hand
    /// </summary>
    public GameObject virtualHand;
    /// <summary>
    /// The warp origin that is used by all redirection techniques. If it is set to NONE, it will
    /// be set to the hand's real position on the start of each redirection
    /// </summary>
    public GameObject warpOrigin;
    /// <summary>

    /// <summary>
    /// The Redirection Technique in the Redirection Manager serves as the default Redirection
    /// Technique, which is used by all Redirected Prefabs, that do not specify another technique 
    /// </summary>
    public GameObject redirectionTechnique;
    /// <summary>

    private HR_Toolkit.Zenner_BodyWarping Zenner_BodyWarping;
    private HR_Toolkit.RedirectionObject dummyObject;
    private Transform dummyTransform;

    // Start is called before the first frame update
    void Start()
    {
        Zenner_BodyWarping = redirectionTechnique.GetComponent<HR_Toolkit.Zenner_BodyWarping>();
    }

    // Update is called once per frame
    void Update()
    {
        Zenner_BodyWarping.ApplyRedirection(realHand.transform,virtualHand.transform,warpOrigin.transform,dummyObject,dummyTransform);
    }
}
