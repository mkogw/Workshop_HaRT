using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneLocationDetector : MonoBehaviour
{
    public GameObject target;
    private Transform tf_target;
    private OVRSkeleton skeleton;

    public OVRSkeleton.BoneId boneId = OVRSkeleton.BoneId.Hand_IndexTip;

    //public float x = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        skeleton = GetComponent<OVRSkeleton>();
        tf_target = target.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(OVRBone bone in skeleton.Bones){
            if(bone.Id == boneId){
                tf_target.position = bone.Transform.position;
                //x = bone.Transform.position.x; //debug
            }
        }

    }
}
