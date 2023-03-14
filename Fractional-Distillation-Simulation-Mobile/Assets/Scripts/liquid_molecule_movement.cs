
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liquid_molecule_movement : MonoBehaviour
{
    private float startY, startX;
    public float condenserYlocation;
    
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
        startX = transform.position.x;

        if (startX < -240)
        {
            //feed liquid flow
            StartCoroutine(leftBoundary(startY, -240));
        }
        else if (startY < 367)
        {
            //reboiler liquid flow
            StartCoroutine(leftBoundary(startY, 280));
        }
        else //condenser liquid flow
        {
            if (startY > condenserYlocation + 500)
            {
                //pre collector liquid flow
                StartCoroutine(lowerBoundary(600, condenserYlocation + 480)); //need condenser location
            }
            else if (startY > condenserYlocation + 100)
            {
                //post collector downward liquid flow
                StartCoroutine(leftBoundary(startY, 835));
            }
            else 
            {
                //post collector back to column flow
                StartCoroutine(rightBoundary(startX, 270));
            }
        }

        //colour change
    }
    
    //liquid falling down, exit left
    private IEnumerator leftBoundary(float startY, int endX) 
    {
        while (true)
        {
            if (transform.position.x > endX || transform.position.y > startY)
            { 
                Destroy(gameObject); 
            }
            else { 
                GetComponent<Rigidbody>().AddForce(new Vector3(0, -20, 0)); 
            }
            yield return null; 
        }
    }

    //liquid going right, exit right
    private IEnumerator rightBoundary(float startX, int endX)
    {
        while (true)
        {
            if (transform.position.x > startX || transform.position.x < endX)
            {
                Destroy(gameObject);
            }
            else
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(-10, 0, 0));
            }
            yield return null;
        }
    }

    //liquid going left, exit down
    private IEnumerator lowerBoundary(float startX, float endY)
    {
        while (true)
        {
            if (transform.position.x < startX || transform.position.y < endY)
            {
                Destroy(gameObject);
            }
            else
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(20, 0, 0));
            }
            yield return null;
        }
    }

}
