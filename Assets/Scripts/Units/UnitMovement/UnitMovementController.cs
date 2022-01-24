using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovementController : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Targeter targeter = null;

    #region Server

    [ServerCallback]
    private void Update()
    {
        if (!_agent.hasPath)
        {
            return;
        }

        if (_agent.remainingDistance > _agent.stoppingDistance)
        {
            return;
        }

        _agent.ResetPath();
    }

    [Command]
    public void CmdMove(Vector3 position)
    {
        targeter.ClearTarget();

        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            return;
        }
        _agent.SetDestination(position);
    }

    #endregion

}
