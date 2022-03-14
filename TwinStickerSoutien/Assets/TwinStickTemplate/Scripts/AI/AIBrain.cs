using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class AIBrain : MonoBehaviour
{
    public enum State
    {
        Idle = 0,
        Patrol,
        ChasePlayer
    }

    [SerializeField]
    private PathFollower _pathFollower = null;

    [SerializeField]
    private AIPerception _perception = null;

    [SerializeField]
    private Timer _timer = null;

    [SerializeField]
    private State _currentState = 0;


    private void OnEnable()
    {
        _perception.PlayerPerceptionUpdated -= OnPlayerPerceptionUpdated;
        _perception.PlayerPerceptionUpdated += OnPlayerPerceptionUpdated;

        _pathFollower.WaypointUpdated -= OnWaypointUpdated;
        _pathFollower.WaypointUpdated += OnWaypointUpdated;

        _timer.StartTimer();

        ChangeState(State.Idle);
    }

    private void OnDisable()
    {
        _perception.PlayerPerceptionUpdated -= OnPlayerPerceptionUpdated;
        _pathFollower.WaypointUpdated -= OnWaypointUpdated;
    }

    private void UpdateState()
    {
        switch (_currentState)
        {
            case State.Idle:
                {
                    bool result = _timer.UpdateTimer();

                    if (result == true)
                    {
                        ChangeState(State.Patrol);
                    }
                }
                break;
            case State.Patrol:
                break;
            case State.ChasePlayer:
                break;
            default:
                break;
        }
    }

    private void ChangeState(State newState)
    {
        // Transition OUT
        switch (_currentState)
        {
            case State.Idle:
                break;
            case State.Patrol:
                break;
            case State.ChasePlayer:
                break;
            default:
                break;
        }

        _currentState = newState;

        // Transition IN
        switch (_currentState)
        {
            case State.Idle:
                {
                    _pathFollower.Activate(false);
                    _timer.StartTimer();
                }
                break;
            case State.Patrol:
                {
                    _pathFollower.Activate(true);
                }
                break;
            case State.ChasePlayer:
                break;
            default:
                break;
        }
    }


    private void Update()
    {
        UpdateState();
    }

    private void OnPlayerPerceptionUpdated(AIPerception sender, AIPerception.AIBrainEventArgs args)
    {
        switch (args.perceptionState)
        {
            case PerceptionState.PlayerInSight:
                {
                    _pathFollower.Activate(false);
                }
                break;
            case PerceptionState.PlayerLostSight:
                {
                    _pathFollower.Activate(true);
                }
                break;
            default:
                break;
        }
    }


    private void OnWaypointUpdated(PathFollower sender, PathFollower.PathFollowerEventArgs args)
    {
        if (args.pathFollowerState == PathFollower.PathFollowerState.WaypointChanged)
        {
            ChangeState(State.Idle);
        }
    }

}
