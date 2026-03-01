using TMPro;
using UnityEngine;

namespace TaskSystem
{
    public class TaskRowUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rowText;

        public void Setup(RoutineTask task)
        {
            rowText.text = task.isCompleted
                ? $"✅ {task.taskTitle}"
                : $"⬜ {task.taskTitle}";
        }
    }
}