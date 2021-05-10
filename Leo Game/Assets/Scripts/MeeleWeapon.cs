using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleWeapon : MonoBehaviour
{
    public float damage;
    public float attackTime;
    private enemyHealth enemyhealth;


    [SerializeField] private bool isGun;
    [SerializeField] private bool isMelee;
    [HideInInspector] public bool isHeld;
    /*[HideInInspector]*/ public bool isHitting;

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
        if (isHeld)
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }
    }


}