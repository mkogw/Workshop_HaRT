using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recenter : MonoBehaviour
{

    [SerializeField] List<SkinnedMeshRenderer> meshlist;

    private void Start()
    {
        foreach (SkinnedMeshRenderer _mesh in meshlist)
        {
            _mesh.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OVRManager.display.RecenterPose();
            foreach (SkinnedMeshRenderer _mesh in meshlist)
            {
                _mesh.enabled = true;
            }
        }
    }
}