using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleWeapon : MonoBehaviour
{
    public float damage;
    public float attackTime;
    private enemyHealth enemyhealth;
    private BoxCollider2D collider;


    [SerializeField] private bool isGun;
    [SerializeField] private bool isMelee;
    [HideInInspector] public bool isHeld;
    /*[HideInInspector]*/ public bool isHitting;
    private ArmAttack armAttack;
    private int callGetComponentArmOnce = 1;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (7) && isHitting)
        {
            enemyhealth = collision.gameObject.GetComponent<enemyHealth>();
            enemyhealth.health -= damage;
        }
    }


    private void Update()
    {
        if (isHeld && callGetComponentArmOnce == 1) { callGetComponentArmOnce = 0; armAttack = transform.parent.GetComponent<ArmAttack>(); }
        if (isHeld) { transform.position = transform.parent.position; isHitting = armAttack.isHitting; }

        if (isHitting && isHeld) { collider.enabled = true; }
        else if (isHeld) { collider.enabled = false; }
    }


}