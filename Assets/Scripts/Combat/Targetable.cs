using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : NetworkBehaviour
{
    [SerializeField] private Transform _aimAtPoint;

    public Transform GetAimAtPoint
    {
        get { return _aimAtPoint; }
    }

}
