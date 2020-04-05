using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;

public class CSV
{
    public static void write(string fileName, POD pod)
    {
        StreamWriter sw = new StreamWriter(fileName);
        for (int i = 0; i < pod.getObjectCount(); i++)
        {
            string str = String.Format("{0},{1}", pod.getObject(i), pod.getTimeSpan(i));
            sw.WriteLine(str);
        }
        sw.Close();
    }

    public static void read(string fileName, POD pod)
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
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
