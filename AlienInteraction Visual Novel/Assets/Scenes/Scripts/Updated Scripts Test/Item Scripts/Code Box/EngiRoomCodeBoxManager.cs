using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EngiRoomCodeBoxManager : MonoBehaviour, IDataPersistance
{
    private int[] purpleCode1 = {1, 4, 1, 5, 3, 1, 4, 2};
    private int[] purpleCode2 = {3, 1, 4, 2, 1, 4, 1, 5};
    private List<int> inputtedCode = new List<int>();
    private bool engineRoomPuzzle1Done = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private bool CompletePuzzle()
    {
        int[] enteredCode = inputtedCode.ToArray();
        if (enteredCode.SequenceEqual(purpleCode1) || enteredCode.SequenceEqual(purpleCode2))
        {
            Debug.Log("code is right");
            return true;
        }
        else
        {
            Debug.Log("code is wrong");
            return false;
        }
    }

    public void InputtedCode(int codeInputted)
    {
        inputtedCode.Add(codeInputted);
        Debug.Log(codeInputted);
    }

    public void PressedEnter()
    {
        if (CompletePuzzle())
        {
            engineRoomPuzzle1Done = true;
        }
        else 
        {
            PressedReset();
            engineRoomPuzzle1Done = false;
            Debug.Log("reset and make sound");
        }
    }

    public void PressedReset()
    {
        inputtedCode.Clear();
        Debug.Log(inputtedCode.Count);
    }

    public void LoadData(GameData data)
    {
        this.engineRoomPuzzle1Done = data.engineRoomPuzzle1Done;
    }

    public void SaveData(ref GameData data) 
    {
        data.engineRoomPuzzle1Done = this.engineRoomPuzzle1Done;
    }
}