using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{

    public static Transform[] waypoints;

    void Awake()
    {
        waypoints = new Transform[transform.childCount];
        FillWaypoints();
    }
    


    private void FillWaypoints()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }

    public static bool EndWasReached(int waypointindex) => waypointindex >= waypoints.Length - 1;

    public static Transform GetTargetWaypoint(int waypointindex, Transform target) => waypoints[waypointindex];
}
