using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody),typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float healthCurrent;
    public float healthMax;
    public bool isCanTakeDamage=true;
    public float moveSpeedCurrent;
    public float moveSpeedStart;

    [Header("UI")]
    public Slider hpUI;
    [SerializeField] private FixedJoystick joystick;

    [Header("Other")]
    [SerializeField] private Rigidbody rb;
    public GameObject GFX;

    private void Start()
    {
        healthCurrent = healthMax;
        moveSpeedStart = moveSpeedCurrent;
    }

    private void FixedUpdate()
    {
        //moving player
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeedCurrent, rb.velocity.y, joystick.Vertical * moveSpeedCurrent);
        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            GFX.transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    public void TakeDamage(float amount)
    {
        if (isCanTakeDamage)
        {
            //damage player
            healthCurrent -= amount;
            //change hp ui
            ChangeHP();

            if (healthCurrent <= 0)
            {
                Die();
            }
        }
    }

    public void ChangeHP()
    {
        hpUI.GetComponent<HealthUI>().UpdateProgress(healthCurrent / healthMax);
    }

    private void Die()
    {
        Debug.Log("Die");
    }

    private void Update()
    {

    }
}
