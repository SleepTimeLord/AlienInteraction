using UnityEngine;
using UnityEngine.UI;

public class Detection : MonoBehaviour
{
    public GameObject playerCharacter;
    public Image pulseImage;
    [SerializeField] private bool isActive;

    [Header("Pulse Settings")]
    [SerializeField] private float maxPulseDistance = 15f;
    [SerializeField] private float minPulseInterval = 0.5f;
    [SerializeField] private float maxPulseInterval = 2f;
    [SerializeField] private float pulseGrowthSpeed = 2f;
    [SerializeField] private float maxPulseSize = 3f;

    private float closestDistance;
    private float pulseProgress;
    private float currentPulseInterval;
    private float smoothedDistance;
    private Color originalColor;

    void Start()
    {
        originalColor = pulseImage.color;
        pulseProgress = 0f;
    }

    void Update()
    {
        if (isActive)
        {
            FindClosestDistance();
            UpdatePulseEffect();
        }
        else
        {
            originalColor = pulseImage.color;
            pulseImage.transform.localScale = Vector3.zero;
            pulseProgress = 0f;
        }
    }

    private void FindClosestDistance()
    {
        //starts at infinity because it is the largest number
        closestDistance = Mathf.Infinity;
        // it's going to assume that it's a GameObject list
        var findableObjects = GameObject.FindGameObjectsWithTag("Findable");

        foreach (var obj in findableObjects)
        {
            if (!obj.activeInHierarchy) continue;

            float currentDistance = Vector3.Distance(
                playerCharacter.transform.position,
                obj.transform.position
            );

            // this updates to the lesser of the past closest distance and the new distance. (closest distance only updates if the current number is smaller than the past number)
            closestDistance = Mathf.Min(closestDistance, currentDistance);
        }

        // Smooth distance changes
        smoothedDistance = Mathf.Lerp(smoothedDistance, closestDistance, Time.deltaTime * 5f);
    }

    private void UpdatePulseEffect()
    {
        if (pulseImage == null) return;

        // Calculate pulse interval based on distance
        float distanceFactor = Mathf.Clamp01(smoothedDistance / maxPulseDistance);
        currentPulseInterval = Mathf.Lerp(minPulseInterval, maxPulseInterval, distanceFactor);

        // Update pulse progress
        pulseProgress += Time.deltaTime / currentPulseInterval;

        if (pulseProgress >= 1f)
        {
            pulseProgress = 0f;
            pulseImage.transform.localScale = Vector3.zero;
            pulseImage.color = originalColor;
        }

        // Calculate current scale and alpha
        float currentScale = Mathf.Lerp(0f, maxPulseSize, pulseProgress * pulseGrowthSpeed);
        float currentAlpha = Mathf.Lerp(1f, 0f, pulseProgress);

        // Apply effects
        pulseImage.transform.localScale = Vector3.one * currentScale;
        pulseImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, currentAlpha);
    }
}
