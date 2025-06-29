using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class EngiRoomCodeBoxManager : MonoBehaviour, IDataPersistance
{
    public abstract bool PuzzleDone { get; set; }
    protected abstract int[] code1 { get; }
    protected abstract int[] code2 { get; }
    private List<int> inputtedCode = new List<int>();
/*    private bool engineRoomPuzzleDone = false;
*/    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    public bool CompletePuzzle()
    {
        int[] enteredCode = inputtedCode.ToArray();
        return enteredCode.SequenceEqual(code1) || enteredCode.SequenceEqual(code2);
    }

    public void InputtedCode(int codeInputted)
    {
        inputtedCode.Add(codeInputted);
    }

    public virtual void PressedEnter()
    {
        if (CompletePuzzle())
        {
            PuzzleDone = true;
            print("the puzzle is done");
        }
        else 
        {
            PressedReset();
            PuzzleDone = false;
            Debug.Log("reset and make sound");
        }
    }

    public void PressedReset()
    {
        inputtedCode.Clear();
        Debug.Log(inputtedCode.Count);
    }

    public virtual void LoadData(GameData data)
    {
        PuzzleDone = GetPuzzleFromGameData(data);

    }

    public virtual void SaveData(ref GameData data)
    {
/*        data.engineRoomPuzzle1Done = this.engineRoomPuzzleDone;*/

        SetPuzzleDoneInData(ref data, PuzzleDone);
    }

    protected abstract bool GetPuzzleFromGameData(GameData data);
    protected abstract void SetPuzzleDoneInData(ref GameData data, bool value);
}