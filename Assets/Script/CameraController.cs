using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Takip Ayarları")]
    public Transform target; 
    public Vector3 offset; 
    
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        transform.position = smoothedPosition;

    }
}
