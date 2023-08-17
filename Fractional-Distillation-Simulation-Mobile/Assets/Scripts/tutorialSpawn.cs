using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tutorialSpawn : MonoBehaviour
{
    public Camera cam;
    
    public GameObject columnPrefab, rightTrayPrefab, leftTrayPrefab,
        feedPrefab, reboilerPrefab, condenserPrefab;
    private int trayNumber, feedPosition;

    public Vector3 ogPos, ogRot;
    private Vector3 feedPos, reboilerPos, condenserPos;
    private float ogZoom;

    private bool zoom = false;

    private Touch touch = new();
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        
        ogPos = transform.localPosition;
        ogZoom = cam.orthographicSize;
        ogRot = cam.transform.eulerAngles;

        trayNumber = 6;
        feedPosition = 3;

        GameObject reboilerInstance = (GameObject)Instantiate(reboilerPrefab, gameObject.transform);
        reboilerPos = new Vector3(0, 467, 0);
        reboilerInstance.transform.localPosition = reboilerPos;

        for (int i = 0; i < trayNumber; i++)
        {
            if (i % 2 == 0)
            {
                GameObject rightTrayInstance = (GameObject)Instantiate(rightTrayPrefab, gameObject.transform);
                rightTrayInstance.transform.localPosition = new Vector3(0, 467 + i * 120, 0);
            }
            else
            {
                GameObject leftTrayInstance = (GameObject)Instantiate(leftTrayPrefab, gameObject.transform);
                leftTrayInstance.transform.localPosition = new Vector3(0, 467 + i * 120, 0);
            }
            if (i == trayNumber - feedPosition - 1)
            {
                GameObject feedInstance = (GameObject)Instantiate(feedPrefab, gameObject.transform);
                feedPos = new Vector3(0, 467 + i * 100 + (i + 1) * 20, 0);
                feedInstance.transform.localPosition = feedPos;
            }
            else if (i < trayNumber - 1)
            {
                GameObject columnInstance = (GameObject)Instantiate(columnPrefab, gameObject.transform);
                columnInstance.transform.localPosition = new Vector3(0, 467 + i * 100 + (i + 1) * 20, 0);
            }
        }
        GameObject condenserInstance = (GameObject)Instantiate(condenserPrefab, gameObject.transform);
        condenserPos = new Vector3(0, 487 + (trayNumber - 1) * 120, 0);
        condenserInstance.transform.localPosition = condenserPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
