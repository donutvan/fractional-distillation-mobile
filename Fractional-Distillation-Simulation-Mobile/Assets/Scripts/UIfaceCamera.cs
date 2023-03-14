using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIfaceCamera : MonoBehaviour
{
    public Camera cameraToLookAt;

    void Start()
    {
        cameraToLookAt = Camera.main;
        transform.LookAt(cameraToLookAt.transform.position);
        transform.Rotate(0, 180, 0);
    }

    void Update()
    {
        Vector3 v = cameraToLookAt.transform.position - transform.position;

        transform.LookAt(cameraToLookAt.transform.position - (v - Vector3.Project(v, -cameraToLookAt.transform.forward)));
        transform.Rotate(0, 180, 0);
    }
}
