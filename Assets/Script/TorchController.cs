using UnityEngine;

public class TorchController : MonoBehaviour
{
    [Header("Meşale Ayarları")]
    public KeyCode toggleKey = KeyCode.F; 
    public Light torchLight; 

    [Header("Titreme (Flicker) Ayarları")]
    public float minIntensity = 3f; 
    public float maxIntensity = 6f; 
    public float flickerSpeed = 0.1f;

    private float targetIntensity;
    private float currentIntensity;

    void Start()
    {
        if (torchLight == null)
        {
            torchLight = GetComponent<Light>();
        }
        
        torchLight.enabled = false;
        
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        currentIntensity = minIntensity;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            torchLight.enabled = !torchLight.enabled;
        }

        if (torchLight.enabled)
        {
            currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, flickerSpeed);
            torchLight.intensity = currentIntensity;

            if (Mathf.Abs(currentIntensity - targetIntensity) < 0.1f)
            {
                targetIntensity = Random.Range(minIntensity, maxIntensity);
            }
        }
    }
}