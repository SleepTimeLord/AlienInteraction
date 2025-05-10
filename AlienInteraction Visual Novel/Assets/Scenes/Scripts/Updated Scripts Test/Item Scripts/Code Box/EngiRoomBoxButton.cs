using UnityEngine;

public enum EngiButtonType
{
    Code1,
    Code2,
    Code3,
    Code4,
    Code5,
    Code6,
    Reset,
    Enter
}

public class EngiRoomBoxButton : MonoBehaviour, IInteractionable
{
    [SerializeField] private EngiButtonType buttonType;
    private EngiRoomCodeBoxManager codeBoxManager;

    void Start()
    {
        codeBoxManager = FindAnyObjectByType<EngiRoomCodeBoxManager>();
    }

    public void Interact()
    {
        switch (buttonType)
        {
            case EngiButtonType.Code1:
            case EngiButtonType.Code2:
            case EngiButtonType.Code3:
            case EngiButtonType.Code4:
            case EngiButtonType.Code5:
            case EngiButtonType.Code6:
                // Parse the enum name “CodeX” to extract the digit
                int code = int.Parse(buttonType.ToString().Replace("Code", ""));
                codeBoxManager.InputtedCode(code);
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
