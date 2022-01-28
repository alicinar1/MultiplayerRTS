﻿using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : NetworkBehaviour
{
    [SerializeField] private Health health = null;


    public static event Action<int> ServerOnPlayerDie;
    public static event Action<UnitBase> ServerOnBaseSpawn;
    public static event Action<UnitBase> ServerOnBaseDespawn;

    #region Server
    public override void OnStartServer()
    {
        health.ServerOnDie += ServerHandleDie;

        ServerOnBaseSpawn?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnBaseDespawn?.Invoke(this);

        health.ServerOnDie += ServerHandleDie;
    }

    [Server]
    private void ServerHandleDie()
    {
        ServerOnPlayerDie?.Invoke(connectionToClient.connectionId);
        NetworkServer.Destroy(gameObject);
    }
    #endregion

    #region Client

    #endregion
}
