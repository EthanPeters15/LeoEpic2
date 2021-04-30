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

	Vector2 moveDirection = new Vector2();

	public static Animator PlayerAnim;

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
    }

}