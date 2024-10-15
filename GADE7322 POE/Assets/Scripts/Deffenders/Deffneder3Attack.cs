using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deffneder3Attack : MonoBehaviour
{
    public float damage = 10f;
    public float attackInterval = 1.5f;
    public float attackRange = 5f;
    public float colliderRadius = 2f;
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
            yield return new WaitForSeconds(attackInterval);
        }
    }

    void Damage()
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
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