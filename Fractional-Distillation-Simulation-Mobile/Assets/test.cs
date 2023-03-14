using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject sphere;
    public Camera cam;
    private Vector2 bottomLeft, topRight;
    void Start()
    {
        bottomLeft = new Vector2(0, 0);
        Debug.Log("screen bottom left: "+bottomLeft);
        topRight = new Vector2(cam.pixelWidth, cam.pixelHeight);
        Debug.Log("screen top right: " + topRight);

        GameObject sphereinst = (GameObject)Instantiate(sphere);
        sphereinst.transform.position = cam.ScreenToWorldPoint(new Vector3(bottomLeft.x, bottomLeft.y, cam.transform.position.z));
        Debug.Log("bottom left sphere: " + sphereinst.transform.position);
        GameObject sphereinst2 = (GameObject)Instantiate(sphere);
        sphereinst2.transform.position = cam.ScreenToWorldPoint(new Vector3(topRight.x, topRight.y, cam.transform.position.z));
        Debug.Log("top right sphere: " + sphereinst2.transform.position);

    }

   
}
