using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feedAnimation : MonoBehaviour
{
    public GameObject liquidDropletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //spawn liquid droplet
        StartCoroutine(FeedLiquidFlow());
    }

    private IEnumerator FeedLiquidFlow()
    {
        while (true)
        {
            //feed column is 375 wide
            GameObject feedLiquid = (GameObject)Instantiate(liquidDropletPrefab);
            feedLiquid.transform.position = transform.position + new Vector3(-200f, -10, 180f);
            feedLiquid.GetComponent<setColour>().liqCon = 0.5f;
            yield return new WaitForSeconds(0.1f);
        }

    }

}
