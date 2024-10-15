using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject healthBarPrefab;
    private GameObject healthBarInstance;
    private Slider healthBarSlider;

    void Start()
    {
        currentHealth = maxHealth;
        SetupHealthBar();
    }

    void SetupHealthBar()
    {
        healthBarInstance = Instantiate(healthBarPrefab);
        healthBarSlider = healthBarInstance.GetComponentInChildren<Slider>();
        healthBarInstance.transform.SetParent(GameObject.Find("Canvas").transform);
        healthBarInstance.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBarSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            DestroyHealthBar();
            Destroy(gameObject);
        }
    }

    void DestroyHealthBar()
    {
        if (healthBarInstance != null)
        {
            Destroy(healthBarInstance);
        }
    }

    void Update()
    {
        if (healthBarInstance != null)
        {
            healthBarInstance.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
        }
    }
}