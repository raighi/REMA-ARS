using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CompassInteraction : MonoBehaviour
{
    [Header("AR")]
    public ARTrackedImageManager trackedImageManager;

    [Header("Game")]
    public GameObject gameBoard;       // ton GameBoard placé sur le plan
    public GameObject arrowObject;     // l'objet 3D à faire tourner (sur le GameBoard)
    public float activationDistance = 0.5f; // distance max en mètres

    private ARTrackedImage activeMarker;

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        // On récupère le marker dès qu'il est détecté
        foreach (var image in args.added)
            activeMarker = image;

        foreach (var image in args.updated)
            if (image.trackingState == TrackingState.Tracking)
                activeMarker = image;
    }

    void Update()
    {
        if (activeMarker == null || gameBoard == null) return;

        float distance = Vector3.Distance(
            activeMarker.transform.position,
            gameBoard.transform.position
        );

        if (distance < activationDistance)
        {
            arrowObject.SetActive(true);

            // On récupère uniquement la rotation Y du marker
            float markerRotationY = activeMarker.transform.eulerAngles.y;

            // On applique cette rotation à l'objet sur le GameBoard
            arrowObject.transform.rotation = Quaternion.Euler(0, markerRotationY, 0);
        }
        else
        {
            arrowObject.SetActive(false);
        }
    }
}