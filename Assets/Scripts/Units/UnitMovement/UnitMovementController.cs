using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovementController : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Targeter targeter = null;
    [SerializeField] private float _chaseRange = 10f;

    #region Server

    public override void OnStartServer()
    {
        GameOverHandler.ServerOnGameOver += ServerHandleGameOver;
    }

    public override void OnStopServer()
    {
        GameOverHandler.ServerOnGameOver -= ServerHandleGameOver;
    }

    [Server]
    private void ServerHandleGameOver()
    {
        _agent.ResetPath();
    }

    [ServerCallback]
    private void Update()
    {
        Targetable target = targeter.Target;

        if (target != null)
        {
            if ((target.transform.position - transform.position).sqrMagnitude > _chaseRange * _chaseRange)
            {
                _agent.SetDestination(target.transform.position);
            }
            else if (_agent.hasPath)
            {
                _agent.ResetPath();
            }

            return;
        }

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
