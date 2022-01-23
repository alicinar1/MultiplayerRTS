using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovementController : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent _player;

    #region Server

    [Command]
    public void CmdMove(Vector3 position)
    {
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            return;
        }
        _player.SetDestination(position);
    }

    #endregion

}
