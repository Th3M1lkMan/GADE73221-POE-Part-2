using UnityEngine;
using UnityEngine.UI;

public class DeffenderBase : MonoBehaviour
{
    public int health = 100;
    public int damage = 10;
    public GameObject healthBarPrefab;
    private GameObject healthBarInstance;
    private Slider healthBarSlider;

    void Start()
    {
        
        GameObject canvas = GameObject.Find("Canvas");
        healthBarInstance = Instantiate(healthBarPrefab, canvas.transform);
        healthBarSlider = healthBarInstance.GetComponentInChildren<Slider>();
        healthBarSlider.maxValue = health;
        healthBarSlider.value = health;
    }

    void Update()
    {
        // Update health bar position
        Vector3 healthBarPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
        healthBarInstance.transform.position = healthBarPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Damage(damage);
            Debug.Log("enemyHit");
        }
    }

    void Damage(int amount)
    {
        health -= amount;
        healthBarSlider.value = health; 

        if (health <= 0)
        {
            Time.timeScale = 0f;
            Destroy(healthBarInstance); 
            Destroy(gameObject);
        }
    }
}