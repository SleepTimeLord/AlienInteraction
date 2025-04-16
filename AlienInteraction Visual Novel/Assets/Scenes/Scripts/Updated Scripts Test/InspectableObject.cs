using UnityEngine;

[CreateAssetMenu(fileName = "InspectableObject", menuName = "Inspectables")]
public class InspectableObject : ScriptableObject
{
    public GameObject holdable;
    public GameObject inspectable;
}
