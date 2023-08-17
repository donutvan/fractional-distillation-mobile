using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class calculationTimeRunner : MonoBehaviour
{
    public float feedR, Xf, R, Vb,
        Fd, Fb, successD, successXd, successXb;
    public List<float> XvariableList = new List<float>();
    public List<float> YvariableList = new List<float>();
    public int trayNumber, feedPosition;
    public bool calculating;

    public TextMeshProUGUI averageTimeText1;

    private List<int> feedFlowRateValues = new List<int>() { 50, 75, 100 };
    private List<float> boilupRatioValues = new List<float>() { 2.5f, 5, 7.5f, 10 };
    private List<float> refluxRatioValues = new List<float>() { 2.5f, 5, 7.5f, 10 };

    private string path = "Assets/Stats/fractionalDistillationAllConditions.txt";
    private string noSolnPath = "Assets/Stats/fractionalDistillationNoSolutions.txt";
    private bool noSoln = false;
    // Start is called before the first frame update
    void Start()
    {
        calculating = true;


        
        Xf = 0.5f;
        /*
        feedR = SliderOptionsMenu.feedRateValue;
        Vb = SliderOptionsMenu.boilUpRatioValue;
        R = SliderOptionsMenu.refluxRatioValue;
        trayNumber = SliderOptionsMenu.trayNumberValue;
        feedPosition = SliderOptionsMenu.feedPositionValue;
        successD = 10;
        runTestCase(averageTimeText1);
        //runTestCase(8, 4, 10, 10, averageTimeText2);
        //runTestCase(20, 10, 5, 5, averageTimeText3);
        //runTestCase(20, 10, 10, 10, averageTimeText4);
        //runTestCase(20, 10, 2.5f, 2.5f, averageTimeText5);
        */
        runTestCaseWriteToFile(6); 
        //runTestCaseWriteToFile(7); 
        //runTestCaseWriteToFile(8); 
        //runTestCaseWriteToFile(9); 
        //runTestCaseWriteToFile(10); 
        //runTestCaseWriteToFile(11); 
        //runTestCaseWriteToFile(12); 
        //runTestCaseWriteToFile(13); 
        //runTestCaseWriteToFile(14); 
        //runTestCaseWriteToFile(15); 
        //runTestCaseWriteToFile(16);
        //runTestCaseWriteToFile(17);
        //runTestCaseWriteToFile(18);
        //runTestCaseWriteToFile(19);
        //runTestCaseWriteToFile(20);
    }

    public void runNoSoln() {
        for (int i = 6; i < 21; i++)
        {
            runTestCaseWriteToFileNoSoln(i);
        }
    }

    void WriteString(string outputLine)
    {
        
        //Write some text to the test.txt file
        StreamWriter writer = new(path, true);
        writer.WriteLine(outputLine);
        writer.Close();
        //Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path);
        //TextAsset asset = Resources.Load("fractionalDIstillationAllConditions.txt");
        //Print the text from the file
        //Debug.Log(asset.text);
    }

    void runTestCaseWriteToFileNoSoln(int trayNo)
    {
        float averageTime;
        trayNumber = trayNo;
        for (int i = 1; i < trayNo; i++)
        {
            feedPosition = i;
            foreach (var flowRate in feedFlowRateValues)
            {
                feedR = flowRate;
                foreach (var boilup in boilupRatioValues)
                {
                    Vb = boilup;
                    foreach (var reflux in refluxRatioValues)
                    {
                        R = reflux;
                        calculating = true;
                        averageTime = calculatorTimeMeasure(1);
                        if (noSoln) 
                        {
                            
                            StreamWriter writer = new(noSolnPath, true);
                            writer.WriteLine("Tray Number: " + trayNumber);
                            writer.WriteLine("Feed Postion: " + feedPosition);
                            writer.WriteLine("Feed Flow Rate: " + feedR);
                            writer.WriteLine("Boil-up Ratio: " + Vb);
                            writer.WriteLine("Reflux Ratio: " + R);
                            writer.WriteLine("Solution found: " + !noSoln);
                            writer.WriteLine("Time taken in ms: " + averageTime);
                            writer.WriteLine();
                            writer.Close();
                            noSoln = false;
                        }
                    }
                }
            }
        }

    }

    void runTestCaseWriteToFile(int trayNo) 
    {
        float averageTime;
        trayNumber = trayNo;
        for (int i = 1; i < trayNo; i++)
        {
            feedPosition = i;
            foreach (var flowRate in feedFlowRateValues)
            {
                feedR = flowRate;
                foreach (var boilup in boilupRatioValues)
                {
                    Vb = boilup;
                    foreach (var reflux in refluxRatioValues)
                    {
                        R = reflux;
                        calculating = true;
                        averageTime = calculatorTimeMeasure(1);
                        /*
                        StreamWriter writer = new StreamWriter(path, true);
                        writer.WriteLine("Tray Number: " + trayNumber);
                        writer.WriteLine("Feed Postion: " + feedPosition);
                        writer.WriteLine("Feed Flow Rate: " + feedR);
                        writer.WriteLine("Boil-up Ratio: " + Vb);
                        writer.WriteLine("Reflux Ratio: " + R);
                        writer.WriteLine("Solution found: " + !calculating);
                        writer.WriteLine("Time taken in ms: " + averageTime);
                        writer.WriteLine();
                        writer.Close();*/

                        if (calculating) {
                            StreamWriter writerNoSoln = new(noSolnPath, true);
                            writerNoSoln.WriteLine("Tray Number: " + trayNumber);
                            writerNoSoln.WriteLine("Feed Postion: " + feedPosition);
                            writerNoSoln.WriteLine("Feed Flow Rate: " + feedR);
                            writerNoSoln.WriteLine("Boil-up Ratio: " + Vb);
                            writerNoSoln.WriteLine("Reflux Ratio: " + R);
                            writerNoSoln.WriteLine("Solution found: " + !calculating);
                            writerNoSoln.WriteLine("Time taken in ms: " + averageTime);
                            writerNoSoln.WriteLine();
                            writerNoSoln.Close();
                        }
                        noSoln = false;
                    }
                }
            }
        }
        
    }

    void runTestCase(TextMeshProUGUI averageTimeText) 
    {
        float averageTime = calculatorTimeMeasure(1);
        string formatAveTime = "Average Time Taken:{0}ms\nTray No.:" + trayNumber + "\nFeed Pos.:" + feedPosition +
            "\nBoil-up Ratio:" + Vb + "\nReflux Ratio:" + R;
        averageTimeText.text = string.Format(formatAveTime, averageTime.ToString("F2"));
    }

    float calculatorTimeMeasure(int iterations)
    {
        Stopwatch stopWatch = new();
        //TimeSpan ts;
        float totalTimeMilliseconds = 0;
        for (int i = 0; i < iterations; i++)
        {
            stopWatch.Start();
            //startTime = Time.time;
            successD = 10;
            //100 calculators
            calculatorManager(0.5f, 0.5f, 0.005f, 0.01f, 3);
            if (calculating)
            {
                //1000 calculators
                UnityEngine.Debug.Log("Starting more rigorous calculations");

                calculatorManager(successXd - 0.01f, 0.02f, 0.00002f, 0.01f, 5);
                if (calculating)
                {
                    stopWatch.Stop();
                    UnityEngine.Debug.Log("No solution found!");
                    noSoln = true;
                    break;
                }
            }
            else { noSoln = false; }
            stopWatch.Stop();
            //ts = stopWatch.Elapsed;
            //endTime = Time.time;
            totalTimeMilliseconds += (float) stopWatch.Elapsed.TotalMilliseconds;
        }
        float aveTime;
        if (!noSoln)
        {
            aveTime = totalTimeMilliseconds / iterations;
            UnityEngine.Debug.Log("Average Time Taken (ms): " + aveTime);
        }
        else {  aveTime = -999; }
        return aveTime;
    }

    void calculatorManager(float Xd, float range, float step, float minD, int maxIterations)
    {
        float calculatorNumber = range / step;
        while (calculating && maxIterations != 0)
        {
            UnityEngine.Debug.Log("step: " + step + " through " + range + " " + calculatorNumber + " times from " + Xd);
            calculationSet(Xd, range, step, minD);
            /*
            Xd = successXd - (range/10);
            range /= 5;
            step = step / (calculatorNumber * 5);
            */
            Xd = successXd - (2 * step);
            range = step * 4;
            step = step / (calculatorNumber * 2);

            UnityEngine.Debug.Log("iterations left: " + maxIterations);
            maxIterations--;
        }
        UnityEngine.Debug.Log("calculating: " + calculating);
    }

    void calculationSet(float Xd, float range, float step, float minD)
    {
        int count = 0;
        float maxCount = range / step;
        while (calculating && count <= maxCount)
        {
            calculator(Xd + count * step, minD);
            UnityEngine.Debug.Log("count: " + count);
            count++;
        }
    }

    void calculator(float Xd, float minD)
    {
        float Xb = XB(Xd);
        if (Xb > 0 && Xb < 1)
        {
            float D = Dcalculator(Xd, Xb);
            //UnityEngine.Debug.Log("D: " + D + ", successD: " + successD);
            if (D < successD)
            {
                successD = D;
                successXd = Xd;
                UnityEngine.Debug.Log("successD updated: " + successD + ", successXd updated: " + successXd);
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

                    UnityEngine.Debug.Log("Solution found!");
                    UnityEngine.Debug.Log("D value: " + successD);
                    UnityEngine.Debug.Log("Fd_rate: " + Fd);
                    UnityEngine.Debug.Log("Fb_rate: " + Fb);
                    UnityEngine.Debug.Log("Xb value: " + successXb);
                    UnityEngine.Debug.Log("Xd value: " + successXd);
                    for (int j = 0; j < YvariableList.Count; j++)
                    {
                        UnityEngine.Debug.Log("Y" + (j) + " :" + YvariableList[j]);
                        UnityEngine.Debug.Log("X" + (j) + " :" + XvariableList[j]);
                    }
                }
            }
        }
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

    public void toMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
