using Newtonsoft.Json.Bson;
using UnityEngine;

public class PipesManager : MonoBehaviour
{
    public Pipes[] powerBoxPipes;
    public Pipes[] doorOnePipes;
    public Pipes[] doorTwoPipes;

    public bool powerBoxActive = false;
    public bool doorOneActive = false;
    public bool doorTwoActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckIfFinishedPipePuzzle()
    {
        powerBoxActive = FinishedPipePuzzle(powerBoxPipes);
        //doorOneActive = FinishedPipePuzzle(doorOnePipes);
        //doorTwoActive = FinishedPipePuzzle(doorTwoPipes);
    }

    private bool FinishedPipePuzzle(Pipes[] puzzleList)
    {
        foreach (Pipes pipe in puzzleList)
        {
            if (!IsPipeCorrect(pipe))
                return false;
        }
        return true;
    }
    private bool IsPipeCorrect(Pipes pipe)
    {
        return pipe.pipeType switch
        {
            PipeType.PowerBox => pipe.isCorrectlyPlacedPower,
            PipeType.Door1 => pipe.isCorrectlyPlacedDoor1,
            PipeType.Door2 => pipe.isCorrectlyPlacedDoor2,
            _ => false
        };
    }
}
