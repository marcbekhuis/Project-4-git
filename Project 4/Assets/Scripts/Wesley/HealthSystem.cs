using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] int health = 100;
    int maxHealth = 0;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damageTaken)
    {
        health -= (int)damageTaken;

        if (health <= 0)
        {
            //death
            Destroy(this.gameObject);
        }
    }
}
