using UnityEngine;

public class TorchController : MonoBehaviour
{
    [Header("Meşale Ayarları")]
    public KeyCode toggleKey = KeyCode.F; // Açma/Kapama tuşu
    public Light torchLight; 

    [Header("Titreme (Flicker) Ayarları")]
    public float minIntensity = 3f; // En düşük parlaklık
    public float maxIntensity = 6f; // En yüksek parlaklık
    public float flickerSpeed = 0.1f; // Ne kadar hızlı titreyeceği

    private float targetIntensity;
    private float currentIntensity;

    void Start()
    {
        // Eğer atanmamışsa ışığı kendinden al
        if (torchLight == null)
        {
            torchLight = GetComponent<Light>();
        }
        
        // Başlangıçta ışığı kapalı yapabiliriz
        torchLight.enabled = false;
        
        // İlk hedef parlaklığı belirle
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        currentIntensity = minIntensity;
    }

    void Update()
    {
        // 1. F tuşu ile Açma/Kapama
        if (Input.GetKeyDown(toggleKey))
        {
            torchLight.enabled = !torchLight.enabled;
        }

        // 2. Eğer ışık açıksa titreme efektini uygula
        if (torchLight.enabled)
        {
            // Mevcut parlaklığı hedefe doğru yumuşakça (Lerp) değiştir
            currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, flickerSpeed);
            torchLight.intensity = currentIntensity;

            // Eğer hedef parlaklığa çok yaklaştıysak, yeni rastgele bir hedef belirle
            if (Mathf.Abs(currentIntensity - targetIntensity) < 0.1f)
            {
                targetIntensity = Random.Range(minIntensity, maxIntensity);
            }
        }
    }
}