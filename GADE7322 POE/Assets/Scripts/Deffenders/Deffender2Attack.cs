using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Deffender2Attack : MonoBehaviour
{
    public float damage = 20f;
    public float shootingInterval = 2.0f;
    public float shootingRange = 20f;
    public float colliderRadius = 5f;
    public GameObject healthBarPrefab;
    private GameObject healthBarInstance;
    private Slider healthBarSlider;
    public float maxHealth = 100f;
    private float currentHealth;
    private SphereCollider attackCollider;

    void Start()
    {
        currentHealth = maxHealth;
        GameObject canvas = GameObject.Find("Canvas");
        healthBarInstance = Instantiate(healthBarPrefab, canvas.transform);
        healthBarSlider = healthBarInstance.GetComponentInChildren<Slider>();
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
        attackCollider = gameObject.AddComponent<SphereCollider>();
        attackCollider.isTrigger = true;
        attackCollider.radius = colliderRadius;
        StartCoroutine(DamageEnemies());
    }

    void Update()
    {
        Vector3 healthBarPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
        healthBarInstance.transform.position = healthBarPosition;
    }

    IEnumerator DamageEnemies()
    {
        while (true)
        {
            Damage();
            yield return new WaitForSeconds(shootingInterval);
        }
    }

    void Damage()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, shootingRange);
        foreach (Collider enemyCollider in enemiesInRange)
        {
            if (enemyCollider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                    break;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBarSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            Destroy(healthBarInstance);
            Destroy(gameObject);
        }
    }
}