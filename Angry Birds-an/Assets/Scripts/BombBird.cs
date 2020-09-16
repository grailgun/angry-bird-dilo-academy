using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBird : Bird
{
    public float blastRadius = 4f;
    public float bombDamage;
    public bool hasBlown = false;

    public GameObject explosionEffect;

    private void Blast() 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Pig"))
            {
                Enemy enemy = col.gameObject.GetComponent<Enemy>();

                if (enemy)
                {
                    Vector2 closestPoint = col.ClosestPoint(transform.position);
                    float distance = Vector2.Distance(closestPoint, (Vector2)transform.position);

                    float damagePercent = Mathf.InverseLerp(blastRadius, 0, distance);

                    enemy.TakeDamage(damagePercent * bombDamage);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Blast();
        gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
