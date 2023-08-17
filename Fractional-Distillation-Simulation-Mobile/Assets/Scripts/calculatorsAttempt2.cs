using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class calculatorsAttempt2 : MonoBehaviour
{

    //Reflux Ratio, R
    //Reboiler Ratio, Vb;

    //Feed composition of ethanol, Xf
    //compositio of ethanol in vapour state, Y
    //compostion of ethanol in liquid state, X
    //Distillate amount, D
    //Mole fraction of ethanol at bottom, xB
    //Mole fraction of ethanol in distillate, XD
    public float offset, feedR, Fd, Fb,
        Xf, R, Vb, XD, D, xB, upperLimit;
    public int trayNumber = 8, feedPosition = 4;
    //float XB;
    public float successD = 0, successXD = 0, successXB = 0;



    public List<float> XvariableList = new List<float>();
    public List<float> YvariableList = new List<float>();
    // Use this for initialization
    //changes below

    //public List<float> XvariableList;
    //public List<float> YvariableList;
    public bool calculating;

    void Start()
    {

        calculating = true;
        Xf = 0.5f;
        feedR = SliderOptionsMenu.feedRateValue;
        Debug.Log("feed rate print from calculator script:" + feedR);
        Vb = SliderOptionsMenu.boilUpRatioValue;
        R = SliderOptionsMenu.refluxRatioValue;
        trayNumber = SliderOptionsMenu.trayNumberValue;
        feedPosition = SliderOptionsMenu.feedPositionValue;
        //XD, XB will change
        successXD = 0.7f;
        offset = 0.4f;

        if (trayNumber < 6) { trayNumber = 6; }
        else if (trayNumber > 20) { trayNumber = 20; }

        if (feedPosition >= trayNumber) { feedPosition = trayNumber - 1; }
        else if (feedPosition < 1) { feedPosition = 1; }


        calculatorManager();
    }

    float EEE(float Y)
    {
        //vapor liquid equilibrium eqn: x = (Ay^4 + By^3 + Cy^2 + Dy + E)/(y^4 + Fy^3 + Gy^2 + Hy + I)
        float E;
        E = (0.2257f * Mathf.Pow(Y, 4) - 0.1482f * Mathf.Pow(Y, 3) - 0.02055f * Mathf.Pow(Y, 2) + 0.02337f * Y + 0.0006006f) /
            (Mathf.Pow(Y, 4) - 2.998f * Mathf.Pow(Y, 3) + 3.744f * Mathf.Pow(Y, 2) - 2.119f * Y + 0.4538f);
        return E;
    }
    float RRR(float X, float xD)
    {
        //rectifying equation: y(n+1) = R/(R+1)*x(n) + x(D)/(R+1)
        float y;
        y = (R / (R + 1)) * X + (xD / (R + 1));
        return y;
    }
    void XB(float xD)
    {

        //equation to find xB for stripping equations
        //xB = (((Vb + 1) / Vb) * Xf - (R * Xf) / (R + 1) - XD / (R + 1)) * Vb;
        xB = (((((Vb + 1) / Vb) - (R / (R + 1))) * Xf) - ((1 / (R + 1)) * xD)) * Vb;

    }
    float SSS(float X)
    {
        //stripping equation to find y(m+1) from x(m)
        float y;
        y = ((Vb + 1) / Vb) * X - xB / Vb;
        return y;
    }

    void Calculator(float xD)
    {
        int i;
        float Yprev, Xprev;

        XB(xD);
        YvariableList.Add(xD);
        XvariableList.Add(EEE(xD));

        for (i = 1; i <= trayNumber; i++)
        {
            if (XvariableList[i - 1] >= Xf /*i <= feedPosition*/)
            {
                Yprev = RRR(XvariableList[i - 1], xD);
                YvariableList.Add(Yprev);
                Xprev = EEE(YvariableList[i]);
                XvariableList.Add(Xprev);

            }
            else
            {
                Yprev = SSS(XvariableList[i - 1]);
                YvariableList.Add(Yprev);
                Xprev = EEE(YvariableList[i]);
                XvariableList.Add(Xprev);

            }
        }
        D = Mathf.Abs(xB - XvariableList[XvariableList.Count - 1]);
    }

    void calculatorManager()
    {
        //XD from 0.5 to 0.9
        float step = 0.01f;
        float stepNum = offset / step;
        int i, k = 6;

        while (k > 0 /*D > (1/10000)*/)
        {
            Debug.Log("step is now " + step + " and successXD is " + successXD);
            XD = successXD - (offset / 2) - step;
            //repeat this segment for different XD and offset
            for (i = 0; i <= stepNum; i++)
            {
                XD += step;
                Calculator(XD);
                Debug.Log("XD entered: " + XD);
                Debug.Log("D: " + D);

                if (D < 0.01 && xB < 1 && xB > 0)
                {
                    if (successD == 0 || (D < successD))
                    {
                        successD = D;
                        successXD = XD;
                        successXB = xB;
                        Debug.Log("successXD updated, it is now: " + successXD + ", k is now: " + k);
                    }
                    else { Debug.Log("Wrong! Looping again."); }
                }
                else
                {
                    Debug.Log("Wrong! Looping again.");
                }
                XvariableList.Clear();
                YvariableList.Clear();
            }
            offset = step * 2;
            step /= 100;
            stepNum = offset / step;
            k--;
        }


        Calculator(successXD);
        Fb = feedR * ((Xf - XD) / (xB - XD));
        Fd = feedR - Fb;

        calculating = false;
        Debug.Log("End Loop!");
        Debug.Log("feedrate: " + feedR);
        Debug.Log("Fd_rate: " + Fd);
        Debug.Log("Fb_rate: " + Fb);
        Debug.Log("D value: " + successD);
        Debug.Log("xB value: " + successXB);
        Debug.Log("xD value: " + successXD);

        int j;
        for (j = 0; j <= YvariableList.Count - 1; j++)
        {
            Debug.Log("Y" + (j) + " :" + YvariableList[j]);
            Debug.Log("X" + (j) + " :" + XvariableList[j]);
        }

    }
}

