using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Events/GameEvent")]
public class GameEvent : ScriptableObject
{
    public UnityEvent OnEventRaised;

    public void Raise()
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke();
    }
}