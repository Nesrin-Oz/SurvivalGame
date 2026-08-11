using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Takip Ayarları")]
    public Transform target; // Takip edilecek karakter (Player)
    public Vector3 offset; // Kamera ile karakter arasındaki sabit mesafe
    
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f; // Kameranın yumuşaklık (gecikme) değeri

    void LateUpdate()
    {
        // Eğer hedef yoksa hata vermemesi için kontrol ediyoruz
        if (target == null) return;

        // Kameranın gitmesi gereken yeni pozisyonu hesapla (Karakterin konumu + aradaki mesafe)
        Vector3 desiredPosition = target.position + offset;
        
        // Vector3.Lerp ile mevcut konumdan yeni konuma yumuşak bir geçiş yap
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // Kameranın pozisyonunu güncelle
        transform.position = smoothedPosition;

        // Opsiyonel: Kameranın her zaman karaktere dönük olmasını istersen aşağıdaki satırın başındaki yorum işaretlerini (//) kaldırabilirsin.
        // transform.LookAt(target);
    }
}
