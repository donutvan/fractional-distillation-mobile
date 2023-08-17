using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class newMassRunCalculation : MonoBehaviour
{

    //Reflux Ratio, R
    //Reboiler Ratio, Vb;
    //Feed composition of ethanol, Xf
    //compositio of ethanol in vapour state, Y
    //compostion of ethanol in liquid state, X
    //Distillate amount, D
    //Mole fraction of ethanol at bottom, Xb
    //Mole fraction of ethanol in distillate, Xd

    public float feedR, Xf, R, Vb,
        Fd, Fb, successD, successXd, successXb;
    public List<float> XvariableList = new List<float>();
    public List<float> YvariableList = new List<float>();
    //public List<float> successxDList = new List<float>();
    //public List<List<List<float>>> allResultsList = new List<List<List<float>>>();
    public int trayNumber, feedPosition;
    public bool calculating;
    //public GameObject noSolutionErrorScreen;

    private List<int> feedFlowRateValues = new List<int>() { 50, 75, 100 };
    private List<float> boilupRatioValues = new List<float>() { 2.5f, 5, 7.5f, 10 };
    private List<float> refluxRatioValues = new List<float>() { 2.5f, 5, 7.5f, 10 };

    private string path = "Assets/Stats/fractionalDistillationAllConditions.txt";
    private string noSolnPath = "Assets/Stats/fractionalDistillationNoSolutions.txt";

    void Start()
    {
        calculating = true;
        Xf = 0.5f;
        //feedR = SliderOptionsMenu.feedRateValue;
        //Vb = SliderOptionsMenu.boilUpRatioValue;
        //R = SliderOptionsMenu.refluxRatioValue;
        //trayNumber = SliderOptionsMenu.trayNumberValue;
        //feedPosition = SliderOptionsMenu.feedPositionValue;

        //if (trayNumber < 6) { trayNumber = 6; }
        //else if (trayNumber > 20) { trayNumber = 20; }

        //if (feedPosition >= trayNumber) { feedPosition = trayNumber - 1; }
        //else if (feedPosition < 1) { feedPosition = 1; }
        //massRun(20);

        //singleRun(20, 10, 100, 2.5f, 2.5f);
        //massRun(6);
        /*
        for (int i = 6; i <= 20; i++) {
            StartCoroutine(massRunCoroutine(6));
        }
        */
        //noSolnPath = "Assets/Stats/6TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutine(6));

        //noSolnPath = "Assets/Stats/7TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutine(7));

        //noSolnPath = "Assets/Stats/8TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutine(8));

        //noSolnPath = "Assets/Stats/9TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutine(9));

        //noSolnPath = "Assets/Stats/10TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutine(10));

        //noSolnPath = "Assets/Stats/11TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutine(11));

        //noSolnPath = "Assets/Stats/12TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutine(12));

        //noSolnPath = "Assets/Stats/13TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutine(13));

        //noSolnPath = "Assets/Stats/14TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutine(14));

        //noSolnPath = "Assets/Stats/15TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutine(15));

        //noSolnPath = "Assets/Stats/16TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutine(16));

        //17 trays
        //noSolnPath = "Assets/Stats/17TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutinePartial(17, 1, 7));
        //noSolnPath = "Assets/Stats/17TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutinePartial(17, 8, 16));

        //18 trays
        //noSolnPath = "Assets/Stats/18TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutinePartial(18, 1, 9));
        //noSolnPath = "Assets/Stats/18TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutinePartial(18, 10, 17));

        //19 trays
        //noSolnPath = "Assets/Stats/19TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutinePartial(19, 1, 10));
        //noSolnPath = "Assets/Stats/19TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutinePartial(19, 11, 18));

        //20 trays
        //noSolnPath = "Assets/Stats/20TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutinePartial(20, 1, 10));
        //noSolnPath = "Assets/Stats/20TrayNoSoln.txt";
        //StartCoroutine(massRunCoroutinePartial(20, 11, 19));
    }

    private IEnumerator massRunCoroutinePartial(int trayNo, int startFeed, int endFeed)
    {
        for (int feedPos = startFeed; feedPos <= endFeed; feedPos++)
        {
            foreach (int flowRate in feedFlowRateValues)
            {
                foreach (float boilup in boilupRatioValues)
                {
                    foreach (float reflux in refluxRatioValues)
                    {
                        singleRun(trayNo, feedPos, flowRate, boilup, reflux);
                        yield return null;
                    }
                }
            }
        }
        Debug.Log("FINALLY DONE");
    }

    private IEnumerator massRunCoroutine(int trayNo)
    {
        for (int feedPos = 1; feedPos < trayNo; feedPos++)
        {
            foreach (int flowRate in feedFlowRateValues)
            {
                foreach (float boilup in boilupRatioValues)
                {
                    foreach (float reflux in refluxRatioValues)
                    {
                        singleRun(trayNo, feedPos, flowRate, boilup, reflux);
                        yield return null;
                    }
                }
            }
        }
        Debug.Log("FINALLY DONE");
    }

    void massRun(int trayNo)
    {
        for (int feedPos = 1; feedPos < trayNo; feedPos++)
        {
            foreach (int flowRate in feedFlowRateValues)
            {
                foreach (float boilup in boilupRatioValues)
                {
                    foreach (float reflux in refluxRatioValues)
                    {
                        singleRun(trayNo, feedPos, flowRate, boilup, reflux);
                    }
                }
            }
        }
    }

    void singleRun(int trayNo, int feedPos, int flowRate, float boilup, float reflux) {
        trayNumber = trayNo;
        feedPosition = feedPos;
        feedR = flowRate;
        Vb = boilup;
        R = reflux;

        calculating = true;
        successD = 10;
        //100 calculators
        calculatorManager(0.5f, 0.5f, 0.005f, 0.01f, 3);
        if (calculating)
        {
            //1000 calculators
            Debug.Log("Starting more rigorous calculations");
            successD = 10;
            calculatorManager(successXd - 0.01f, 0.02f, 0.00002f, 0.01f, 5);
            if (calculating)
            {
                Debug.Log("No solution found");
                
                StreamWriter writerNoSoln = new(noSolnPath, true);
                writerNoSoln.WriteLine("Tray Number: " + trayNumber);
                writerNoSoln.WriteLine("Feed Postion: " + feedPosition);
                writerNoSoln.WriteLine("Feed Flow Rate: " + feedR);
                writerNoSoln.WriteLine("Boil-up Ratio: " + Vb);
                writerNoSoln.WriteLine("Reflux Ratio: " + R);
                writerNoSoln.WriteLine("Solution found: " + !calculating);
                writerNoSoln.WriteLine();
                writerNoSoln.Close();
                
            }
        }
    }

    float EEE(float Y)
    {
        //vapor liquid equilibrium eqn: x = (Ay^4 + By^3 + Cy^2 + Dy + E)/(y^4 + Fy^3 + Gy^2 + Hy + I)
        float X;
        X = (0.2257f * Mathf.Pow(Y, 4) - 0.1482f * Mathf.Pow(Y, 3) - 0.02055f * Mathf.Pow(Y, 2) + 0.02337f * Y + 0.0006006f) /
            (Mathf.Pow(Y, 4) - 2.998f * Mathf.Pow(Y, 3) + 3.744f * Mathf.Pow(Y, 2) - 2.119f * Y + 0.4538f);
        return X;
    }
    float RRR(float X, float Xd)
    {
        //rectifying equation: y(n+1) = R/(R+1)*x(n) + x(D)/(R+1)
        float y;
        y = (R / (R + 1)) * X + (Xd / (R + 1));
        return y;
    }
    float XB(float Xd)
    {
        float Xb;
        //equation to find Xb for stripping equations
        //Xb = (((Vb + 1) / Vb) * Xf - (R * Xf) / (R + 1) - Xd / (R + 1)) * Vb;
        Xb = (((((Vb + 1) / Vb) - (R / (R + 1))) * Xf) - ((1 / (R + 1)) * Xd)) * Vb;
        return Xb;
    }
    float SSS(float X, float Xb)
    {
        //stripping equation to find y(m+1) from x(m)
        float y;
        y = ((Vb + 1) / Vb) * X - Xb / Vb;
        return y;
    }

    float Dcalculator(float Xd, float Xb)
    {
        List<float> localXvariableList = new List<float>();
        List<float> localYvariableList = new List<float>();

        localYvariableList.Add(Xd);
        localXvariableList.Add(EEE(Xd));
        for (int i = 1; i <= trayNumber; i++)
        {
            if (i <= feedPosition)
            {
                localYvariableList.Add(RRR(localXvariableList[i - 1], Xd));
                localXvariableList.Add(EEE(localYvariableList[i]));
            }
            else
            {
                localYvariableList.Add(SSS(localXvariableList[i - 1], Xb));
                localXvariableList.Add(EEE(localYvariableList[i]));
            }
        }
        return Mathf.Abs(Xb - localXvariableList[localXvariableList.Count - 1]);
    }

    void calculator(float Xd, float minD)
    {
        float Xb = XB(Xd);
        if (Xb > 0 && Xb < 1)
        {
            float D = Dcalculator(Xd, Xb);
            //Debug.Log("D: " + D + ", successD: " + successD);
            if (D < successD)
            {
                successD = D;
                successXd = Xd;
                //Debug.Log("successD updated: " + successD + ", successXd updated: " + successXd);
                if (D <= minD)
                {
                    calculating = false;

                    successXb = Xb;
                    Fb = feedR * ((Xf - Xd) / (Xb - Xd));
                    Fd = feedR - Fb;
                    YvariableList.Add(Xd);
                    XvariableList.Add(EEE(Xd));
                    for (int i = 1; i <= trayNumber; i++)
                    {
                        if (i <= feedPosition)
                        {
                            YvariableList.Add(RRR(XvariableList[i - 1], Xd));
                            XvariableList.Add(EEE(YvariableList[i]));
                        }
                        else
                        {
                            YvariableList.Add(SSS(XvariableList[i - 1], Xb));
                            XvariableList.Add(EEE(YvariableList[i]));
                        }
                    }

                    Debug.Log("Solution found!");
                    Debug.Log("D value: " + successD);
                    Debug.Log("Fd_rate: " + Fd);
                    Debug.Log("Fb_rate: " + Fb);
                    Debug.Log("Xb value: " + successXb);
                    Debug.Log("Xd value: " + successXd);
                    for (int j = 0; j < YvariableList.Count; j++)
                    {
                        Debug.Log("Y" + (j) + " :" + YvariableList[j]);
                        Debug.Log("X" + (j) + " :" + XvariableList[j]);
                    }
                }
            }
        }
    }

    void calculationSet(float Xd, float range, float step, float minD)
    {
        int count = 0;
        float maxCount = range / step;
        while (calculating && count <= maxCount)
        {
            calculator(Xd + count * step, minD);
            //Debug.Log("count: " + count);
            count++;
        }
    }

    void calculatorManager(float Xd, float range, float step, float minD, int maxIterations)
    {
        float calculatorNumber = range / step;
        while (calculating && maxIterations != 0)
        {
            //Debug.Log("step: " + step + " through " + range + " " + calculatorNumber + " times from " + Xd);
            calculationSet(Xd, range, step, minD);
            /*
            Xd = successXd - (range/10);
            range /= 5;
            step = step / (calculatorNumber * 5);
            */
            Xd = successXd - (2 * step);
            range = step * 4;
            step /= (calculatorNumber * 2);

            //Debug.Log("iterations left: " + maxIterations);
            maxIterations--;
        }
        //Debug.Log("calculating: " + calculating);
    }

}


