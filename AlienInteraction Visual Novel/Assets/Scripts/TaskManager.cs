using UnityEngine;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    private static TaskManager instance;
    private HashSet<string> completedTasks = new HashSet<string>(); // Track completed tasks

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensure it persists across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent multiple instances
        }
    }

    // Mark a task as completed
    public static void CompleteTask(string taskID)
    {
        if (instance != null)
        {
            instance.completedTasks.Add(taskID);
        }
    }

    // Check if a task is completed
    public static bool IsTaskCompleted(string taskID)
    {
        return instance != null && instance.completedTasks.Contains(taskID);
    }
}

