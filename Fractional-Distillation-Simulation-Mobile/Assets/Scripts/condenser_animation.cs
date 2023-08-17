using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class condenser_animation : MonoBehaviour
{
    public GameObject vapourPrefab, liquidDropletPrefab, collectorLiquidPrefab, waterSurfacePrefab;

    public float liqCon;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startCondenserAnimation());
    }

    
    private IEnumerator startCondenserAnimation()
    {
        //spawn vapour in tube travelling to condenser
        StartCoroutine(preCondenserAnimation());
        yield return new WaitForSeconds(5f);

        //spawn vapour in condenser chamber
        StartCoroutine(vapourSpawnPoints(3));
        yield return new WaitForSeconds(4.2f);

        //spawn liquid molecules to collector
        StartCoroutine(preCollectorAnimation());
        yield return new WaitForSeconds(4.5f);

        StartCoroutine(collectorLiquidRise());
        yield return new WaitForSeconds(1f);

        StartCoroutine(postCollectorLiquidDown());
        yield return new WaitForSeconds(2.1f);

        StartCoroutine(postCollectorLiquidBackToColumn());
    }

    //spawn vapour from column to condenser
    private IEnumerator preCondenserAnimation()
    {
        while (true)
        {
            GameObject vapourInstance = (GameObject)Instantiate(vapourPrefab);
            vapourInstance.transform.position = transform.position + new Vector3(Random.Range(-8, 8), 
                160, Random.Range(-8, 8));
            vapourInstance.GetComponent<setColour>().liqCon = randomConcentrationGenerator(liqCon);
            yield return new WaitForSeconds(0.5f);
        }
    }

    //vapour spawn points
    private IEnumerator vapourSpawnPoints(int vapourSources)
    {
        int i;
        for (i = 0; i < vapourSources; i++)
        {
            StartCoroutine(condenserChamberAnimation());
            yield return null;
        }
    }

    //spawn vapour in condenser chamber
    private IEnumerator condenserChamberAnimation() 
    {
        int vapourCount = 0;
        while (vapourCount <= 50)
        {
            GameObject vapourInstance = (GameObject)Instantiate(vapourPrefab);
            vapourInstance.transform.position = transform.position + new Vector3(230, 628 + Random.
                Range(-8, 8), Random.Range(-8, 8));
            vapourInstance.GetComponent<setColour>().liqCon = randomConcentrationGenerator(liqCon);
            vapourCount++;
            yield return new WaitForSeconds(0.4f);
        }
    }

    //spawn liquid after condenser
    private IEnumerator preCollectorAnimation() 
    {
        while (true)
        {
            GameObject condenserLiquid = (GameObject)Instantiate(liquidDropletPrefab);
            condenserLiquid.transform.position = transform.position + new Vector3(600, 628, 0);
            condenserLiquid.GetComponent<liquid_molecule_movement>().condenserYlocation = transform.position.y;
            condenserLiquid.GetComponent<setColour>().liqCon = liqCon;
            yield return new WaitForSeconds(0.3f);
        }
    }

    //spawn collector liquid
    private IEnumerator collectorLiquidRise()
    {
        GameObject collectorLiquidInstance = (GameObject)Instantiate(collectorLiquidPrefab);
        collectorLiquidInstance.transform.position = transform.position + new Vector3(577, 130, -180);
        collectorLiquidInstance.GetComponent<colourChangeSimple>().liqCon = liqCon;

        GameObject collectorSurfaceInstance = (GameObject)Instantiate(waterSurfacePrefab);
        collectorSurfaceInstance.transform.position = transform.position + new Vector3(577+180, 130+28, 0);
        collectorSurfaceInstance.GetComponent<colourChange>().liqCon = liqCon;
        //collectorSurfaceInstance.transform.localScale -= new Vector3(5/23, 0, 5/23);

        //Vector3 prevPos = collectorSurfaceInstance.transform.position;
        //Vector3 objectScale = collectorLiquidInstance.transform.localScale;
        //og scale is 0.1 in y direction
        float totalTime = 1.80f;
        Vector3 scaleChange = new(0, (1 - collectorLiquidInstance.transform.localScale.y) / 90, 0);
        //float yIncrease = (1-objectScale.y)/90;
        float period = totalTime / 90;
        Vector3 posChange = new(0, 2.8f, 0);
        while (totalTime > 0)
        {
            //objectScale.y += yIncrease;
            collectorLiquidInstance.transform.localScale += scaleChange;
            collectorSurfaceInstance.transform.position += posChange;
            totalTime -= period;
            yield return new WaitForSeconds(period);
        }
    }

    //spawn liquid flowing down
    private IEnumerator postCollectorLiquidDown()
    {
        while (true)
        {
            GameObject condenserLiquid = (GameObject)Instantiate(liquidDropletPrefab);
            condenserLiquid.transform.position = transform.position + new Vector3(757, 122, 0);
            condenserLiquid.GetComponent<liquid_molecule_movement>().condenserYlocation = transform.position.y;
            condenserLiquid.GetComponent<setColour>().liqCon = liqCon;
            yield return new WaitForSeconds(0.3f);
        }
    }

    //spawn liquid flowing down
    private IEnumerator postCollectorLiquidBackToColumn()
    {
        while (true)
        {
            GameObject condenserLiquid = (GameObject)Instantiate(liquidDropletPrefab);
            condenserLiquid.transform.position = transform.position + new Vector3(745, 50, 0);
            condenserLiquid.GetComponent<liquid_molecule_movement>().condenserYlocation = transform.position.y;
            condenserLiquid.GetComponent<setColour>().liqCon = liqCon;
            yield return new WaitForSeconds(0.3f);
        }
    }

    //colour randomizer
    private float randomConcentrationGenerator(float ogCon)
    {
        if (ogCon < 0.5f)
        {
            return Random.Range(ogCon, 0.5f);
        }
        else if (ogCon > 0.5f)
        {
            return Random.Range(0.5f, ogCon);
        }
        else
        {
            return ogCon;
        }
    }
}
