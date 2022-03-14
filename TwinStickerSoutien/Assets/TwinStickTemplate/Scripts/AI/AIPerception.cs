using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PerceptionState
{
    PlayerInSight,
    PlayerLostSight,
}

public class AIPerception : MonoBehaviour
{
    private PlayerController _perceivedObject = null;

    public struct AIBrainEventArgs
    {
        public PerceptionState perceptionState;

        public AIBrainEventArgs(PerceptionState perceptionState)
        {
            this.perceptionState = perceptionState;
        }
    }

    public delegate void AIPerceptionEvent(AIPerception sender, AIBrainEventArgs args);
    public event AIPerceptionEvent PlayerPerceptionUpdated = null;

    public PlayerController PerceivedObject => _perceivedObject;

    private void OnTriggerEnter(Collider other)
    {
        var playerController = other.GetComponentInParent<PlayerController>();

        if (playerController != null)
        {
            _perceivedObject = playerController;
            PlayerPerceptionUpdated?.Invoke(this, new AIBrainEventArgs(PerceptionState.PlayerInSight));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var playerController = other.GetComponentInParent<PlayerController>();

        if (playerController != null)
        {
            _perceivedObject = null;
            PlayerPerceptionUpdated?.Invoke(this, new AIBrainEventArgs(PerceptionState.PlayerLostSight));
        }
    }

}
