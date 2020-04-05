using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;

public class POD
{
    // Constructor
    public POD()
    {
        objects = new List<string>();
        objectsSummary = new List<string>();
        timeSpans = new List<long>();
        timeSpansSummary = new List<long>();
        stopWatch = new System.Diagnostics.Stopwatch();
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
     * Helper function to attempt to add object to summary list
     */
    public void addSummary(string obj, long timeElapsed, bool condition)
    {
        if (objectsSummary.Contains(obj))
        {

        }
    }
    /*
    * Funcion for adding objects using a stopwatch to time for how long user
    * watched given object. Used together with Raycasting functionality
    */
    public void addTimedObject(string obj)
    {
        long timeElapsed = 0;
        bool condition = false;
        if (this.objects.Count == 0) // First item exception
        {
            stopWatch.Start();
        }
        else
        {
            // Else stop stopwatch, add time taken and restart
            stopWatch.Stop();
            timeElapsed = stopWatch.ElapsedMilliseconds;
            condition = true;
            this.timeSpans.Add(timeElapsed);
            stopWatch.Restart();
        }
        // For debugging purposes
        Thread.Sleep(1);
        // Add object to summary
        addSummary(obj, timeElapsed);
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
        string obj = objects[objects.Count - 1];
        if (objectsSummary.Contains(obj))
        {
            int index = objectsSummary.IndexOf(obj);
            timeSpansSummary[index] = timeSpansSummary[index] + stopWatch.ElapsedMilliseconds;
        }
        else
        {
            objectsSummary.Add(objects[objects.Count - 1]);
            timeSpansSummary.Add(stopWatch.ElapsedMilliseconds);
        }
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
     * Add summary timespan *from* CSV file
     */
    public void addSummaryTimeSpan(long timeSpan)
    {
        this.timeSpansSummary.Add(timeSpan);
    }

    /*
     * Add summary object *from* CSV file
     */
     public void addSummaryObject(string obj)
    {
        this.objectsSummary.Add(obj);
    }

    /*
     * Getter for the amount of objects currently stored in POD.
     * Should always be the same as timespans thus no need for getter for
     * timespans.
     */
    public int getObjectCount()
    {
        return this.objects.Count;
    }

    /*
     * Getter for the amount of objects currently stored in summary List,
     * this should match the amount of items in timeSpansSummary.
     */
    public int getSummaryCount()
    {
        return this.objectsSummary.Count;
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
     * Function for retrieving an object from the list, used in CSV summary write
     */
    public string getSummaryObject(int i)
    {
        return this.objectsSummary[i];
    }

    /*
     * Function for retrieving an object from the list, used in CSV summary write
     */
    public long getSummaryTimeSpan(int i)
    {
        return this.timeSpansSummary[i];
    }

    /*
    * Private functions used in POD
    */
    private System.Diagnostics.Stopwatch stopWatch;
    private List<string> objects;
    private List<string> objectsSummary;
    private List<long> timeSpans;
    private List<long> timeSpansSummary;
}

