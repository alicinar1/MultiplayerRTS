using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Building building = null;
    [SerializeField] private Image IconImage = null;
    [SerializeField] private TMP_Text priceText = null;
    [SerializeField] private LayerMask floorMask = new LayerMask();

    private Camera mainCamera;
    private RTSPlayer player;
    private GameObject buildingPreciewInstance;
    private Renderer buildingRendererInstance;


    private void Start()
    {
        mainCamera = Camera.main;

        IconImage.sprite = building.Icon;
        priceText.text = building.Price.ToString();
        buildingPreciewInstance = building.BuildingPreview;
    }

    private void Update()
    {
        if (player == null)
        {
            player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
            Debug.Log("SearchingPlayer");
        }

        if (buildingPreciewInstance == null)
        {
            return;
        }

        UpdateBuildingPreview();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        buildingPreciewInstance = Instantiate(building.BuildingPreview);
        buildingRendererInstance = buildingPreciewInstance.GetComponentInChildren<Renderer>();

        buildingPreciewInstance.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buildingPreciewInstance == null)
        {
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, floorMask))
        {

        }

        Destroy(buildingPreciewInstance);
    }

    private void UpdateBuildingPreview()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, floorMask))
        {
            return;
        }

        buildingPreciewInstance.transform.position = hit.point;

        if (!buildingPreciewInstance.activeSelf)
        {
            buildingPreciewInstance.SetActive(true);
        }
    }
}
