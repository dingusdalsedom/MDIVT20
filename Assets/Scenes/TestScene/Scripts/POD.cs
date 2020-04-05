using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;

public class POD : MonoBehaviour
{
    // Constructor
    public POD()
    {
        objects = new List<string>();
        timeSpans = new List<long>();
        stopWatch = new Stopwatch();
        timeSpan = new TimeSpan();
    }

    // Used to verify that the code works
    public void writeInfo()
    {
        for (int i = 0; i < this.objects.Count; i++)
        {
            Console.WriteLine("{0},{1}", this.objects[i], this.timeSpans[i]);
        }
    }

    /*
    * Funcion for adding objects using a stopwatch to time for how long user
    * watched given object. Used together with Raycasting functionality
    */
    public void addTimedObject(string obj)
    {
        if (this.objects.Count == 0) // First item exception
        {
            stopWatch.Start();
        }
        else
        {
            // Else stop stopwatch, add time taken and restart
            stopWatch.Stop();
            this.timeSpans.Add(stopWatch.ElapsedMilliseconds);
            stopWatch.Restart();
        }
        // For debugging purposes
        Thread.Sleep(1);
        // Always add object
        this.objects.Add(obj);
    }

    /*
    * Used with raycaster, when doing recording stopTimer so it adds the last
    * timespan
    */
    public void stopTimer()
    {
        this.stopWatch.Stop();
        this.timeSpans.Add(stopWatch.ElapsedMilliseconds);
    }

    /*
    * Adding objects *from* CSV file where timespans are already supplied
    */
    public void addUntimedObject(string obj)
    {
        this.objects.Add(obj);
    }

    /*
        * Add timespan *from* CSV file
        */
    public void addTimeSpan(long timeSpan)
    {
        this.timeSpans.Add(timeSpan);
    }

    /*
    * Getter for the amount of objects currently stored in POD.
    * Should always be the same as timespans thus no need for getter for
    * timespans.
    */
    public int getObjectCount()
    {
        return objects.Count;
    }

    /*
    * Function for retrieving an object from the list, used in CSV Write
    */
    public string getObject(int i)
    {
        return this.objects[i];
    }

    /*
        * Function for retrieving an object from the list, used in CSV Write
        */
    public long getTimeSpan(int i)
    {
        return this.timeSpans[i];
    }

    /*
        * Private functions used in POD
        */
    private Stopwatch stopWatch;
    private TimeSpan timeSpan;
    private List<string> objects;
    private List<long> timeSpans;

    // File utility stuff
    static void CSVWrite(string fileName, POD pod)
    {
        StreamWriter sw = new StreamWriter(fileName);
        for (int i = 0; i < pod.getObjectCount(); i++)
        {
            string str = String.Format("{0},{1}", pod.getObject(i), pod.getTimeSpan(i));
            sw.WriteLine(str);
        }
        sw.Close();
    }

    static void CSVRead(string fileName, POD pod)
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
}

