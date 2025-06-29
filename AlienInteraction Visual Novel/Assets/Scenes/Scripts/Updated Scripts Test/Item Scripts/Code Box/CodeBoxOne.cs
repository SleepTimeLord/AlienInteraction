using UnityEngine;

public class CodeBoxOne : EngiRoomCodeBoxManager
{
    protected override int[] code1 => new int[] { 1, 4, 1, 5, 3, 1, 4, 2 };

    protected override int[] code2 => new int[] { 3, 1, 4, 2, 1, 4, 1, 5 };

    private bool puzzleDone;

    public PowerBoxPuzzle test1;

    private void Start()
    {
    }
    public override bool PuzzleDone 
    { 
        // gets the boolean and sets it as the value for the puzzle
        get => puzzleDone;
        set => puzzleDone = value;
    }
    protected override bool GetPuzzleFromGameData(GameData data)
    {
        return data.engineRoomPuzzle1Done;
    }

    protected override void SetPuzzleDoneInData(ref GameData data, bool value)
    {
        data.engineRoomPuzzle1Done = value;
    }

    private void Update()
    {
        if (puzzleDone)
        {
            print("puzzleOneIsDone its time to activate next puzzle and deactivate this one");

            test1.enabled = true;
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
