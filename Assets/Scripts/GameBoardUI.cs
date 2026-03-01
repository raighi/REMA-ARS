using System.Linq;
using TMPro;
using UnityEngine;

namespace TaskSystem
{
    public class GameBoardUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI progressText;
        [SerializeField] private Transform taskListContainer;
        [SerializeField] private GameObject taskRowPrefab;

        public void Refresh()
        {
            // Vider la liste existante
            foreach (Transform child in taskListContainer)
                Destroy(child.gameObject);

            var tasks = TaskManager.Instance.GetAllTasks();

            // Recréer une ligne par tâche
            foreach (var task in tasks)
            {
                var row = Instantiate(taskRowPrefab, taskListContainer);
                row.GetComponent<TaskRowUI>().Setup(task);
            }

            // Mettre à jour le compteur
            int completed = tasks.Count(t => t.isCompleted);
            progressText.text = $"{completed} / {tasks.Count} tâches faites";
        }
    }
}