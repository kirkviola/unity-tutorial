using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            Die();
        }
        
        
    }

    private bool ShouldDieFromCollision(Collision2D coll)
    {
        Bird bird = coll.gameObject.GetComponent<Bird>();

        if (bird != null)
            return true;

        if (coll.contacts[0].normal.y < -0.5)
            return true;

        return false;

     
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
