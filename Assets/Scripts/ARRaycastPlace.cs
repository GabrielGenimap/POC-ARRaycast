using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;  // N'oubliez pas d'ajouter ce namespace pour l'UI

public class ARRaycastPlace : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject objectToPlace;

    public Camera arCamera;

    public GameObject popup;  // Le popup que nous afficherons

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        // Vérifier si un doigt touche l'écran
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);  // Obtenir le premier touché (si plusieurs doigts, utiliser 0 pour le premier)

            // Vérifier si le touché est un tap
            if (touch.phase == TouchPhase.Began)  // Phase de début de touché (équivalent à GetMouseButtonDown)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);  // Convertir la position du doigt en rayon dans l'espace 3D

                // Vérifier si le rayon touche une surface plane
                if (raycastManager.Raycast(ray, hits, TrackableType.Planes))
                {
                    Pose hitPose = hits[0].pose;

                    // Vérification si le rayon touche un objet
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        // Si l'objet touché est un cube, on ouvre le popup
                        if (hit.transform.CompareTag("Cube"))
                        {
                            OpenPopup(hit.transform.gameObject);
                        }
                    }
                    else
                    {
                        // Sinon, on instancie un cube
                        Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                    }
                }
            }
        }
    }

    // Ouvrir le popup et afficher des informations
    void OpenPopup(GameObject touchedObject)
    {
        // Afficher le popup
        popup.SetActive(true);
    }

    // Vous pouvez ajouter une fonction pour fermer le popup si nécessaire
    public void ClosePopup()
    {
        popup.SetActive(false);
    }
}
