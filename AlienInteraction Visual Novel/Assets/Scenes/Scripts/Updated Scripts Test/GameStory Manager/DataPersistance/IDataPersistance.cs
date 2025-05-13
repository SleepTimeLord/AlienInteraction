using UnityEngine;

public interface IDataPersistance 
{
    // this reads the data.
    void LoadData(GameData data);

    // this modifies the data by taking a reference of a gamedata.
    void SaveData(ref GameData data);
}
