using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;



[System.Serializable]
public class RoutineTask
{
    public string markerName;    // ex: "Marker_01"
    public string taskTitle;     // ex: "Boire un verre d'eau"
    public string taskDetail;    // ex: "Au moins 250ml"
    public bool isCompleted;
}
namespace TaskSystem
{
public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    private Dictionary<string, RoutineTask> _tasks = new()
    {
        {"marker_01", new RoutineTask { markerName = "marker_01", taskTitle = "Boire un litre d'eau"}},
        {"marker_02", new RoutineTask { markerName = "marker_02", taskTitle = "Faire le lit"}},
        {"marker_03", new RoutineTask { markerName = "marker_03", taskTitle = "Faire les étirements"}},
        {"marker_04", new RoutineTask { markerName = "marker_04", taskTitle = "Se brosser les dents"}},
        {"marker_05", new RoutineTask { markerName = "marker_05", taskTitle = "Prendre une douche"}},
        {"marker_06", new RoutineTask { markerName = "marker_06", taskTitle = "S'habiller"}},
        {"marker_07", new RoutineTask { markerName = "marker_07", taskTitle = "Ranger la chambre"}},
        {"marker_08", new RoutineTask { markerName = "marker_08", taskTitle = "Petit-déjeuner"}}
        };
    
    void Awake()
    {
        Instance = this;
        LoadFromPlayerPrefs();
    }
    
    public RoutineTask GetTask(string markerName)
    => _tasks.TryGetValue(markerName, out var task) ? task : null;

    public void SetTaskTitle(string markerName, string title)
    {
        if (_tasks.ContainsKey(markerName))
        {
            _tasks[markerName].taskTitle = title;
            SaveToPlayerPrefs();
        }
    }

    public void CompleteTask(string markerName)
    {
        if (_tasks.ContainsKey(markerName))
        {
            _tasks[markerName].isCompleted = true;
            SaveToPlayerPrefs();
        }
    }

    private void SaveToPlayerPrefs()
    {
        foreach (var task in _tasks.Values)
        {
            PlayerPrefs.SetString(task.markerName, task.taskTitle);
        }
        PlayerPrefs.Save();
    }

    private void LoadFromPlayerPrefs()
    {
        foreach (var task in _tasks.Values)
        {if (PlayerPrefs.HasKey(task.markerName))
            task.taskTitle = PlayerPrefs.GetString(task.markerName);}
    }

    public List<RoutineTask> GetAllTasks()
{
    return new List<RoutineTask>(_tasks.Values);
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