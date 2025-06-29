using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;


    private GameData gameData;

    // gets all scripts using the dataPersistance interface
    private List<IDataPersistance> dataPersistanceObjects;
    private FileDataHandler dataHandler;
    // can get the instance publicly but can only be edited privately
    public static DataPersistanceManager instance {  get; private set; }

    private void Awake()
    {
        if (instance != null) 
        {
            Debug.LogError("There are more than two data persistance manager in this scene");
        }
        instance = this;
    }

    private void Start()
    {
        // this directs data and saves data
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        Debug.Log(Application.persistentDataPath);
        // loads game on start. 
        // NOTE: change this when we get start and load UI.
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void ResetGame()
    {
        // Start a new GameData object
        this.gameData = new GameData();

        // Overwrite any existing save file with the new (empty) data
        dataHandler.Save(this.gameData);

        // Push this new data to all other scripts (so they reset as well)
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }

        Debug.Log("Game data has been reset.");
    }

    public GameData GetGameData()
    {
        return this.gameData;
    }

    public void LoadGame()
    {
        // Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        // load new game if no data was saved.
        if (this.gameData == null)
        {
            Debug.Log("No recent game saves. Loading new game.");
            NewGame();
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // pass the data to other scripts so they can update it
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData);
        }

        // save that data to a file using the data handler
        dataHandler.Save(gameData);

    }

    private void OnApplicationQuit()
    {
        // saves game when player exits game.
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> datapersistanceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IDataPersistance>();
        return new List<IDataPersistance>(datapersistanceObjects);
    }
}