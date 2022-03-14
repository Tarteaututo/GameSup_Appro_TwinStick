using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField]
    private Path _path = null;

    [SerializeField]
    private float _moveSpeed = 1f;

    [SerializeField]
    private float _nextWaypointDistanceThreshold = 0.25f;

    private int currentWaypointIndex = 0;

    public enum PathFollowerState
    {
        WaypointChanged
    }

    public struct PathFollowerEventArgs
    {
        public PathFollowerState pathFollowerState;

        public PathFollowerEventArgs(PathFollowerState pathFollowerState)
        {
            this.pathFollowerState = pathFollowerState;
        }
    }

    public delegate void PathFollowerEvent(PathFollower sender, PathFollowerEventArgs args);
    public event PathFollowerEvent WaypointUpdated = null;

    public void Activate(bool isActive)
    {
        enabled = isActive;
    }

    private void Update()
    {
        Vector3 position = _path.GetPosition(currentWaypointIndex);
        if (Vector3.Distance(position, transform.position) < _nextWaypointDistanceThreshold)
        {
            currentWaypointIndex += 1;

            if (currentWaypointIndex >= _path.GetWaypointCount)
            {
                currentWaypointIndex = 0;
            }

            position = _path.GetPosition(currentWaypointIndex);
            WaypointUpdated?.Invoke(this, new PathFollowerEventArgs(PathFollowerState.WaypointChanged));
        }
        transform.position = Vector3.MoveTowards(transform.position, position, _moveSpeed * Time.deltaTime);
    }
}
