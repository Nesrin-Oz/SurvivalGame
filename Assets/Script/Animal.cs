using UnityEngine;

public class Animal : MonoBehaviour
{
    [Header("Hayvan Ayarları")]
    public float health = 50f; // Hayvanın canı
    public GameObject meatPrefab; // Öldüğünde yere düşüreceği et

    // Dışarıdan (Sapan taşından) çağrılacak hasar alma fonksiyonu
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Hayvan vuruldu! Kalan can: " + health);

        // Canı sıfıra veya altına düştüyse öl
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Hayvan öldü!");

        // 1. Eğer et prefab'ı atandıysa, hayvanın tam bulunduğu yere (transform.position) o eti yerleştir
        if (meatPrefab != null)
        {
            Instantiate(meatPrefab, transform.position, Quaternion.identity);
        }

        // 2. Hayvan objesini sahneden tamamen sil
        Destroy(gameObject);
    }
}