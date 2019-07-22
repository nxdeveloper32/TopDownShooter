using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour,iDamageable {
    public float StartingHealth;
    protected float health;
    protected bool dead;
    public event System.Action onDeath;
    protected virtual void Start()
    {
        health = StartingHealth;
    }
    public void TakeHit(float damage,RaycastHit hit)
    {
        health -= damage;
        if(health <= 0 && !dead)
        {
            Die();
        }
    }
    protected void Die()
    {
        dead = true;
        if(onDeath!= null)
        {
            onDeath();
        }
        Destroy(gameObject);
    }
}

