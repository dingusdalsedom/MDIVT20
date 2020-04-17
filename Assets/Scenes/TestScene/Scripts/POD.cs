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
        lookingAt = new List<Vector3>();
        currentLocation = new List<Vector3>();
        FrameTime = new List<Double>();

    }

    /*
     * Function for adding a vector to the looking at list
     */
    public void addLookingAtVector(Vector3 v)
    {
        lookingAt.Add(v);
    }
    public void addFrameTime(Double time)
    {
        FrameTime.Add(time);
    }
    /*
     * Function for adding a vector to the current location list
     */
    public void addCurrentLocation(Vector3 v)
    {
        currentLocation.Add(v);
    }

    /*
     * Function for getting the vector3 from list at index i
     */
    public Vector3 getLocationObject(int i)
    {
        return currentLocation[i];
    }
    public Double getFrameTime(int i)
    {
        return FrameTime[i];
    }

    /*
     * Function for getting the vector3 from list at index i
     */
    public Vector3 getLookingAtObject(int i)
    {
        return lookingAt[i];
    }

    // Function for fetching the numbers of items in lookingAt list
    // Should *always* be the same as lookingAt
    public int getLocationCount()
    {
        return currentLocation.Count;
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
    public void addSummary(string obj, long timeElapsed)
    {
        if (objectsSummary.Contains(obj))
        {
            int index = objectsSummary.IndexOf(obj);
            timeSpansSummary[index] = timeSpansSummary[index] + timeElapsed;
        }
        else
        {
            objectsSummary.Add(obj);
            timeSpansSummary.Add(timeElapsed);
        }
    }
    /*
    * Funcion for adding objects using a stopwatch to time for how long user
    * watched given object. Used together with Raycasting functionality
    */
    public void addTimedObject(string obj)
    {
        long timeElapsed = 0;
        if (this.objects.Count == 0) // First item exception
        {
            stopWatch.Start();
        }
        else
        {
            // Else stop stopwatch, add time taken and restart
            stopWatch.Stop();
            timeElapsed = stopWatch.ElapsedMilliseconds;
            this.timeSpans.Add(timeElapsed);
            stopWatch.Restart();
        }
        // For debugging purposes
        //Thread.Sleep(1);

        // Add object to summary
        if(objects.Count > 0)
            addSummary(objects[objects.Count - 1], timeElapsed);
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
    public double getCurrentStopwatchTime()
    {
        return stopWatch.Elapsed.TotalSeconds;
    }

    /*
     * Function to see how many items in the list that only appears once
     */
    public int nr_of_objects_looked_at()
    {
        List<string> found_singles = new List<string>();
        foreach(string item in objects)
        {
            if(!found_singles.Contains(item))
            {
                found_singles.Add(item);
            }
        }

        return found_singles.Count;
    }
    /*
    * Private variables used in the POD class
    */
    private System.Diagnostics.Stopwatch stopWatch;
    private string lastObject;
    private List<string> objects;
    private List<string> objectsSummary;
    private List<long> timeSpans;
    private List<long> timeSpansSummary;
    private List<Vector3> lookingAt;
    private List<Vector3> currentLocation;
    private List<Double> FrameTime;
}

