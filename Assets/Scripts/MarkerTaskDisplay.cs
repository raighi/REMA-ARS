using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace TaskSystem
{
public class MarkerTaskDisplay : MonoBehaviour
{   
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private GameObject taskCanvasPrefab;

    private Dictionary<string,GameObject> _activeCanvases = new();


    void SpawnCanvas(ARTrackedImage image)
        {
            var task = TaskManager.Instance.GetTask(image.referenceImage.name);
            if (task==null) return;

            var canvas = Instantiate(taskCanvasPrefab,
                image.transform.position + Vector3.up * 0.1f,
                image.transform.rotation);
            
            canvas.GetComponentInChildren<TaskCardUI>().Setup(task);
            _activeCanvases[image.referenceImage.name] = canvas;
        }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var image in trackedImageManager.trackables)
        {
            string markerName = image.referenceImage.name;
            if (image.trackingState== TrackingState.Tracking)
            {
                if (!_activeCanvases.ContainsKey(markerName))
                {
                    SpawnCanvas(image);

                }
            else
                {
                    _activeCanvases[markerName].transform.SetPositionAndRotation(
                        image.transform.position + Vector3.up *0.1f,
                        image.transform.rotation
                    );
                };
            }
            else
                {
                    if (_activeCanvases.TryGetValue(markerName, out var canvas))
                    {
                        canvas.SetActive(false);
                    }
                }
        }
    }
}
}