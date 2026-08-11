using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Zaman Ayarları")]
    public float dayDurationInSeconds = 120f; 

    [Header("Güneş (Directional Light)")]
    public Transform directionalLight;

    private float timeMultiplier;

    void Start()
    {
        timeMultiplier = 360f / dayDurationInSeconds;
    }

    void Update()
    {
        if (directionalLight != null)
        {
            directionalLight.Rotate(Vector3.right * timeMultiplier * Time.deltaTime);
        }
    }
}