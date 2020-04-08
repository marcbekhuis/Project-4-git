using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    GameObject healthBar;

    public bool isHealthbarActive = false;

    [SerializeField] int health = 100;
    int maxHealth = 0;

    float maxHealthSize;

    int lastDamageWaitTime = 5;
    float lastDamageTimer;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        if (this.transform.Find("HealthBar") != null)
        {
            healthBar = this.transform.Find("HealthBar").gameObject;
            healthBar.GetComponent<SpriteRenderer>().color = Color.green;
            maxHealthSize = healthBar.transform.localScale.x;
            HealthBarState(false);
        }
    }

    private void Update()
    {
        if (isHealthbarActive)
        {
            lastDamageTimer += Time.deltaTime;
            if (lastDamageTimer >= lastDamageWaitTime)
            {
                HealthBarState(false);
            }
        }
    }

    public void TakeDamage(float damageTaken)
    {
        health -= (int)damageTaken;
        HealthBarState(true);

        healthBar.transform.localScale = new Vector3((maxHealthSize / maxHealth) * health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        CheckHealthState();

        if (health <= 0)
        {
            //death
            Destroy(this.gameObject);
        }

        lastDamageTimer = 0;
    }

    void CheckHealthState()
    {
        if (health >= maxHealth - ((maxHealth / 8) * 3))
        {
            healthBar.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (health <= maxHealth - ((maxHealth / 8) * 6))
        {
            healthBar.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            healthBar.GetComponent<SpriteRenderer>().color = new Color(255, 150, 0, 255);
        }
    }

    void HealthBarState(bool state)
    {
        if (healthBar != null)
        {
            isHealthbarActive = state;
            healthBar.SetActive(state);
        }
    }
}
