using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [HideInInspector] public GameObject enemy;

    public float damageModifier = 1;
    [HideInInspector] public bool inCombat;

    [SerializeField] int damage = 10;
    [SerializeField] int combatTime = 60;
    float combatTimer;
    [SerializeField] float attackTime = 2;
    float attackTimer = 0;

    private void Start()
    {
        if (this.GetComponentInChildren<ObjectCheck>() != null)
        {
            this.GetComponentInChildren<ObjectCheck>().combatSystem = this.GetComponent<CombatSystem>();
        }
    }   
    
    // Update is called once per frame
    void Update()
    {
        if (inCombat && enemy != null)
        {
            attackTimer += Time.deltaTime;
            combatTimer += Time.deltaTime;
            if (attackTimer >= attackTime)
            {
                enemy.GetComponent<HealthSystem>().TakeDamage(damage * damageModifier);
                attackTimer = 0;
            }
        }
    }
}
