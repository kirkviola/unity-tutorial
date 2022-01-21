using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] 
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;
    private bool _hasDied = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die()); // Must use start coroutine with IEnumorator -- like async
        }
        
    }

    private bool ShouldDieFromCollision(Collision2D coll)
    {
        Bird bird = coll.gameObject.GetComponent<Bird>();
        if (this._hasDied)
            return false;

        if (bird != null)
            return true;

        if (coll.contacts[0].normal.y < -0.5)
            return true;

        return false;

     
    }
    private IEnumerator Die()
    {
        this._hasDied = true;

        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        this._particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
