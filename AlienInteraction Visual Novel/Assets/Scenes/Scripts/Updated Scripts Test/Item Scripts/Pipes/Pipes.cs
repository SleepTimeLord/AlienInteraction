using UnityEngine;
using UnityEngine.Rendering;

public enum PipeType
{
    PowerBox,
    Door1,
    Door2,
    None
}
public class Pipes : MonoBehaviour, IInteractionable
{
    private Transform tr;
    private PipesManager pipesManager;
    public PipeType pipeType = PipeType.None;
    float[] rotation = { 0, 90, 180, 270 };
    public float[] correctRotationPower;
    public float[] correctRotationDoor1;
    public float[] correctRotationDoor2;

    public bool isCorrectlyPlacedPower = false;
    public bool isCorrectlyPlacedDoor1 = false;
    public bool isCorrectlyPlacedDoor2 = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tr = GetComponent<Transform>();
        pipesManager = FindAnyObjectByType<PipesManager>();

        int rand = Random.Range(0, rotation.Length);
        tr.eulerAngles = new Vector3 (0, rotation[rand], 90);

        switch (pipeType)
        {
            case PipeType.PowerBox:
                isCorrectlyPlacedPower = SetCorrectRotation(correctRotationPower);
                break;
            case PipeType.Door1:
                isCorrectlyPlacedDoor1 = SetCorrectRotation(correctRotationDoor1);
                break;
            case PipeType.Door2:
                isCorrectlyPlacedDoor2 = SetCorrectRotation(correctRotationDoor2);
                break;
        }
        pipesManager.CheckIfFinishedPipePuzzle();
    }

    public void Interact()
    {
        tr.Rotate(new Vector3(90, 0, 0));

        switch (pipeType)
        {
            case PipeType.PowerBox:
                isCorrectlyPlacedPower = SetCorrectRotation(correctRotationPower);
                break;
            case PipeType.Door1:
                isCorrectlyPlacedDoor1 = SetCorrectRotation(correctRotationDoor1);
                break;
            case PipeType.Door2:
                isCorrectlyPlacedDoor2 = SetCorrectRotation(correctRotationDoor2);
                break;

        }
        pipesManager.CheckIfFinishedPipePuzzle();

    }

    private bool SetCorrectRotation(float[] correctRotation)
    {
        float currentY = transform.eulerAngles.y;
        foreach (float rot in correctRotation)
        {
            if (Mathf.Approximately(currentY, rot))
                return true;
        }
        return false;
    }
}
