using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rising_wave : MonoBehaviour
{
    // Start is called before the first frame update
    public float yIncrease = 1f;
    private float totalTime;
    //GameObject mc;
    // Start is called before the first frame update
    void Start()
    {
        //mc = GameObject.FindWithTag("MainCamera");
        totalTime = 1;
        StartCoroutine(LiquidRise(totalTime));
    }

    private IEnumerator LiquidRise(float totalTime)
    {
        Vector3 objectPos;
        float period = totalTime / (15 / yIncrease);
        float totalIncrease = 0;
        objectPos = transform.position;
        while (totalIncrease < 15)
        {
            //Debug.Log("yScaling: " + objectScale.y);
            //Debug.Log("yIncrease: " + yIncrease);
            objectPos.y += yIncrease;
            transform.position = objectPos;
            totalIncrease += yIncrease;
            yield return new WaitForSeconds(period);
        }
    }
}