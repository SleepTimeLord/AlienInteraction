using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TaskManager : MonoBehaviour
{
    private static TaskManager instance;
    private HashSet<string> completedTasks = new HashSet<string>(); // Track completed tasks
    private List<string> activeTasks = new List<string>();          // Track all active tasks

    [Header("UI Elements")]
    [SerializeField] private List<TextMeshProUGUI> taskTexts; // Text elements for displaying tasks
    [SerializeField] private GameObject taskUI;              // Parent UI GameObject

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    // Add a new task
    public static void AddTask(string taskID)
    {
        if (instance != null && !instance.activeTasks.Contains(taskID))
        {
            instance.activeTasks.Add(taskID);
            instance.UpdateTaskUI();
        }
    }

    // Mark a task as completed
    public static void CompleteTask(string taskID)
    {
        if (instance != null)
        {
            instance.completedTasks.Add(taskID);
            instance.activeTasks.Remove(taskID);
            instance.UpdateTaskUI();
        }
    }

    // Check if a task is completed
    public static bool IsTaskCompleted(string taskID)
    {
        return instance != null && instance.completedTasks.Contains(taskID);
    }

    // Reset all tasks
    public static void ResetAllTasks()
    {
        if (instance != null)
        {
            instance.activeTasks.Clear();
            instance.completedTasks.Clear();
            instance.UpdateTaskUI();
        }
    }

    // Update the task UI
    private void UpdateTaskUI()
    {
        if (taskTexts == null || taskTexts.Count == 0 || taskUI == null) return;

        // Update task text elements
        for (int i = 0; i < taskTexts.Count; i++)
        {
            if (i < activeTasks.Count)
            {
                taskTexts[i].text = activeTasks[i];
                taskTexts[i].fontStyle = FontStyles.Normal; // Ensure normal font style for active tasks
                taskTexts[i].gameObject.SetActive(true);
            }
            else
            {
                taskTexts[i].gameObject.SetActive(false);
            }
        }

        // Hide the UI if no tasks are active
        taskUI.SetActive(activeTasks.Count > 0);
    }

    // Get the list of active tasks
    public static List<string> GetActiveTasks()
    {
        return instance != null ? new List<string>(instance.activeTasks) : new List<string>();
    }

    // Get the list of completed tasks
    public static List<string> GetCompletedTasks()
    {
        return instance != null ? new List<string>(instance.completedTasks) : new List<string>();
    }
}
