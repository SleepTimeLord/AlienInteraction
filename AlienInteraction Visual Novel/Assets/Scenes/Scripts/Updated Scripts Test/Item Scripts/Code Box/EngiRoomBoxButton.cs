using UnityEngine;

public enum EngiButtonType
{
    Number,
    Reset,
    Enter
}

public class EngiRoomBoxButton : MonoBehaviour, IInteractionable
{
    [SerializeField] private EngiButtonType buttonType;
    public EngiRoomCodeBoxManager codeBoxManager;
    public int buttonNumber;
    private ItemManager itemManager;

    void Start()
    {
        itemManager = FindAnyObjectByType<ItemManager>();
    }

    void Update()
    {
        if (codeBoxManager.PuzzleDone || !itemManager.isInPickUpMode)
        {
            gameObject.layer = 6;
        }
        else
        {
            gameObject.layer = 3;

        }
    }

    public void Interact()
    {
        switch (buttonType)
        {
            case EngiButtonType.Number:
                codeBoxManager.InputtedCode(buttonNumber);
                break;

            case EngiButtonType.Reset:
                codeBoxManager.PressedReset();
                break;

            case EngiButtonType.Enter:
                codeBoxManager.PressedEnter();
                break;
        }
    }
}
