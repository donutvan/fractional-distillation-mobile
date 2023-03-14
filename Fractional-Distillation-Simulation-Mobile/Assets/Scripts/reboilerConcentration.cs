using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class reboilerConcentration : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The text shown will be formatted using this string. {0} is replaced with the actual value")]
    private string formatTextPercent;

    private string ratioStr;
    private GameObject reboilerTrayConeLiquid;

    public float liqCon;

    public TextMeshProUGUI tmproTextPercent;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("waterColumnSurfacePrefab").GetComponent<colourChangeSimple>().liqCon = liqCon;
        transform.Find("circularConeLastTray").GetComponent<colourChangeSimple>().liqCon = liqCon;
        transform.Find("square_mesh_circle").GetComponent<colourChange>().liqCon = liqCon;

    }

    
   
}
