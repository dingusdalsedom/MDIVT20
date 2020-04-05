using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;

public class CSV
{
    public static void WriteSequential(string fileName, POD pod)
    {
        StreamWriter sw = new StreamWriter(fileName);
        for (int i = 0; i < pod.getObjectCount(); i++)
        {
            string str = String.Format("{0},{1}", pod.getObject(i), pod.getTimeSpan(i));
            sw.WriteLine(str);
        }
        sw.Close();
    }

    public static void ReadSequential(string fileName, POD pod)
    {
        StreamReader sr = new StreamReader(fileName);
        string readStr = sr.ReadLine();
        string[] tempStr;
        while (readStr != null)
        {
            tempStr = readStr.Split(',');
            pod.addUntimedObject(tempStr[0]);
            pod.addTimeSpan(Convert.ToInt64(tempStr[1]));
            readStr = sr.ReadLine();
        }
        sr.Close();
    }

    public static void ReadSummary(string fileName, POD pod)
    {
        StreamReader sr = new StreamReader(fileName);
        string readStr = sr.ReadLine();
        string[] tempStr;
        while(readStr != null)
        {
            tempStr = readStr.Split(',');
            pod.addSummaryObject(tempStr[0]);
            pod.addTimeSpan(Convert.ToInt64(tempStr[1]));
            readStr = sr.ReadLine();
        }
        sr.Close();
    }

    public static void WriteSummary(string fileName, POD pod)
    {
        StreamWriter sw = new StreamWriter(fileName);
        for(int i = 0; i < pod.getSummaryCount(); i++)
        {
            string str = String.Format("{0},{1}", pod.getSummaryObject(i), pod.getSummaryTimeSpan(i));
            sw.WriteLine(str);
        }
        sw.Close();
    }
}
