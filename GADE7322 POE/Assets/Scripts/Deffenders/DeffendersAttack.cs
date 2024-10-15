using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeffendersAttack : MonoBehaviour
{
    public float attackRange = 5f;
    public int damage = 10;
    public float attackInterval = 1.5f;
    private float lastAttackTime;

    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject healthBarPrefab;
    private GameObject healthBarInstance;
    private Slider healthBarSlider;

    void Start()
    {
        currentHealth = maxHealth;
        GameObject canvas = GameObject.Find("Canvas");
        healthBarInstance = Instantiate(healthBarPrefab, canvas.transform);
        healthBarSlider = healthBarInstance.GetComponentInChildren<Slider>();
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
    }

    void Update()
    {
        if (Time.time >= lastAttackTime + attackInterval)
        {
            DamageEnemies();
            lastAttackTime = Time.time;
        }

        if (healthBarInstance != null)
        {
            Vector3 healthBarPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
            healthBarInstance.transform.position = healthBarPosition;
        }
    }

    void DamageEnemies()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, attackRange);
        List<EnemyHealth> enemiesList = new List<EnemyHealth>();

        foreach (Collider enemyCollider in enemiesInRange)
        {
            if (enemyCollider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemiesList.Add(enemyHealth);
                }
            }
        }

        enemiesList.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position)));

        int enemiesToAttack = Mathf.Min(2, enemiesList.Count);
        for (int i = 0; i < enemiesToAttack; i++)
        {
            enemiesList[i].TakeDamage(damage);
        }
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