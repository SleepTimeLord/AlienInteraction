using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UI_Image = UnityEngine.UI.Image;

public class Oxygen : MonoBehaviour
{
    public UI_Image oxygenBar;
    public float oxygenAmount = 100f;
    public float oxygenDecreaseRate = 1f;
    private bool isIncreasing = false;

    void Start() 
    {
        StartCoroutine(DecreaseOxygenOvertime());
    }

    void Update()
    {

    }

    // Increases oxygen gradually until reaching the target value.
    public IEnumerator IncreaseOxygenOvertime(float oxygenAdded)
    {
        isIncreasing = true;
        float targetOxygen = Mathf.Clamp(oxygenAmount + oxygenAdded, 0, 100);
        Debug.Log("Target Oxygen: " + targetOxygen);
        while (oxygenAmount < targetOxygen)
        {
            // Increase oxygen by 7 units per second.
            oxygenAmount += 7f * Time.deltaTime;
            oxygenAmount = Mathf.Clamp(oxygenAmount, 0, 100);
            if (oxygenBar != null)
            {
                oxygenBar.fillAmount = oxygenAmount / 100f;
            }
            yield return null;
        }
        Debug.Log("Oxygen Done Healing");
        isIncreasing = false;
        
    }

    // Continuously decreases oxygen, but pauses if IncreaseOxygenOvertime is running.
    public IEnumerator DecreaseOxygenOvertime()
    {
        while (oxygenAmount > 0)
        {
            // Pause oxygen decrease while oxygen is being increased. WaitWhile checks if isIncreasing is true or false even if it's only called once.
            if (isIncreasing)
            {
                yield return new WaitWhile(() => isIncreasing);
            }

            oxygenAmount -= oxygenDecreaseRate * Time.deltaTime;
            oxygenAmount = Mathf.Clamp(oxygenAmount, 0, 100);
            if (oxygenBar != null)
            {
                oxygenBar.fillAmount = oxygenAmount / 100f;
            }
            yield return null;
        }
        Debug.Log("Oxygen depleted!");
    }
}
