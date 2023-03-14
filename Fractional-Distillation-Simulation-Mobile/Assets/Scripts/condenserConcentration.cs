using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class condenserConcentration : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text shown will be formatted using this string. {0} is replaced with the actual value")]
    private string formatTextPercent;

    private string ratioStr;

    public float liqCon;

    public TextMeshProUGUI tmproTextPercent;

    // Start is called before the first frame update
    void Start()
    {
        formatTextPercent = "{0}%\nEthanol";

        ratioStr = (liqCon * 100).ToString("F2");
        
        tmproTextPercent.text = string.Format(formatTextPercent, ratioStr);
    }

    
}
