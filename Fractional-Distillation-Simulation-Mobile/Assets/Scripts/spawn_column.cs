using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_column : MonoBehaviour
{
    public GameObject columnPrefab, rightTrayPrefab, leftTrayPrefab,
        feedPrefab, reboilerPrefab, condenserPrefab;
    public int trayNumber, feedPosition;
    public float condenserYPos;

    //public static Vector3Int worldViewBottomCoord = new Vector3Int(0, 0, 0), worldViewTopCoord;
    private int i;
    void Start()
    {
        trayNumber = SliderOptionsMenu.trayNumberValue;
        feedPosition = SliderOptionsMenu.feedPositionValue;

        if (trayNumber < 6) { trayNumber = 6; }
        else if (trayNumber > 20) { trayNumber = 20; }

        if (feedPosition >= trayNumber) { feedPosition = trayNumber - 1; }
        else if (feedPosition < 1) { feedPosition = 1; }

        GameObject reboilerInstance = (GameObject)Instantiate(reboilerPrefab);
        reboilerInstance.transform.position = new Vector3(0,467,0);
        /*
        worldViewTopCoord = worldViewBottomCoord;
        worldViewTopCoord.y += 487 + (trayNumber - 1) * 120 + 757;
        */


        for (i = 0; i < trayNumber; i++) {
            if (i % 2 == 0) {
                GameObject rightTrayInstance = (GameObject)Instantiate(rightTrayPrefab);
                rightTrayInstance.transform.position = new Vector3(0, 467 + i * 120, 0);
            }
            else
            {
                GameObject leftTrayInstance = (GameObject)Instantiate(leftTrayPrefab);
                leftTrayInstance.transform.position = new Vector3(0, 467 + i * 120, 0);
            }
            if (i == trayNumber - feedPosition - 1) {
                GameObject feedInstance = (GameObject)Instantiate(feedPrefab);
                feedInstance.transform.position = new Vector3(0, 467 + i * 100 + (i+1) * 20, 0);
            }
            else if (i < trayNumber - 1)
            {
                GameObject columnInstance = (GameObject)Instantiate(columnPrefab);
                columnInstance.transform.position = new Vector3(0, 467 + i * 100 + (i + 1) * 20, 0);
            }
        }
        GameObject condenserInstance = (GameObject)Instantiate(condenserPrefab);
        condenserInstance.transform.position = new Vector3(0, 487 + (trayNumber - 1) * 120, 0);
        float heightofCol = condenserInstance.transform.position.y + (756.24f - (467f - 436f));
        condenserYPos = condenserInstance.transform.position.y;
    }

    
}
