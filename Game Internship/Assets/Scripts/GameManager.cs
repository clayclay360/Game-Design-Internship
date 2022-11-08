using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class GameManager
{
    public static int reliableDate;
    public static int currentDay;
    public static int totalDays = 2;
    public static bool readyForNextSource;

    public static int correctlySorted;
    public static int[] correctNeeded = { 2, 2 } ;

    public static string[] dayText = { "Day One", "Day Two", "Day Three", "Day Four", "Final Day"};
}
