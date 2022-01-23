using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask = new LayerMask();
    private Camera mainCamera;
    private List<Unit> selectedUnits = new List<Unit>();

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            foreach (Unit selectedUnit in selectedUnits)
            {
                selectedUnit.Deselect();
            }

            selectedUnits.Clear();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ClearSelectionArea();
            Debug.Log("MouseReleased");
        }
    }

    private void ClearSelectionArea()
    {
        Debug.Log("Clear Selection Area");
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            Debug.Log("Return !Physics");
            return;
        }

        if (!hit.collider.TryGetComponent<Unit>(out Unit unit))
        {
            Debug.Log("Return !hit");
            return;
        }

        if (!unit.hasAuthority)
        {
            Debug.Log("Return !unit");
            return;
        }

        selectedUnits.Add(unit);
        Debug.Log(selectedUnits.Count);
        foreach (Unit selectedUnit in selectedUnits)
        {
            selectedUnit.Select();
            Debug.Log(selectedUnit.name);
        }
    }
}
