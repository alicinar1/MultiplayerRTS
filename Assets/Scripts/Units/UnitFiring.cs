using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFiring : NetworkBehaviour
{
    [SerializeField] private Targeter _targeter;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private Transform projectileSpawnPoint = null;
    [SerializeField] private float fireRange = 5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float rotationSpeed = 20f;

    private float lastFireTime;

    [ServerCallback]
    private void Update()
    {
        if (_targeter.Target == null)
        {
            return;
        }

        if (!CanFireAtTarget())
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(_targeter.Target.transform.position - transform.position);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Time.time > (1 / fireRate) + lastFireTime)
        {
            Quaternion projectileRotation = Quaternion.LookRotation(_targeter.Target.GetAimAtPoint.position - projectileSpawnPoint.position);

            GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileRotation);

            NetworkServer.Spawn(projectileInstance, connectionToClient);
            lastFireTime = Time.time;
        }
    }

    private bool CanFireAtTarget()
    {
        return (_targeter.Target.transform.position - transform.position).sqrMagnitude <= fireRange * fireRange;
    }
}
