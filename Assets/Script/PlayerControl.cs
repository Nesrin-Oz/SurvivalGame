using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour
{
    [Header("Hareket")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float rotationSpeed = 10f; 
    
    [Header("Fizik")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("Kamera Referansı")]
    public Transform cameraTransform;

    [Header("Can Ayarları")]
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthSlider; 

    [Header("Açlık Ayarları")]
    public float maxHunger = 100f;
    public float currentHunger;
    public Slider hungerSlider; 
    public float hungerDepletionRate = 2f; 
    public float starvationDamage = 5f; 

    [Header("Susuzluk Ayarları")]
    public float maxThirst = 100f;
    public float currentThirst;
    public Slider thirstSlider; 
    public float thirstDepletionRate = 2f; 

    [Header("Envanter Verileri")]
    public int foodCount = 0; 
    public int waterCount = 0; 

    [Header("Durum Metinleri (TMP)")]
    public TextMeshProUGUI healtText;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI thirstText;

    [Header("Envanter Arayüzü (UI)")]
    public TextMeshProUGUI foodText; 
    public TextMeshProUGUI waterText; 

    [Header("Etkileşim (SphereCast)")]
    public float interactRange = 3f; 
    public float interactRadius = 0.5f; 
    public KeyCode interactKey = KeyCode.E; 
    public LayerMask interactableLayer;

    [Header("Efektler (Juice)")]
    public Image damageScreen; 
    public float flashSpeed = 5f; 
    public float pulseSpeed = 2f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.5f); 
    private bool isTakingDamage = false;

    [Header("Sesler (Audio)")]
    public AudioSource audioSource; 
    
    public AudioClip yemeSesi; 
    [Range(0f, 1f)] public float yemeSesiSiddeti; 
    public AudioClip icmeSesi;
    [Range(0f, 1f)] public float icmeSesiSiddeti; 
    public AudioClip hasarAlmaSesi; 
    [Range(0f, 1f)] public float hasarSesiSiddeti = 0.5f; 
    
    [Header("Hasar Sesi Zamanlayıcısı")]
    public float hasarSesiBeklemeSuresi = 1.5f; 
    private float sonHasarSesiZamani = -10f; 

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float jumpVelocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;

        healthSlider.maxValue = maxHealth;
        hungerSlider.maxValue = maxHunger;
        thirstSlider.maxValue = maxThirst;
        
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        UpdateInventoryUI(); 
        UpdateStatUI();
    }

    void Update()
    {
        HandleGroundCheck();
        HandleMovement();
        HandleJump();
        ApplyGravity();
        
        HandleInteraction();
        HandleInventoryUsage();
        HandleDamageScreen(); 

        if (currentHunger > 0)
        {
            currentHunger -= hungerDepletionRate * Time.deltaTime;
        }
        else
        {
            currentHunger = 0; 
            TakeDamage(starvationDamage * Time.deltaTime); 
        }

        if (currentThirst > 0)
        {
            currentThirst -= thirstDepletionRate * Time.deltaTime;
        }
        else
        {
            currentThirst = 0; 
            TakeDamage(starvationDamage * Time.deltaTime); 
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        healthSlider.value = currentHealth;
        hungerSlider.value = currentHunger;
        thirstSlider.value = currentThirst;

        UpdateStatUI();
    }

    private void HandleDamageScreen()
    {
        if (damageScreen != null)
        {
            if (currentHunger <= 0 || currentThirst <= 0 || currentHealth <= (maxHealth * 0.3f))
            {
                float pulseAlpha = Mathf.PingPong(Time.time * flashSpeed * pulseSpeed , flashColor.a);
                damageScreen.color = new Color(flashColor.r, flashColor.g, flashColor.b, pulseAlpha);
                
                isTakingDamage = false;
            }
            else if (isTakingDamage)
            {
                damageScreen.color = flashColor;
                isTakingDamage = false;
            }
            else
            {
                damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, flashSpeed * Time.deltaTime);
            }
        }
    }

    private void UpdateStatUI()
    {
        if (healtText != null) 
            healtText.text = currentHealth.ToString("0") + " / " + maxHealth;
            
        if (hungerText != null) 
            hungerText.text = currentHunger.ToString("0") + " / " + maxHunger;
            
        if (thirstText != null) 
            thirstText.text = currentThirst.ToString("0") + " / " + maxThirst;
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray.origin, interactRadius, ray.direction, out hit, interactRange, interactableLayer))
            {
                if (hit.collider.CompareTag("Food"))
                {
                    foodCount++; 
                    Destroy(hit.collider.gameObject); 
                    UpdateInventoryUI(); 
                }
                else if (hit.collider.CompareTag("Water"))
                {
                    waterCount++; 
                    Destroy(hit.collider.gameObject); 
                    UpdateInventoryUI();
                }
            }
        }
    }

    private void HandleInventoryUsage()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (foodCount > 0)
            {
                foodCount--; 
                EatFood(20f); 
                UpdateInventoryUI(); 
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (waterCount > 0)
            {
                waterCount--; 
                WaterDrink(20f); 
                UpdateInventoryUI(); 
            }
        }
    }

    private void UpdateInventoryUI()
    {
        if(foodText != null) foodText.text = "Yemek: " + foodCount;
        if(waterText != null) waterText.text = "Su: " + waterCount;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        isTakingDamage = true; 
        
        if (audioSource != null && hasarAlmaSesi != null)
        {
            if (Time.time >= sonHasarSesiZamani + hasarSesiBeklemeSuresi)
            {
                audioSource.PlayOneShot(hasarAlmaSesi, hasarSesiSiddeti);
                sonHasarSesiZamani = Time.time; 
            }
        }
    }

    public void EatFood(float nutritionValue)
    {
        currentHunger += nutritionValue;
        if (currentHunger > maxHunger) currentHunger = maxHunger;
        
        if (audioSource != null && yemeSesi != null)
        {
            audioSource.PlayOneShot(yemeSesi, yemeSesiSiddeti); 
        }
    }

    public void WaterDrink(float nutritionValue)
    {
        currentThirst += nutritionValue;
        if (currentThirst > maxThirst) currentThirst = maxThirst;
        
        if (audioSource != null && icmeSesi != null)
        {
            audioSource.PlayOneShot(icmeSesi, icmeSesiSiddeti); 
        }
    }

    void Die()
    {
        Debug.Log("Karakter öldü!");
    }

    private void HandleGroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        
        camForward.y = 0;
        camRight.y = 0;
        
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = (camForward * vertical + camRight * horizontal).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            controller.Move(moveSpeed * Time.deltaTime * moveDirection);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = jumpVelocity;
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}