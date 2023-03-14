using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onlyZRotation : MonoBehaviour
{
    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
    }
}
