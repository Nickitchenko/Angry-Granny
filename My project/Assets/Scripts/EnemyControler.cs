using Unity.Android.Types;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private Rigidbody rigidbody;
    [Header("Enemy Stats")]
    public float enemyHealth; //хп ворога
    public float lookRadius = 4f; //рад≥ус баченн€ ворога гравц€
    //moving
    public Vector3 currentpoint; //потр≥бна точка
    private Vector3 minPoint; //обмеженн€
    private Vector3 maxPoint;
    private bool canMove = true; //чи може рухатись

    [Header("Enemy shoot")]
    private Transform target;//target to shoot
    public GameObject bulletPrefab;//bullet
    public Transform firePoint; //pont on enemy from which bullet fly to target
    public float fireCountDown;//current cd of shooting
    public float fireRate;//cd of shooting

    private bool simplemove = true;

    private void Start()
    {
        currentpoint = transform.position;
        minPoint = new Vector3(currentpoint.x - 3, transform.position.y, currentpoint.z - 3);
        maxPoint = new Vector3(currentpoint.x + 3, transform.position.y, currentpoint.z + 3);

        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (simplemove)
        {
            //пол≥т до рандомноњ точки
            agent.SetDestination(currentpoint);
            //Debug.Log("moving");
            //щукаЇмо нову точку
            if (transform.position == currentpoint)
            {
                currentpoint = randomPoint(currentpoint);
                //Debug.Log("stay");
            }
        }
        else
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            simplemove = false;
            if(distance<=agent.stoppingDistance)
            {
                if (fireCountDown <= 0f)
                {
                    Damage(target);
                    fireCountDown = 1f / fireRate;
                }
                fireCountDown -= Time.deltaTime;
            }
        }
        else
        {
            simplemove = true;
        }
    }

    private void FaceTarget()
    {
        Vector3 direction= (target.position-transform.position).normalized;
        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * 5f);
    }

    private Vector3 randomPoint(Vector3 point)
    {
        return new Vector3(Random.Range(minPoint.x, maxPoint.x), 1, Random.Range(minPoint.z, maxPoint.z)); //0,1 -x; 0,2 - y;
    }

    private void Damage(Transform target)
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null) bullet.Seek(target);
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        if(enemyHealth<=0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy die");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
