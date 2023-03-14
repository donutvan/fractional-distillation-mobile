using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject, objectToPlace, spriteCursor;
    private GameObject columnInstance;
    private Color cursorColor = new Color(1, 1, 1, 0.6f);

    public ARRaycastManager raycastManager;

    public bool useCursor = true;
    private bool spawnedColumn = false;
    // Start is called before the first frame update
    void Start()
    {
        cursorChildObject.SetActive(useCursor);
        spriteCursor.GetComponent<SpriteRenderer>().color = cursorColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !spawnedColumn)
        {
            if (useCursor)
            {
                columnInstance = (GameObject)Instantiate(objectToPlace, transform.position, transform.rotation);
                Vector3 newAngle = columnInstance.transform.eulerAngles + 180 * Vector3.up;
                columnInstance.transform.eulerAngles = newAngle;
                spriteCursor.GetComponent<SpriteRenderer>().color = Color.clear;
                spawnedColumn = true;
                useCursor = false;
            }
            else 
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                if (hits.Count > 0)
                {
                    columnInstance = (GameObject)Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                    Vector3 newAngle = columnInstance.transform.eulerAngles + 180 * Vector3.up;
                    spriteCursor.GetComponent<SpriteRenderer>().color = Color.clear;
                    columnInstance.transform.eulerAngles = newAngle;
                    spawnedColumn = true;
                }
            }
        }
    }

    void UpdateCursor()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }

    public void despawnColumn()
    {
        Destroy(columnInstance);
        spawnedColumn = false;
        useCursor = true;
        spriteCursor.GetComponent<SpriteRenderer>().color = cursorColor;
    }
}
