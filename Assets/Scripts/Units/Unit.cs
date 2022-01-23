using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [SerializeField] private UnityEvent _onSelected = null;
    [SerializeField] private UnityEvent _onDeselected = null;

    #region Client

    [Client]
    public void Select()
    {
        if (!hasAuthority)
        {
            return;
        }

        _onSelected?.Invoke();
        Debug.Log("Selected");
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
