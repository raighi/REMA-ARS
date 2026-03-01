using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class BoardPlacement : MonoBehaviour
{
    [SerializeField] private GameObject gameBoardPrefab;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private ARPlaneManager planeManager;

    private GameObject _gameBoard;
    private readonly List<ARRaycastHit> _hits = new();

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        // 1. Retrieve active touches
        var activeTouches = Touch.activeTouches;
        if (activeTouches.Count == 0) return;

        // 2. Get the first touch
        Touch touch = activeTouches[0];

        // 3. Filter Began phase only
        if (touch.phase != TouchPhase.Began) return;

        // 4. AR Raycast toward PlaneWithinPolygon
        if (!raycastManager.Raycast(touch.screenPosition, _hits, TrackableType.PlaneWithinPolygon))
            return;

        // 5. Use the closest hit pose
        Pose hitPose = _hits[0].pose;

        if (_gameBoard == null)
        {
            // Remplace hitPose.rotation par une rotation fixe
            _gameBoard = Instantiate(gameBoardPrefab, hitPose.position, Quaternion.identity);
            

            // Dans le bloc if (_gameBoard == null) :
            foreach (var plane in planeManager.trackables)
            plane.gameObject.SetActive(false);

planeManager.enabled = false;
        }
        else
        {
            // Move the existing GameBoard
            _gameBoard.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
        }
        
    }
}