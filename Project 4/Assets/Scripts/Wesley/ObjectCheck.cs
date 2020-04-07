using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    BasicMovement basicMovement;

    [HideInInspector]public bool isMovingAway = false;

    private void Start()
    {
        basicMovement = this.transform.parent.GetComponent<BasicMovement>();
    }

    private void Update()
    {
        if (isMovingAway)
        {
            basicMovement.Movement("Up");
        }
    }

    void OnTriggerEnter2D(Collider2D collission)
    {
        if (collission.transform.parent.CompareTag("Prop"))
        {
            ChangeState();
        }
        else if (collission.transform.parent.gameObject == basicMovement.movePosition)
        {
            basicMovement.movePosition = null;
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
}
