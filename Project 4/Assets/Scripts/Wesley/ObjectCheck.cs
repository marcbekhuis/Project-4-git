using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
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

        if (basicMovement.movePosition != null)
        {
            distance = Vector2.Distance(this.transform.parent.position, basicMovement.movePosition.transform.position);

            if (isMovingAway)
            {
                basicMovement.Movement("Up");
            }
            if (distance <= 0.005f && basicMovement.doesMerge)
            {
                this.transform.parent.position = basicMovement.movePosition.transform.position;
                StopMovement();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collission)
    {
        if (collission.transform.parent.CompareTag("Prop"))
        {
            ChangeState();
        }
        else if (collission.transform.parent.gameObject == basicMovement.movePosition && !basicMovement.doesMerge)
        {
            StopMovement();
        }
    }

    void OnTriggerExit2D(Collider2D collission)
    {
        if (collission.transform.parent.CompareTag("Prop"))
        {
            ChangeState();
        }
    }

    void ChangeState()
    {
        isMovingAway = !isMovingAway;
        basicMovement.isConstructed = !basicMovement.isConstructed;
    }

    void StopMovement()
    {
        basicMovement.movePosition = null;
        isMoving = false;
    }
}
