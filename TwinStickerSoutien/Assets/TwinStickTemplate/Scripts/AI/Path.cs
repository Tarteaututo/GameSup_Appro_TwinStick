using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField]
    private Transform[] _waypoints = null;

    public int GetWaypointCount => _waypoints.Length;

    public Vector3 GetPosition(int waypointIndex)
    {
        return _waypoints[waypointIndex].position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0, length = _waypoints.Length; i < length - 1; i++)
        {
            Gizmos.DrawLine(_waypoints[i].position, _waypoints[i + 1].position);
        }
        Gizmos.DrawLine(_waypoints[_waypoints.Length - 1].position, _waypoints[0].position);
    }

}
