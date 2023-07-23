using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Witch : Enemy_State,ITakeDamage
{
    private Animator anim;

    public float moveSpeed;
    
    

    public bool isRun;
    
    

    public Vector2 attackArea;


    public Transform attackPoint;
    public Transform idelPoint;

    public LayerMask attackLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private QuestTarget questTarget;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        questTarget = GetComponent<QuestTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isRun", isRun);
        
    }


    public void TakeDamage(float _amount,object obj=null)
    {

            anim.SetTrigger("Hit");
            currentHp -= _amount;
            if (currentHp <= 0)
            {
                if (questTarget != null) questTarget.TaskCompleted();
                anim.SetTrigger("Death");
                rb.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                Destroy(gameObject, 2);
            }
        
        
    }
    public void Attack_Event()
    {
        
        Collider2D collider2D = Physics2D.OverlapBox(attackPoint.position, attackArea, 0, attackLayer);

        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();


        if (collider2D!=null&&!playerMovement.isProtected)
        {
            
            ITakeDamage takeDamage =collider2D.GetComponent<ITakeDamage>();
            takeDamage.TakeDamage(damage,this.transform);
            
        }


    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(attackPoint.position, attackArea);
    }
}
