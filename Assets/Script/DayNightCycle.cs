using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Zaman Ayarları")]
    public float dayDurationInSeconds = 30f; 

    [Header("Güneş (Directional Light)")]
    public Transform directionalLight;

    [Header("Gökyüzü Renk Geçişi (Tint)")]
    public Gradient gokyuzuRenkleri; 

    private float timeMultiplier;
    private Material dinamikSkybox;

    void Start()
    {
        timeMultiplier = 360f / dayDurationInSeconds;
        
        dinamikSkybox = new Material(RenderSettings.skybox);
        RenderSettings.skybox = dinamikSkybox;
    }

    void Update()
    {
        if (directionalLight != null)
        {
            directionalLight.Rotate(Vector3.right * timeMultiplier * Time.deltaTime);

            float gununYuzdesi = directionalLight.eulerAngles.x / 360f;

           Color anlikRenk = gokyuzuRenkleri.Evaluate(gununYuzdesi);

            dinamikSkybox.SetColor("_Tint", anlikRenk);
        }
    }
}