using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuUI : MonoBehaviour
{

    // Update is called once per frame
    public void toSliderOptionScreen()
    {
        SceneManager.LoadScene("SliderOptionScene");
    }

    public void toTutorialScreen()
    {
        SceneManager.LoadScene("ARTutorialScene");
    }
}
