using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get; private set; }

    [SerializeField]
    public HashSet<string> completedProgressIDs = new HashSet<string>();
    public event Action<string> OnProgressUpdated; // Notifies when progress is updated
    public string currentStoryPart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        LoadProgress(); // Load progress at the start
    }

    public bool IsProgressCompleted(string progressID)
    {
        return completedProgressIDs.Contains(progressID);
    }

    public void MarkProgressCompleted(string progressID)
    {
        if (!string.IsNullOrEmpty(progressID) && !completedProgressIDs.Contains(progressID))
        {
            completedProgressIDs.Add(progressID);

            // Notify other scripts
            if (OnProgressUpdated != null)
            {
                OnProgressUpdated(progressID);
            }

            SaveProgress(); // Save progress immediately
        }
    }

    public void SaveProgress()
    {
        Debug.Log("data saved");
        string path = Application.persistentDataPath + "/storyProgress.json";
        string data = JsonUtility.ToJson(this);

        File.WriteAllText(path, data); // Save progress to a file
    }

    public void LoadProgress()
    {
        string path = Application.persistentDataPath + "/storyProgress.json";

        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(data, this);
        }
    }
}

