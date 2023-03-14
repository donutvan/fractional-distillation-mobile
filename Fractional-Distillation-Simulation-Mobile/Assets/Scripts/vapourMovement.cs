using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vapourMovement : MonoBehaviour
{
    public int trayNumber;

    private float startY, startX;

    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
        startX = transform.position.x;

        if (gameObject.tag == "column") //column vapour
        {
            StartCoroutine(columnVapourAnimation(startY + 240 + (trayNumber - 1) * 120, startY));
        }
        else if (startY < 360)
        {
            if (startX <= 660) //reboiler chamber vapour
            {
                StartCoroutine(reboilerChamberVapourAnimation());
            }
            else if (startX < 760) //reboiler tube left vapour
            {
                StartCoroutine(reboilerTubeLeft());
            }
            else //reboiler tube up vapour
            {
                StartCoroutine(reboilerTubeUp());
            }
        }
        else
        {
            if (startX >= 200)
            {
                StartCoroutine(condenserChamberVapourAnimation());
            }
            else
            {
                StartCoroutine(condenserTubeUp());
            }
        }
    }

    //animation for column vapour
    private IEnumerator columnVapourAnimation(float maxY, float minY)
    {
        while (true) 
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 5 + Random.Range(0, 2), 0));
            if (transform.position.y >= maxY || transform.position.y < minY) { Destroy(gameObject); }
            yield return null;
        }
    }

    //animation for reboiler chamber vapour
    private IEnumerator reboilerChamberVapourAnimation()
    {
        GetComponent<SphereCollider>().material.bounciness = 1;
        while (true) 
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(15, 10 * Random.Range(-3, 3), 10 * Random.Range(-3, 3)));
            if (transform.position.x > 670 || transform.position.x < 285) { Destroy(gameObject); }
            yield return null;
        }
    }

    //animation for reboiler tube to left
    private IEnumerator reboilerTubeLeft()
    {
        while (true) 
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(15 + Random.Range(1, 2), 0, 0));
            if (transform.position.x < 688 || transform.position.x > 874) { Destroy(gameObject); }
            yield return null;
        }
    }

    //animation for reboiler tube up
    private IEnumerator reboilerTubeUp()
    {
        while (true)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 15 + Random.Range(1, 2), 0));
            if (transform.position.x < 260) { Destroy(gameObject); }
            yield return null;
        }
    }

    //animation for condenser tube up
    private IEnumerator condenserTubeUp()
    {
        while (true)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 15 + Random.Range(1, 2), 0));
            if (transform.position.x > 210) { Destroy(gameObject); }
            yield return null;
        }
    }

    //animation for condenser chamber vapour
    private IEnumerator condenserChamberVapourAnimation()
    {
        GetComponent<SphereCollider>().material.bounciness = 1;
        while (true)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(15, 10 * Random.Range(-3, 3), 10 * Random.Range(-3, 3)));
            if (transform.position.x > 590 || transform.position.x < 205) { Destroy(gameObject); }
            yield return null;
        }
    }
}
