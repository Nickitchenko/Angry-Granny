using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;

    public float enemySpeed;
    public float enemyHealth;

    private Vector3 currentpoint; //потрібна точка
    private Vector3 minPoint; //обмеження
    private Vector3 maxPoint;
    private bool canMove = true; //чи може рухатись

    private void Start()
    {
        currentpoint = transform.position;
        minPoint = new Vector3(currentpoint.x - 2, transform.position.y, currentpoint.z - 2);
        maxPoint = new Vector3(currentpoint.x + 2, transform.position.y, currentpoint.z + 2);
    }

    private void FixedUpdate()
    {
        //політ до рандомної точки
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, currentpoint, enemySpeed);

        //щукаємо нову точку
        if (transform.localPosition == currentpoint)
        {
            currentpoint = randomPoint(currentpoint);
        }
    }

    private Vector3 randomPoint(Vector3 point)
    {
        return new Vector3(Random.Range(minPoint.x, maxPoint.x), transform.position.y, Random.Range(minPoint.z,maxPoint.z)); //0,1 -x; 0,2 - y;
    }
}
