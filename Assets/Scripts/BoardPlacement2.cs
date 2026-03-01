using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace TaskSystem
{
    public class BoardPlacement2 : MonoBehaviour
    {
        [SerializeField] private GameObject gameBoardPrefab;
        [SerializeField] private ARRaycastManager raycastManager;

        private GameObject _gameBoard;
        private readonly List<ARRaycastHit> _hits = new();

        private void OnEnable() => EnhancedTouchSupport.Enable();
        private void OnDisable() => EnhancedTouchSupport.Disable();

        private void Update()
        {
            var activeTouches = Touch.activeTouches;
            if (activeTouches.Count == 0) return;

            Touch touch = activeTouches[0];
            if (touch.phase != TouchPhase.Began) return;

            if (!raycastManager.Raycast(touch.screenPosition, _hits,
                TrackableType.PlaneWithinPolygon)) return;

            Pose hitPose = _hits[0].pose;
            Quaternion flatRotation = Quaternion.Euler(90, hitPose.rotation.eulerAngles.y, 0);

            if (_gameBoard == null)
            {
                _gameBoard = Instantiate(gameBoardPrefab, hitPose.position, flatRotation);
            }
            else
            {
                _gameBoard.transform.SetPositionAndRotation(hitPose.position, flatRotation);
            }

            // Rafraîchir le récap à chaque placement
            _gameBoard.GetComponent<GameBoardUI>().Refresh();
        }
    }
}