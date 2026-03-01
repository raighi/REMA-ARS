using System.Threading.Tasks.Sources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TaskSystem
{
public class TaskCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button completeButton;
    [SerializeField] private GameObject completedVisual;

    private RoutineTask _task;


    public void Setup(RoutineTask task)
    {
        _task = task;
        titleText.text = task.taskTitle;
        completedVisual.SetActive(task.isCompleted);
        completeButton.onClick.AddListener(OnComplete);

    }

    void OnComplete()
    {
        TaskManager.Instance.CompleteTask(_task.markerName);
        completedVisual.SetActive(true);
        completeButton.gameObject.SetActive(false);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}