using UnityEngine;

public class MouseOrbitCamera : MonoBehaviour
{
    [Header("Hedef ve Mesafe")]
    public Transform target; 
    public float distance = 5.0f; 
    
    [Header("Fare Hassasiyeti")]
    public float mouseSensitivity = 3.0f;

    [Header("Açı Sınırları (Yukarı/Aşağı)")]
    public float minYAngle = -20.0f; 
    public float maxYAngle = 80.0f;  

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        currentX += Input.GetAxis("Mouse X") * mouseSensitivity;
        
        currentY -= Input.GetAxis("Mouse Y") * mouseSensitivity; 

        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        Vector3 position = target.position - (rotation * Vector3.forward * distance);

        transform.position = position;
        transform.rotation = rotation;
    }
}