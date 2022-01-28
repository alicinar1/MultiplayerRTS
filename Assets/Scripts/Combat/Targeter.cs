using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : NetworkBehaviour
{
    private Targetable _target;

    public Targetable Target { get { return _target; } }

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
        ClearTarget();
    }

    [Command]
    public void CmdSetTargey(GameObject targetGameObject)
    {
        if (!targetGameObject.TryGetComponent<Targetable>(out Targetable newTarget))
        {
            return;
        }

        this._target = newTarget;
    }

    [Server]
    public void ClearTarget()
    {
        _target = null;
    }

    #endregion


    #region Client

    #endregion
}
