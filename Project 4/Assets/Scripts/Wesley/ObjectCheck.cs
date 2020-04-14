using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    [HideInInspector] public CombatSystem combatSystem;
    BasicMovement basicMovement;

    [HideInInspector]public bool isMovingAway = false;
    [HideInInspector]public bool isMoving = false;

    float distance = 5000;

    private void Start()
    {
        basicMovement = this.transform.parent.GetComponent<BasicMovement>();
    }

    private void Update()
    {
        //Checks if the object is currently moving to a new position
        if (basicMovement.destination != Vector2.zero)
        {
            distance = Vector2.Distance(this.transform.parent.position, basicMovement.destination);

            //Moves the object up if it runs into anything
            if (isMovingAway)
            {
                basicMovement.Movement("Up");
            }
            //Makes the object stop when it has merged inside the position if doesMerge is enabled
            if (distance <= 0.005f && basicMovement.doesMerge)
            {
                this.transform.parent.position = basicMovement.destination;
                StopMovement();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collission)
    {
        if (collission.transform.parent.CompareTag("Prop"))
        {
            ChangeState(true);
        }
        //else if (collission.transform.parent.gameObject == basicMovement.movePosition && !basicMovement.doesMerge)
        //{
        //    StopMovement();
        //}

        if (collission.transform.parent.CompareTag("Hostile") && combatSystem != null)
        {
            combatSystem.enemy = collission.transform.parent.gameObject;

            //Temp
            this.transform.parent.GetComponent<CombatSystem>().inCombat = true;
        }
    }

    void OnTriggerExit2D(Collider2D collission)
    {
        if (collission.transform.parent.CompareTag("Prop"))
        {
            ChangeState(false);
        }
    }

    void ChangeState(bool state)
    {
        isMovingAway = state;
        basicMovement.isConstructed = state;
    }

    void StopMovement()
    {
        //basicMovement.movePosition = null;
        isMoving = false;
    }
}
