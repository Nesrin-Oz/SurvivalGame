using UnityEngine;

public class MouseOrbitCamera : MonoBehaviour
{
    [Header("Hedef ve Mesafe")]
    public Transform target; // Takip edilecek karakter
    public float distance = 5.0f; // Karakter ile kamera arasındaki mesafe
    
    [Header("Fare Hassasiyeti")]
    public float mouseSensitivity = 3.0f; // Dönüş hızı

    [Header("Açı Sınırları (Yukarı/Aşağı)")]
    public float minYAngle = -20.0f; // Kameranın inebileceği en alt açı
    public float maxYAngle = 80.0f;  // Kameranın çıkabileceği en üst açı

    // Farenin mevcut dönüş değerlerini tutacağımız değişkenler
    private float currentX = 0.0f;
    private float currentY = 0.0f;

    void Start()
    {
        // Fare imlecini ekranın ortasına kilitle ve gizle (oyun hissiyatı için)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Farenin hareketlerini al
        currentX += Input.GetAxis("Mouse X") * mouseSensitivity;
        
        // Y eksenini ters çevirmek (Inverted) istersen buradaki eksiyi artı yapabilirsin
        currentY -= Input.GetAxis("Mouse Y") * mouseSensitivity; 

        // 2. Yukarı/Aşağı bakış açısını sınırla (Kamera takla atmasın veya yerin dibine girmesin)
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);

        // 3. Rotasyonu (Dönüşü) hesapla
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // 4. Pozisyonu hesapla: Hedefin pozisyonundan, kameranın baktığı yönün tersine 'distance' kadar git
        Vector3 position = target.position - (rotation * Vector3.forward * distance);

        // 5. Kameranın yeni pozisyonunu ve dönüşünü uygula
        transform.position = position;
        transform.rotation = rotation;
    }
}