using UnityEngine;

public class CodeBoxTwo : EngiRoomCodeBoxManager
{
    private bool puzzleDone;
    public PowerBoxPuzzle enablePuzzle;
    public override bool PuzzleDone 
    { 
        get => puzzleDone; 
        set => puzzleDone = value; 
    }

    protected override int[] code1 => new int[] { 1, 4, 1, 5, 1, 6, 2, 1 };

    protected override int[] code2 => new int[] { 1, 2, 6, 1, 5, 1, 4, 1 };

    protected override bool GetPuzzleFromGameData(GameData data)
    {
        return data.engineRoomPuzzle2Done;
    }

    protected override void SetPuzzleDoneInData(ref GameData data, bool value)
    {
        data.engineRoomPuzzle2Done = value;
    }

    private void Update()
    {
        if (puzzleDone)
        {
            print("puzzleTwoIsDone its time to activate next puzzle and deactivate this one");
            this.enabled = false;
        }
    }

    public override void PressedEnter()
    {
        if (CompletePuzzle())
        {
            PuzzleDone = true;
            print("the puzzle is ONE done");
        }
        else
        {
            PressedReset();
            PuzzleDone = false;
            Debug.Log("reset and make sound ONE");
        }
    }
}
