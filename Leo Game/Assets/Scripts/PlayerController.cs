using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

	public float speed = 1;
	public ForceMode2D fMode;
	private Rigidbody2D rb;
	public string state;
	private Animator animator;
	[SerializeField] private float RotationSpeed;
	Vector2 moveDirection = new Vector2();
	public static Animator PlayerAnim;
	public ArmAttack armAttack;
	public ArmAttack armAttack2;
	[HideInInspector] public bool isAttacking;

	//Arm Rotation Variables
	private Vector3 mouse_pos;
	[SerializeField] private Transform target;
	private Vector3 object_pos;
	private float angle;
	public scaleLock scalelock;

	

	void Start()
	{
		state = "Normal";
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (state == "Normal")
        {
			Movement();
			Flip();
			ArmRotate();
			Attack();
			hasWeaponCheck();
        }
		if (state == "Attacking")
        {
			Movement();
			Flip();
			ArmRotate();
			hasWeaponCheck();
        }
	}

	void FixedUpdate()
	{
		transform.position += new Vector3(moveDirection.x * Time.deltaTime, moveDirection.y * Time.deltaTime, 0);
	}

	private void Movement()
    {
		moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		moveDirection *= speed;

		if (Input.GetAxis("Horizontal") > 0.05f || Input.GetAxis("Vertical") > 0.05f || Input.GetAxis("Horizontal") < -0.05f || Input.GetAxis("Vertical") < -0.05f)
        {
			animator.SetBool("isWalking", true);
        }else { animator.SetBool("isWalking", false); }

	}

	private void Flip()
    {
		if (Input.GetAxis("Horizontal") > 0.1f)
        {
			transform.localScale = new Vector3(1f, 1, 1f);
        }
		if (Input.GetAxis("Horizontal") < -0.1f)
        {
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}

		//Arm rotation as well
		if (Input.GetAxis("Horizontal") > 0.1f)
		{
			target.localScale = new Vector3(1f, 1, 1f);
		}
		if (Input.GetAxis("Horizontal") < -0.1f)
		{
			target.localScale = new Vector3(-1f, 1f, 1f);
		}
	}

	private void ArmRotate()
    {
		/*Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		armROT.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - armROT.position);*/

		if (isAttacking)
		{
			scalelock.isHitting = true;
			mouse_pos = Input.mousePosition;
			mouse_pos.z = -20;
			object_pos = Camera.main.WorldToScreenPoint(target.position);
			mouse_pos.x = mouse_pos.x - object_pos.x;
			mouse_pos.y = mouse_pos.y - object_pos.y;
			angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
			target.rotation = Quaternion.Euler(0f, 0f, angle);
		}else { target.rotation = Quaternion.Euler(0f, 0f, 0f); scalelock.isHitting = false; }
	}

	private void Attack()
    {
		if (Input.GetMouseButton(0) && armAttack.currentWeapon == null)
        {
			armAttack.Attack();
			armAttack2.Attack();
			armAttack.isHitting = true;
			armAttack2.isHitting = true;
			state = "Attacking";
			isAttacking = true;
			StartCoroutine(WaitForAttackTime(armAttack.currentWeaponAttackTime));
        }
		if (Input.GetMouseButton(0) && armAttack.currentWeapon != null)
		{
			armAttack.Attack();
			armAttack2.Attack();
			armAttack.isHitting = true;
			armAttack2.isHitting = true;
			state = "Attacking";
			isAttacking = true;
			StartCoroutine(WaitForAttackTime(armAttack.currentWeaponAttackTime));
		}
	}

	private IEnumerator WaitForAttackTime (float attackTime)
    {
		yield return new WaitForSeconds(attackTime);
		animator.SetBool("isHitting", false);
		armAttack.isHitting = false;
		armAttack2.isHitting = false;
		isAttacking = false;
		state = "Normal";
    }

	private void hasWeaponCheck()
    {
		if (armAttack.currentWeapon != null){ animator.SetBool("hasWeapon", true); } else { animator.SetBool("hasWeapon", false); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.layer == (8))
		{
			armAttack.GetNewWeapon(collision.gameObject);
		}
	}

}