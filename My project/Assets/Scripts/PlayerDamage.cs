using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private Transform target; //target of shooting
    private EnemyControler targetEnemy;

    [Header("Shoot")]
    public float radiusDamage;
    private string enemyTag = "Enemy";
    public GameObject bulletPrefab;
    public float fireRate; //cd of shooting
    private float fireCountDown = 0f;
    public Transform firePoint;

    [Header("Rotate while shoot")]
    public Transform partToRotate;//part of body to rotate player
    public float turnSpeed; //speed of rotate

    [Header("Can shoot?")]
    public bool canShoot;
    public int inOnePlace = 0;
    public int inOnePlaceMax = 5;
    private Vector3 lastPosition;

    private void Start()
    {
        //SphereCollider sc=transform.GetComponent<SphereCollider>();
        //radiusDamage = sc.radius;
        canShoot = true;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        InOnePlace();
        if (canShoot)
        {
            if (target != null)
            {
                Debug.Log("Shoot");
                LookOnTarget();
                if (fireCountDown <= 0f)
                {
                    Shoot();
                    fireCountDown = 1f / fireRate;
                }

                fireCountDown -= Time.deltaTime;
            }
        }
    }

    private void UpdateTarget()
    {
        Debug.Log("UpdateTarget");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //find all enemies

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        //find nearest enemy for distance to player
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= radiusDamage)
        {
            target = nearestEnemy.transform;
            Debug.Log("New Target");
            targetEnemy = nearestEnemy.GetComponent<EnemyControler>();
        }
        else
        {
            target = null;
        }
    }

    private void LateUpdate()
    {
        lastPosition = new Vector3((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
    }

    private void LookOnTarget()
    {
        Vector3 dir = target.position - transform.position; //point to look rotation
        Quaternion lookRotation = Quaternion.LookRotation(dir); //vector of lookrotation
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;//rotate player in turnspeed to needrotation
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); //rotation of player to rotation target
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null) bullet.Seek(target);
    }

    private void InOnePlace()
    {
        Vector3 intPosition = new Vector3((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        if (intPosition == lastPosition)
        {
            inOnePlace++;
            if (inOnePlace >= inOnePlaceMax)
            {
                canShoot = true;
            }
        }
        else
        {
            inOnePlace = 0;
            canShoot = false;
        }
    }
}
