using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Zaman Ayarları")]
    public float dayDurationInSeconds = 120f; // Bir tam gün oyunda kaç saniye sürsün? (Örn: 120 saniye = 2 dakika)
    
    [Header("Güneş (Directional Light)")]
    public Transform directionalLight; // Sahnedeki güneş ışığı objesi

    // Zamanın ne kadar hızlı akacağını hesaplayacağımız değişken
    private float timeMultiplier;

    void Start()
    {
        // 360 dereceyi (bir tam tur) belirlediğimiz süreye bölüyoruz
        // Böylece güneşin 1 saniyede kaç derece dönmesi gerektiğini buluyoruz
        timeMultiplier = 360f / dayDurationInSeconds;
    }

    void Update()
    {
        if (directionalLight != null)
        {
            // Güneşi X ekseninde (yukarı/aşağı) belirlenen hızda döndür
            directionalLight.Rotate(Vector3.right * timeMultiplier * Time.deltaTime);
        }
    }
}