using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed;

    public int damage;

    //public float explosionRadius;
    //public GameObject impactEffect;

    public string targetTag;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        //GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(effectIns, 2f);

        //if (explosionRadius > 0f)
        //{
        //    Explode();
        //}
        //else
        //{
        Damage(target);
        //}

        Destroy(gameObject);
    }

    //private void Explode()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
    //    foreach (var collider in colliders)
    //    {
    //        if (collider.tag == targetTag)
    //        {
    //            Damage(collider.transform);
    //        }
    //    }
    //}

    void Damage(Transform target)
    {
        if(targetTag=="Player")
        {
            PlayerController p = target.GetComponent<PlayerController>();
            if (p != null)
            {
                p.TakeDamage(damage);
            }
        }
        else if(targetTag=="Enemy")
        {
            EnemyControler e = target.GetComponent<EnemyControler>();
            if(e!=null)
            {
                e.TakeDamage(damage);
            }
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, explosionRadius);
    //}
}
