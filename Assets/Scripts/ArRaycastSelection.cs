using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ArRaycastSelection : MonoBehaviour
{
    public ARRaycastManager raycastManager;

    public Camera arCamera;

    public GameObject window;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Cube"))
                {
                    window.SetActive(true);
                }
            }
            else
            {
                window.SetActive(false);
            }

        }
    }
}
