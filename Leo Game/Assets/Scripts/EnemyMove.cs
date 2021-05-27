using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : Enemies
{
    //variables
    public int _moveSpeed;
    public int _attackDamage;
    public float _attackRadius;

    //movement
    public float _followRadius;
    public Transform Player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 Rmovement;
    private string state;
   
    
        //end
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator enemyAnim;
    SpriteRenderer enemySR;

    void Start()
    {
        //get the player transform   
        playerTransform = GameObject.FindWithTag("Leo").transform;
        //enemy animation and sprite renderer 
        enemyAnim = gameObject.GetComponent<Animator>();
        enemySR = GetComponent<SpriteRenderer>();
        //set the variables
        setMoveSpeed(_moveSpeed);
        setAttackDamage(_attackDamage);
        setAttackRadius(_attackRadius);
        setFollowRadius(_followRadius);
        rb = this.GetComponent<Rigidbody2D>();
        Vector2 Rdirection = new Vector2(Random.Range(-0.8f, 0.8f), Random.Range(-0.8f, 0.8f));
        Rmovement = Rdirection;
        state = "wander";
    }
    
    
    // Update is called once per frame
    
    private void Update()
    {
       
        Vector3 direction = Player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;



        if (Vector3.Distance(playerTransform.position, transform.position) < _followRadius)
        {
            state = "follow";

            if (Vector3.Distance(playerTransform.position, transform.position) < _attackRadius)
            {
                state = "attack";
            }

        }

        else
        {
            state = "wander";

        }
        

        if (state == "attack")
        {

        }
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 Rdirection = new Vector2();
        Rdirection.x = Random.value > 0.5f ?
            Random.Range(-0.8f, -0.3f) :
            Random.Range(0.3f, 0.8f);
       Rdirection.y = Random.value > 0.5f ?
            Random.Range(-0.8f, -0.3f) :
            Random.Range(0.3f, 0.8f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Vector2 Rdirection = new Vector2(Random.Range(-0.8f, 0.8f), Random.Range(-0.8f, 0.8f));
        Rmovement = Rdirection;
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
        RmoveCharacter(Rmovement);
    }

   




    void RmoveCharacter(Vector2 Rdirection)
    {
        if (state == "wander")
        { 
            rb.MovePosition((Vector2)transform.position + (Rdirection * _moveSpeed * Time.deltaTime));
            
        }
    }


    void moveCharacter(Vector2 direction)
    {

        if (state == "follow")
        {
            rb.MovePosition((Vector2)transform.position + (direction * _moveSpeed * Time.deltaTime));

        }




    }
}   