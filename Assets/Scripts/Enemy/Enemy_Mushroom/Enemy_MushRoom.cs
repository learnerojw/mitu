using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MushRoom : Enemy_State, ITakeDamage
{
    //public FSM_MushRoom FSM;
    //public Enemy_State enemy_State;
    

    private Animator anim;

    public bool isPatrol;

    public float idelTime;

    public Transform[] PatrolPoints;

    public float patrolSpeed;
   

    public Transform attackPoint;
    public float attackRaduis;
    public LayerMask attackLayer;

    public GameObject AttackEffect;

    //private bool startCoroutineRepelled;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHp = maxHp;
        //FSM = GetComponent<FSM_MushRoom>();
    }

    private void Update()
    {
        anim.SetBool("Patrol", isPatrol);
    }


    public void TakeDamage(float _amount,object obj=null)
    {
        anim.SetTrigger("Hit");
        GameObject attackEffect = Instantiate(AttackEffect, transform.position, Quaternion.identity, transform);
        attackEffect.transform.localPosition = new Vector3(0, -0.1f, 0);
        currentHp -= _amount;
        if(currentHp<=0)
        {
            Destroy(gameObject);
        }
        //FSM.TransitionState(StateType.Hit);
    }


    

    public void Attack()
    {
        Collider2D collider2D = Physics2D.OverlapCircle(attackPoint.position, attackRaduis, attackLayer);

        if (collider2D != null)
        {
            ITakeDamage takeDamage = collider2D.transform.GetComponent<ITakeDamage>();

            takeDamage.TakeDamage(damage,this.transform);


        }


    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRaduis);
        Gizmos.color = Color.red;
    }
}
