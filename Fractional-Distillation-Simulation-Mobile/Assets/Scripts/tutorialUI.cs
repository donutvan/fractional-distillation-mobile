using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialUI : MonoBehaviour
{
    private Camera cam;
    private Vector3 ogPos, ogRot;
    private float ogZoom;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        ogPos = cam.transform.position;
        ogZoom = cam.orthographicSize;
        ogRot = cam.transform.eulerAngles;
    }

    public void backButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void resetViewPoint()
    {
        cam.transform.position = ogPos;
        cam.transform.eulerAngles = ogRot;
        cam.orthographicSize = ogZoom;
    }
}
