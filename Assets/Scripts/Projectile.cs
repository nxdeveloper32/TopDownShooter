using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    float speed = 10;
    float damage = 1;
    public LayerMask collisionMask;
    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
	void Update () {
        float moveDistance = speed * Time.deltaTime;
        checkCollisions(moveDistance);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}
    void checkCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            onHitObject(hit);
        }
    }
    void onHitObject(RaycastHit hit)
    {
        iDamageable damageableObject = hit.collider.GetComponent<iDamageable>();
        if(damageableObject != null)
        {
            damageableObject.TakeHit(damage, hit);
        }
        Destroy(gameObject);
    }
}
