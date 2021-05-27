using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAttack : MonoBehaviour
{

    //Stats
    public float damage;
    public Transform player;

    //Default Stats
    private Vector2 defaultHitbox = new Vector2(0.5f, 0.35f);
    private float defaultDamage = 1;
    [HideInInspector] public float defaultAttackTime = 0.33f;


    //State Variables
    /*[HideInInspector]*/ public bool isHitting;
    /*[HideInInspector]*/ public bool isHoldingWeapon;


    //Weapons Stats
    public float currentWeaponAttackTime;
    private MeeleWeapon weaponStats;
    [HideInInspector] public GameObject currentWeapon;


    //Components
    private BoxCollider2D boxCollider;
    public TrailRenderer trailRenderer;
    public Animator playerAnimator;


    //Enemy Health
    private enemyHealth enemyhealth;



    private void Awake()
    {
        //Default Stats
        isHitting = false;
        currentWeaponAttackTime = defaultAttackTime;
        boxCollider.size = defaultHitbox;
        damage = defaultDamage;

        //Get Components
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //if (isHoldingWeapon) {Debug.Log("Should be setting"); }

        //if (isHitting) { trailRenderer.enabled = true; } else { trailRenderer.enabled = false; }

        //if (isHoldingWeapon && isHitting) { weaponStats.isHitting = true; } else if (isHoldingWeapon && !isHitting) { weaponStats.isHitting = false; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (7) && isHitting  && !isHoldingWeapon)
        {
            enemyhealth = collision.gameObject.GetComponent<enemyHealth>();
            enemyhealth.health -= damage;
        }
        if (collision.gameObject.layer == (7) && isHitting && isHoldingWeapon)
        {
            return;
        }
    }

    public void Attack()
    {
        if (currentWeapon == null)
        {
            playerAnimator.SetBool("isHitting", true);
        }
    }


    public void GetNewWeapon (GameObject collision)
    {
        if (!isHoldingWeapon)
        {
            currentWeapon = collision;
            weaponStats = collision.GetComponent<MeeleWeapon>();
            collision.transform.parent = transform;
            isHoldingWeapon = true;
            weaponStats.isHeld = true;
            currentWeaponAttackTime = weaponStats.attackTime; currentWeapon.transform.position = new Vector3(0f, 0f, 0f);
            currentWeapon.transform.position = new Vector3(0f, 0f, 0f);
            if (player.localScale.x == -1f) { currentWeapon.transform.localScale = new Vector3(1f, -1f, 1f); }
        }
    }

    public void DropWeapon ()
    {
        Destroy(currentWeapon);
        weaponStats = null;
        currentWeapon = null;
        isHoldingWeapon = false;
        currentWeaponAttackTime = 0.33f;
    }
}
