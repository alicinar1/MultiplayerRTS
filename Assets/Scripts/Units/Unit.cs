using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [SerializeField] private Health health = null;
    [SerializeField] private UnitMovementController _unitMovement = null;
    [SerializeField] private UnityEvent _onSelected = null;
    [SerializeField] private UnityEvent _onDeselected = null;
    [SerializeField] private Targeter _targeter = null;

    public static event Action<Unit> ServerOnUnitSpawned;
    public static event Action<Unit> ServerOnUnitDespawned;
    public static event Action<Unit> AuthorityOnUnitSpawned;
    public static event Action<Unit> AuthorityOnUnitDespawned;
    public UnitMovementController UnitMovementController { get { return _unitMovement; } }

    public Targeter GetTargeter()
    {
        return _targeter;
    }

    #region Server

    public override void OnStartServer()
    {
        ServerOnUnitSpawned?.Invoke(this);
        health.ServerOnDie += HandleServerOnDie;
    }

    public override void OnStopServer()
    {
        ServerOnUnitDespawned?.Invoke(this);
        health.ServerOnDie -= HandleServerOnDie;
    }

    [Server]
    private void HandleServerOnDie()
    {
        NetworkServer.Destroy(gameObject);
    }

    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        AuthorityOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopClient()
    {
        if (!hasAuthority)
        {
            return;
        }

        AuthorityOnUnitDespawned?.Invoke(this);
    }

    [Client]
    public void Select()
    {
        if (!hasAuthority)
        {
            return;
        }

        _onSelected?.Invoke();
    }

    [Client]
    public void Deselect()
    {
        if (!hasAuthority)
        {
            return;
        }

        _onDeselected?.Invoke();
    } 

    #endregion



}
