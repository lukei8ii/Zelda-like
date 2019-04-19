using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Pot>().Smash();
        }

        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            var hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    var enemy = other.GetComponent<Enemy>();

                    enemy.currentState = EnemyState.stagger;
                    enemy.Knock(hit, knockTime, damage);
                } else if (other.gameObject.CompareTag("Player"))
                {
                    var player = other.GetComponent<PlayerMovement>();

                    if (player.currentState != PlayerState.stagger)
                    {
                        player.currentState = PlayerState.stagger;
                        player.Knock(knockTime, damage);
                    }
                }
                
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
            }
        }
    }
}
