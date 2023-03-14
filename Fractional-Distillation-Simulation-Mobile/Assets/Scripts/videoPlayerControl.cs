using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class videoPlayerControl : MonoBehaviour
{
    public GameObject pauseButton, playButton;
    public Slider videoSlider;

    private VideoPlayer video;
    private GameObject tutorialCanvas;
    private bool tracking = true;
    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoPlayer>();
        video.targetCamera = Camera.main;
    }

    void Update()
    {
        if (tracking)
        {
            videoSlider.value = (float)video.frame / (float)video.frameCount;
        }
    }

    public void scrubbingActivate() 
    {
        tracking = false;
    }

    public void scrubbingDeactivate()
    {
        float frame = (float) videoSlider.value * (float)video.frameCount;
        video.frame = (long)frame;
        tracking = true;
    }

    public void activateVideoPlayer()
    {
        gameObject.SetActive(true);
        tutorialCanvas = GameObject.FindWithTag("tutorialCanvas");
        tutorialCanvas.SetActive(false);
    }

    public void deactiviateVideoPlayer()
    {
        tutorialCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void pauseVid()
    {
        playButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void playVid()
    {
        pauseButton.SetActive(true);
        playButton.SetActive(false);
    }
}
