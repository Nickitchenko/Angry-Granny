using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[RequireComponent(typeof(Rigidbody),typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float health;
    public TMP_Text healthText;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float y;
    public GameObject GFX;

    private void Awake()
    {
        healthText.text = "Health " + health.ToString();
    }

    private void FixedUpdate()
    {
        //moving player
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            GFX.transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    public void TakeDamage(float amount)
    {
        //damage player
        health -= amount;
        //change hp ui
        healthText.text = "Health " + health.ToString();
        //healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Die");
    }

    private void Update()
    {
    //    y = transform.rotation.y;
    //    transform.rotation = Quaternion.Euler(0, y, 0);
    }
}
