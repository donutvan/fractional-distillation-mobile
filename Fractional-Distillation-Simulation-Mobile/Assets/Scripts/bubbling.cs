using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbling : MonoBehaviour
{
    public GameObject bubblePrefab;
    public float bubbleDelay = 0.1f, radius = 150f;
    public bool startBubble = true;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 objectPos;
        objectPos = transform.position;
        StartCoroutine(Bubbling(objectPos.y));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Bubbling(float trayHeight)
    {
        while (startBubble)
        {
            GameObject bubbleInstance = (GameObject)Instantiate(bubblePrefab);
            bubbleInstance.transform.position = new Vector3(Random.Range(-radius, radius), trayHeight + 6, Random.Range(-radius, radius));

            yield return new WaitForSeconds(bubbleDelay);
        }
    }
}
