using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : Player_State,ITakeDamage
{
    private Rigidbody2D rb;
    private Animator anim;

    public float moveH;
    public float moveV;
    public float moveSpeed;
    public float jumpForce;
    public float fallForce;
    public float repelledDistance;
    public float distance;
    public float hangingForce;
    public float climbSpeed;
    public float shakeTime;
    public float strength;
    public float protectdTime;
    public int hitPauseTime;
    public int attackPauseTime;
    public float hitDistance;
    public float hitHeight;



    private bool isRun;
    public bool isJump;
    public bool isGround;
    public bool stopInput;
    public bool canMove;
    
    public bool isHanging;
    public bool isOnWall;
    public bool isClimb;
    public bool isProtected;
    public bool isHit;

    public Transform groundPoint;
    public Transform attackPoint;

    public Vector2 groundSize;
    private Vector2 distanceVec2;

    public float attackRadius;

    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    

    private BoxCollider2D boxCollider2D;

    private Cinemachine.CinemachineCollisionImpulseSource myInpulse;

    public object enemy;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        myInpulse = GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
        //Physics2D.queriesStartInColliders = false;
        currentHp = maxHp;
    }

    private void Update()
    {
        if(!stopInput)
        {
            moveH = Input.GetAxisRaw("Horizontal") * moveSpeed;
            moveV = Input.GetAxisRaw("Vertical") * climbSpeed;
            Flip();
            if (Input.GetKeyDown(KeyCode.Space) && (isGround||isHanging))
            {
                isJump = true;
            }
        }


        InputCheck();
        PhysicsCheck();
        AnimationState();
        
        
    }

    private void FixedUpdate()
    {
        if(isJump)
        {
            if(isHanging)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                isHanging = false;
                rb.AddForce(Vector2.up * hangingForce);
            }
            else
            {
                rb.velocity = new Vector2(0, jumpForce);
            }
            
            anim.SetTrigger("Jump");
            isJump = false;
        }
        if(isOnWall)
        {
            rb.velocity = new Vector2(0, moveV);
        }
        BetterJump();
        rb.velocity = new Vector2(moveH,rb.velocity.y);
    }

    private void Flip()
    {
        if(moveH>0)
        {
            transform.localScale = new Vector3(5,5,5);
        }
        else if(moveH<0)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }

    }

    private void AnimationState()
    {
        if(moveH!=0)
        {
            isRun = true;
        }
        else 
        {
            isRun = false;
        }


        if (moveV > 0)
        {
            isClimb = true;
        }
        else
        {
            isClimb = false;
        }

        anim.SetBool("Run", isRun);
        anim.SetBool("isGround", isGround);
        anim.SetBool("isHanging", isHanging);
        anim.SetBool("isOnWall", isOnWall);
        anim.SetBool("isClimb", isClimb);
    }

    private void PhysicsCheck()
    {
        //地面检测
        Collider2D collider2D = Physics2D.OverlapBox(groundPoint.position, groundSize, 0,groundLayer);
        //悬挂检测
        RaycastHit2D headHit2D = Physics2D.Raycast(transform.position + new Vector3(0.2f * distance, 0.7f, 0), distanceVec2, 0.5f, groundLayer);
        RaycastHit2D eyeHit2D = Physics2D.Raycast(transform.position + new Vector3(0.2f * distance, 0.4f, 0), distanceVec2, 0.5f, groundLayer);
        RaycastHit2D upToDown = Physics2D.Raycast(transform.position + new Vector3(0.5f * distance, 0.7f, 0), Vector3.down, 0.5f, groundLayer);
        RaycastHit2D footHit2D = Physics2D.Raycast(transform.position + new Vector3(0.2f * distance, -0.7f, 0), distanceVec2, 0.5f, groundLayer);



        if (collider2D!=null)
        {
            isGround = true;
            
        }
        else
        {
            isGround = false;
        }

        distance = transform.localScale.x/5;
        distanceVec2 = new Vector2(distance, 0);

        
        

        if(headHit2D.collider==null&&eyeHit2D.collider!=null&&upToDown.collider!=null)
        {
            //固定悬挂的位置
            Vector3 pos = transform.position;
            pos.x += (eyeHit2D.distance-0.2f) * distance;
            pos.y -= (upToDown.distance-0.05f);
            transform.position = pos;


            isHanging = true;
            isOnWall = false;
            rb.bodyType = RigidbodyType2D.Static;
        }
        else if(headHit2D.collider!=null&&footHit2D.collider!=null&&(distance>0 ? Input.GetKey(KeyCode.D)
                                                                                 :Input.GetKey(KeyCode.A))
                                                                    &&Input.GetKey(KeyCode.LeftShift))
        {
            isOnWall = true;
            //rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            isHanging = false;
            isOnWall = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            
        }
        


    }

    private void BetterJump()
    {
        if (!isHanging)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity * fallForce * Time.fixedDeltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rb.velocity += Vector2.up * Physics2D.gravity * fallForce * Time.fixedDeltaTime;
            }
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * fallForce*1.5F * Time.fixedDeltaTime;
        }
    }

    private void Attack()
    {
        MusicManager.instance.PlaySound("挥剑", false);
        Collider2D collider2D = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayer);

        if(collider2D!=null)
        {
            ITakeDamage takeDamage = collider2D.transform.GetComponent<ITakeDamage>();
            Enemy_State enemy_State = collider2D.transform.GetComponent<Enemy_State>();
            if(takeDamage!=null)
            {
                MusicManager.instance.PlaySound("剑击中", false);
                takeDamage.TakeDamage(attackAmount);
                //enemy_State.Repelled(transform);
                collider2D.GetComponent<Rigidbody2D>().AddForce(new Vector2((collider2D.transform.position.x - transform.position.x), 0).normalized*5f,ForceMode2D.Impulse);

                
                if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
                {
                    CameraShake_Pause.instance.HitPause(attackPauseTime);
                    myInpulse.GenerateImpulse();
                    //print("666");
                }
                

            }
            //print("aaa");
        }
    }
    private void InputCheck()
    {
        

        if (Input.GetKeyDown(KeyCode.J) && !isGround && Input.GetKey(KeyCode.S))
        {
            anim.SetTrigger("Attack_down");
            
        }
        else if (Input.GetKeyDown(KeyCode.J) && !isGround)
        {
            anim.SetTrigger("Attack_air1");
            
        }
        else if (Input.GetKeyDown(KeyCode.J) && isGround)
        {
            anim.SetTrigger("Attack1");
            
        }


    }

    public void TakeDamage(float _amount,object obj=null)
    {
        if(obj!=null)
        {
            enemy = obj;
        }
        else
        {
            enemy = null;
        }
        anim.SetTrigger("Hit");
        currentHp -= _amount;
        if (currentHp <= 0) currentHp = maxHp;
        EventCenter.EventTrigger("Hit", this);
        
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * hitHeight);//受击高度

        StartCoroutine(Pretected());//开启无敌时间
        myInpulse.GenerateImpulse();//屏幕震动
        CameraShake_Pause.instance.HitPause(hitPauseTime);//顿帧，慢动作
        
    }

    IEnumerator Pretected()
    {
        isProtected = true;
        float timer = protectdTime;
        while(timer>0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        isProtected = false;

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundPoint.position, groundSize);
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.DrawRay(transform.position + new Vector3(0.2f * distance, 0.7f, 0), distanceVec2*0.5f);
        Gizmos.DrawRay(transform.position + new Vector3(0.2f * distance, 0.4f, 0), distanceVec2 * 0.5f);
        Gizmos.DrawRay(transform.position + new Vector3(0.5f * distance, 0.7f, 0), Vector3.down * 0.5f);
        Gizmos.DrawRay(transform.position + new Vector3(0.2f * distance, -0.7f, 0), distanceVec2*0.5f);
        Gizmos.color = Color.red;
    }
}
