using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FileDataHandler
{
    private string dataDirPath = " ";

    private string dataFileName = " ";

    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "word";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load()
    {
        // use Path.Combine to account for different OS's having different path sparators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // load the serialized data from the file
                string dataToLoad = " ";
                // we use FileMode.Open because you want to access the file
                using (FileStream stream = new FileStream (fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader (stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // optionally decrypt the data
                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // deserialize the data from Json back into the C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data) 
    {
        // use Path.Combine to account for different OS's having different path sparators
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            // this creates the directory file if the directory doesn't already exists
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize (convert) the C# game data object into Json string
            string dataToStore = JsonUtility.ToJson(data, true);

            // optionally encrypt the data
            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            // write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream)) 
                { 
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            // this is to log if errors occur
            Debug.LogError("Error occured who trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    // uses XOR encryption to encrypt data
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}