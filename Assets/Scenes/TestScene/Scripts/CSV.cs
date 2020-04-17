using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;

public class CSV
{
    /*
     * Function that inserts time & data for filename as
     * well as generates a path for it
     */
    private static string CreatePath(string fileName)
    {
        string[] tempName = fileName.Split('.');
        string dateTime = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
        fileName = tempName[0] + dateTime + "." + tempName[1];
        return fileName;
    }

    /*
     * Function for writing sequential data to file
     */
    public static void WriteSequential(string fileName, POD pod)
    {
        StreamWriter sw = new StreamWriter(CreatePath(fileName));
        for (int i = 0; i < pod.getObjectCount(); i++)
        {
            string str = String.Format("{0},{1}", pod.getObject(i), pod.getTimeSpan(i));
            sw.WriteLine(str);
        }
        sw.Close();
    }

    /*
     * Function for writing Time Event Data such as currently looking at
     * and current position
     * Used to "replay" the players movement, both keyboard and mouse
     */
    public static void WriteTimeEventData(string fileName, POD pod)
    {
        StreamWriter sw = new StreamWriter(CreatePath(fileName));
        for(int i = 0; i < pod.getLocationCount() - 5; i++)
        {
            Vector3 look = pod.getLookingAtObject(i);
            Vector3 loc = pod.getLocationObject(i);
            Double time = pod.getFrameTime(i);

            string str = String.Format("{0},{1},{2},{3},{4},{5},{6}", look.x, look.y, look.z, loc.x, loc.y, loc.z, time);
            sw.WriteLine(str);
        }
        sw.Close();
    }
    
    /*
     * Function to load Time Event Data from file
     * Will be used to replay an earlier run from supplied CSV file
     */
    public static void ReadTimeEventData(string fileName, POD pod)
    {
        StreamReader sr = new StreamReader(fileName);
        string readStr = sr.ReadLine();
        string[] tempStr;
        while(readStr != null)
        {
            tempStr = readStr.Split(',');
            Vector3 tempLoc;
            Vector3 tempLook;
            tempLook.x = float.Parse(tempStr[0], CultureInfo.InvariantCulture.NumberFormat);
            tempLook.y = float.Parse(tempStr[1], CultureInfo.InvariantCulture.NumberFormat);
            tempLook.z = float.Parse(tempStr[2], CultureInfo.InvariantCulture.NumberFormat);
            tempLoc.x = float.Parse(tempStr[3], CultureInfo.InvariantCulture.NumberFormat);
            tempLoc.y = float.Parse(tempStr[4], CultureInfo.InvariantCulture.NumberFormat);
            tempLoc.z = float.Parse(tempStr[5], CultureInfo.InvariantCulture.NumberFormat);
            pod.addLookingAtVector(tempLook);
            pod.addCurrentLocation(tempLoc);
            readStr = sr.ReadLine();
        }
        sr.Close();
    }

    /*
     * Function to read sequential data from a CSV file into
     * a Plain Old Data (POD) class.
     */
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

    /*
     * Function to read CSV file and fill the summary lists in
     * the supplied POD
     */
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

    /*
     * Function to write summary from supplied POD onto a CSV file
     */
    public static void WriteSummary(string fileName, POD pod)
    {
        StreamWriter sw = new StreamWriter(CreatePath(fileName));
        for (int i = 0; i < pod.getSummaryCount(); i++)
        {
            string str = String.Format("{0},{1}", pod.getSummaryObject(i), pod.getSummaryTimeSpan(i));
            sw.WriteLine(str);
        }
        sw.Close();
    }
}
