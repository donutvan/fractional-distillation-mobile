using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spawnAnimationValve : MonoBehaviour
{
    public GameObject feedLiquid1Prefab, feedLiquid2Prefab, waterSurfacePrefab, leftTrayLiquid, rightTrayLiquid, 
        reboilerLiquid1Prefab, reboilerLiquid2Prefab,
         condenserLiquid1Prefab, condenserLiquid2Prefab;
    public float SCALE = 1f;
    public int trayNumber, feedPosition, fPos;

    public bool validSolution = true;
    private bool reboilerAnimationOk = false, condenserAnimationOk = false,
        feedAnimationStarted = false, reboilerAnimationStarted = false, condenserAnimationStarted = false;

    private Touch touch = new Touch();

    public string feedValve, reboilerValve, condenserValve;
    public Material columnMat, glowingMat;
    private GameObject[] valves;

    public Camera cam;
    public AudioClip[] valveSounds = new AudioClip[3];
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        trayNumber = SliderOptionsMenu.trayNumberValue;
        feedPosition = SliderOptionsMenu.feedPositionValue; 

        if (trayNumber < 6) { trayNumber = 6; }
        else if (trayNumber > 20) { trayNumber = 20; }

        if (feedPosition >= trayNumber) { feedPosition = trayNumber - 1; }
        else if (feedPosition < 1) { feedPosition = 1; }

        fPos = trayNumber - feedPosition;
        Debug.Log("trayNumber = " + trayNumber);
        Debug.Log("feedPosition = " + feedPosition);

        if (validSolution)
        {
            GameObject feedLiquidInstance = (GameObject)Instantiate(feedLiquid1Prefab, transform);
            feedLiquidInstance.transform.position = new Vector3(0, 467 + (fPos - 1) * 100 + fPos * 20, 0);
        }
        //startFeedAnimationFxn();
        //startReboilerAnimationFxn();
        //startCondenserAnimationFxn();
    }

    
    void FixedUpdate()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 pos = touch.position;
                RaycastHit hit;
                Vector3 camPos = cam.ScreenToWorldPoint(pos);
                Vector3 fwd = cam.transform.TransformDirection(Vector3.forward);
                if (Physics.Raycast(camPos, fwd, out hit) && hit.collider.tag == feedValve) 
                {
                    StartCoroutine(rotateValve(hit.transform));
                    if (!feedAnimationStarted)
                    {
                        startFeedAnimationFxn();
                        hit.collider.GetComponent<Renderer>().material = columnMat;
                        hit.collider.GetComponent<AudioSource>().clip = valveSounds[Random.Range(0, 3)];
                        hit.collider.GetComponent<AudioSource>().Play();
                    } 
                }
                else if (Physics.Raycast(camPos, fwd, out hit) && hit.collider.tag == reboilerValve)
                {
                    StartCoroutine(rotateValve(hit.transform));
                    if (reboilerAnimationOk && !reboilerAnimationStarted) 
                    {
                        startReboilerAnimationFxn();
                        hit.collider.GetComponent<Renderer>().material = columnMat;
                        hit.collider.GetComponent<AudioSource>().clip = valveSounds[Random.Range(0, 3)];
                        hit.collider.GetComponent<AudioSource>().Play();
                    }
                }
                else if (Physics.Raycast(camPos, fwd, out hit) && hit.collider.tag == condenserValve)
                {
                    StartCoroutine(rotateValve(hit.transform));
                    if (condenserAnimationOk && !condenserAnimationStarted)
                    {
                        startCondenserAnimationFxn();
                        hit.collider.GetComponent<Renderer>().material = columnMat;
                        hit.collider.GetComponent<AudioSource>().clip = valveSounds[Random.Range(0, 3)];
                        hit.collider.GetComponent<AudioSource>().Play();
                    }
                }
            }
        }
    }
    
    public IEnumerator rotateValve (Transform transform) 
    {
        float period = 1/90;
        float deg = 90;
        while (deg >= 0)
        {
            transform.Rotate(0f, 0f, 1f, Space.World);
            deg -= 1;
            yield return new WaitForSeconds(period);
        }
    }

    public void startFeedAnimationFxn()
    {
        if (validSolution)
        {
            StartCoroutine(startFeedAnimation());
        }
    }

    public void startReboilerAnimationFxn()
    {
        if (validSolution)
        {
            StartCoroutine(startReboilerAnimation());
        }
    }

    public void startCondenserAnimationFxn()
    {
        if (validSolution)
        {
            StartCoroutine(startCondenserAnimation());
        }
    }

    public IEnumerator startFeedAnimation() 
    {
        feedAnimationStarted = true;
        yield return new WaitForSeconds(0.01f);
        //spawn feed liquid
        spawnFeedLiquid(fPos);
        yield return new WaitForSeconds(6.5f);

        //spawn tray liquid going down
        for (int i = fPos; i > 0; i--)
        {
            Debug.Log("i = " + i);
            if (i % 2 == 1) { spawnTrayLiquidLeft(true, i, trayNumber); }
            else { spawnTrayLiquidLeft(false, i, trayNumber); }
            yield return new WaitForSeconds(1.2f);
        }

        spawnReboilerLiquid1();
        yield return new WaitForSeconds(1.2f);
        //Debug.Log("Valve 2 ready to open! Time elapsed: " + timeElapsed);
        reboilerAnimationOk = true;
        valves = GameObject.FindGameObjectsWithTag(reboilerValve);
        valves[0].GetComponent<Renderer>().material = glowingMat;
    }

    public IEnumerator startReboilerAnimation()
    {
        reboilerAnimationStarted = true;
        //spawn last level and reboiler liquid
        spawnReboilerLiquid2(trayNumber);
        yield return new WaitForSeconds(24.2f + (trayNumber - 6) * 0.5f);
        //Debug.Log("Valve 2 ready to open! Time elapsed: " + timeElapsed);
        spawnCondenserLiquid1(trayNumber);
        yield return new WaitForSeconds(14.7f);
        condenserAnimationOk = true;
        valves = GameObject.FindGameObjectsWithTag(condenserValve);
        valves[0].GetComponent<Renderer>().material = glowingMat;
    }

    public IEnumerator startCondenserAnimation()
    {
        condenserAnimationStarted = true;
        //spawn condenser liquid
        spawnCondenserLiquid2(trayNumber);
        yield return new WaitForSeconds(8.3f);

        //spawn rest of tray liquid going down
        for (int i = trayNumber; i > fPos; i--)
        {
            if (i % 2 == 1) { spawnTrayLiquidLeft(true, i, trayNumber); }
            else { spawnTrayLiquidLeft(false, i, trayNumber); }
            yield return new WaitForSeconds(1.2f);
        }
    }
    
    //spawn feed liquid prefab
    private void spawnFeedLiquid(int fPos)
    {
        GameObject feedLiquidInstance = (GameObject)Instantiate(feedLiquid2Prefab, transform);
        feedLiquidInstance.transform.position = new Vector3(0, 467 + (fPos - 1) * 100 + fPos * 20, 0) * SCALE;
        feedLiquidInstance.GetComponent<feedFlowRate>().feedRate = GetComponent<calculationNew>().feedR;
    }

    //spawn tray liquid prefab
    private void spawnTrayLiquidLeft(bool isLeft, int trayLevel, int trayNumber)
    {
        //trayLevel counts from the bottom
        if (isLeft)
        {
            GameObject liquidColumnInstance = (GameObject)Instantiate(leftTrayLiquid, transform);
            liquidColumnInstance.transform.position = new Vector3(0, (367 + trayLevel * 120) * SCALE, 0);
            liquidColumnInstance.GetComponent<ethanolConDisplay>().endCon =
                GetComponent<calculationNew>().XvariableList[trayNumber - trayLevel];
        }
        else
        {
            GameObject liquidColumnInstance = (GameObject)Instantiate(rightTrayLiquid, transform);
            liquidColumnInstance.transform.position = new Vector3(0, (367 + trayLevel * 120) * SCALE, 0);
            liquidColumnInstance.GetComponent<ethanolConDisplay>().endCon =
                GetComponent<calculationNew>().XvariableList[trayNumber - trayLevel];
        }
    }

    private void spawnReboilerLiquid1()
    {
        GameObject reboilerLiquidInstance = (GameObject)Instantiate(reboilerLiquid1Prefab, transform);
        reboilerLiquidInstance.transform.position = new Vector3(0, 367 * SCALE, 0);
        reboilerLiquidInstance.GetComponent<reboilerConcentration>().liqCon = GetComponent<calculationNew>().successXb;
    }

    private void spawnReboilerLiquid2(int trayNumber)
    {
        GameObject reboilerLiquidInstance = (GameObject)Instantiate(reboilerLiquid2Prefab, transform);
        reboilerLiquidInstance.transform.position = new Vector3(0, 367 * SCALE, 0);
        reboilerLiquidInstance.GetComponent<reboiler_animation>().trayNumber = trayNumber;
        reboilerLiquidInstance.GetComponent<reboiler_animation>().liqCon = GetComponent<calculationNew>().successXb;
        reboilerLiquidInstance.GetComponent<reboilerText>().flowRate = GetComponent<calculationNew>().Fb;
        reboilerLiquidInstance.GetComponent<reboilerText>().liqCon = GetComponent<calculationNew>().successXb;
    }

    private void spawnCondenserLiquid1(int trayNumber)
    {
        GameObject condenserLiquidInstance = (GameObject)Instantiate(condenserLiquid1Prefab, transform);
        condenserLiquidInstance.transform.position = new Vector3(0, (487 + (trayNumber - 1) * 120) * SCALE, 0);
        condenserLiquidInstance.GetComponent<condenser_animation1>().liqCon = GetComponent<calculationNew>().successXd;
    }

    private void spawnCondenserLiquid2(int trayNumber)
    {
        GameObject condenserLiquidInstance = (GameObject)Instantiate(condenserLiquid2Prefab, transform);
        condenserLiquidInstance.transform.position = new Vector3(0, (487 + (trayNumber - 1) * 120) * SCALE, 0);
        condenserLiquidInstance.GetComponent<condenserText>().flowRate = GetComponent<calculationNew>().Fd;
        condenserLiquidInstance.GetComponent<condenserText>().liqCon = GetComponent<calculationNew>().successXd;
        condenserLiquidInstance.GetComponent<condenser_animation2>().liqCon = GetComponent<calculationNew>().successXd;
    }


}
