using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FlyMove
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Egg"))
        {
            LifeCheck.Life--;
            Debug.Log(LifeCheck.Life);
        }
    }
}
