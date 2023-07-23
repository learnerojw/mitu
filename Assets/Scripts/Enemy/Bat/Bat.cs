using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class BlackBoard_Bat:BlackBoard
{
    public float maxHp;

    public float chaseDistance;
    //public float attackDistance;
    public Transform idlePoint;

    public Transform findPoint;

    public Vector3 findArea;

    public float flySpeed;

    public float attackCD;

    public Transform attackPoint;

    public float attackRadius;

    public float attackMoveSpeed;

    public Transform targetPlayer;

    public Animator animator;

    public Transform transform;

    public LayerMask layerMask;

    public Rigidbody2D rb;
}

public class Bat : MonoBehaviour,ITakeDamage
{
    public BlackBoard_Bat blackBoard_Bat;

    public FSM fsm;

    private void Awake()
    {
        blackBoard_Bat.animator = GetComponent<Animator>();
        blackBoard_Bat.transform = this.transform;
        blackBoard_Bat.rb = GetComponent<Rigidbody2D>();
        fsm = new FSM(blackBoard_Bat);
        fsm.AddState(StateType.Idle, new Idle_Bat(fsm));
        fsm.AddState(StateType.Chase, new Chase_Bat(fsm));
        fsm.AddState(StateType.Attack, new Attack_Bat(fsm));
        fsm.AddState(StateType.Hit, new Hit_Bat(fsm));
        fsm.SwitchState(StateType.Idle);
    }

    private void Update()
    {
        Collider2D collider2D = Physics2D.OverlapBox(blackBoard_Bat.findPoint.position, blackBoard_Bat.findArea, 0,blackBoard_Bat.layerMask);
        if (collider2D != null)
        {
            blackBoard_Bat.targetPlayer = collider2D.transform;
        }
        else
        {
            blackBoard_Bat.targetPlayer = null;
        }

        fsm.OnUpdate();
    }


    private void FixedUpdate()
    {
        fsm.OnFixUpdate();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(blackBoard_Bat.attackPoint.position, blackBoard_Bat.attackRadius);
        Gizmos.DrawWireCube(blackBoard_Bat.findPoint.position, blackBoard_Bat.findArea);
    }

    public void TakeDamage(float _amount, object obj = null)
    {
        fsm.SwitchState(StateType.Hit);
        blackBoard_Bat.maxHp -= _amount;
        if(blackBoard_Bat.maxHp<=0)
        {
            Destroy(gameObject);
        }
    }
}

public class Idle_Bat : IState
{
    public FSM fsm;

    public BlackBoard_Bat blackBoard_Bat;
    public Idle_Bat(FSM fsm)
    {
        this.fsm = fsm;
        blackBoard_Bat = fsm.blackBoard as BlackBoard_Bat;
    }
    public void OnEnter()
    {
        blackBoard_Bat.animator.CrossFade("Idle", 0.5f);
        blackBoard_Bat.rb.velocity = Vector2.zero;
    }

    public void OnExit()
    {
        
    }

    public void OnFixUpdate()
    {
        
    }

    public void OnUpdate()
    {
        
        if(blackBoard_Bat.targetPlayer!=null)
        {
            fsm.SwitchState(StateType.Chase);
        }
    }
}

public class Chase_Bat : IState
{
    public FSM fsm;
    public BlackBoard_Bat blackBoard_Bat;

    private float attackTimer;
    private Vector3 patrolPoint;//Ñ²Âßµã
    private bool isUpdatePoint;
    public Chase_Bat(FSM fsm)
    {
        this.fsm = fsm;
        blackBoard_Bat = fsm.blackBoard as BlackBoard_Bat;
    }
    public void OnEnter()
    {
        blackBoard_Bat.animator.CrossFade("Fly",0.5f);
        attackTimer = blackBoard_Bat.attackCD;
        isUpdatePoint = true;
    }

    public void OnExit()
    {
        attackTimer = 0;
    }

    public void OnUpdate()
    {
        if(blackBoard_Bat.targetPlayer==null)
        {
            //blackBoard_Bat.transform.position = Vector3.Lerp(blackBoard_Bat.transform.position,
            //                                               blackBoard_Bat.idlePoint.position,
            //                                               blackBoard_Bat.flySpeed * Time.deltaTime);

            
            Filp(blackBoard_Bat.idlePoint);
            if ((blackBoard_Bat.transform.position- blackBoard_Bat.idlePoint.position).magnitude<0.1f)
            {
                
                fsm.SwitchState(StateType.Idle);
                return;
            }
            
        }

        if (blackBoard_Bat.targetPlayer!=null)
        {
            Filp(blackBoard_Bat.targetPlayer);
            attackTimer -= Time.deltaTime;
            if(attackTimer<=0)
            {
                fsm.SwitchState(StateType.Attack);
                return;
            }
            if(isUpdatePoint)
            updatePatrolPoint();
            //blackBoard_Bat.transform.position = Vector3.Lerp(blackBoard_Bat.transform.position,
            //                                             patrolPoint,
            //                                             blackBoard_Bat.flySpeed * Time.deltaTime);
            
            if ((blackBoard_Bat.transform.position - patrolPoint).magnitude < 0.5f) isUpdatePoint = true;
        }
    }

    private void updatePatrolPoint()
    {
        float pointX = UnityEngine.Random.Range(-blackBoard_Bat.chaseDistance, blackBoard_Bat.chaseDistance);
        float pointY = UnityEngine.Random.Range(0, blackBoard_Bat.chaseDistance);
        patrolPoint = blackBoard_Bat.targetPlayer.position + new Vector3(pointX, pointY, 0);
        isUpdatePoint = false;
    }

    private void Filp(Transform transform)
    {
        if(blackBoard_Bat.transform.position.x< transform.position.x)
        {
            blackBoard_Bat.transform.localScale = new Vector3(-3, 3, 3);
        }
        else
        {
            blackBoard_Bat.transform.localScale = new Vector3(3, 3, 3);
        }
    }

    public void OnFixUpdate()
    {
        if (blackBoard_Bat.targetPlayer == null)
        {
            Vector2 dir = (blackBoard_Bat.idlePoint.position - blackBoard_Bat.transform.position).normalized;
            blackBoard_Bat.rb.velocity = dir * blackBoard_Bat.flySpeed * Time.fixedDeltaTime;
        }
        else
        {
            Vector2 dir = (patrolPoint - blackBoard_Bat.transform.position).normalized;
            blackBoard_Bat.rb.velocity = dir * blackBoard_Bat.flySpeed * Time.fixedDeltaTime;
        }
        
    }
}

public class Attack_Bat : IState
{
    public FSM fsm;

    public BlackBoard_Bat blackBoard_Bat;

    public Attack_Bat(FSM fsm)
    {
        this.fsm = fsm;
        this.blackBoard_Bat = fsm.blackBoard as BlackBoard_Bat;
    }
    public void OnEnter()
    {
        blackBoard_Bat.animator.CrossFade("Attack", 0.5f);
    }

    public void OnExit()
    {
        
    }

    public void OnFixUpdate()
    {
        Vector2 dir = (blackBoard_Bat.targetPlayer.position - blackBoard_Bat.transform.position).normalized;
        blackBoard_Bat.rb.velocity = dir * blackBoard_Bat.attackMoveSpeed*Time.fixedDeltaTime;
    }

    public void OnUpdate()
    {
        if(blackBoard_Bat.targetPlayer==null)
        {
            
            fsm.SwitchState(StateType.Chase);
        }

        if(blackBoard_Bat.targetPlayer!=null)
        {
            Filp();
            //blackBoard_Bat.transform.position = Vector3.Lerp(blackBoard_Bat.transform.position,
            //                                                 blackBoard_Bat.targetPlayer.position,
            //                                                 blackBoard_Bat.attackMoveSpeed * Time.deltaTime);
            
            Collider2D collider2D = Physics2D.OverlapCircle(blackBoard_Bat.attackPoint.position, blackBoard_Bat.attackRadius,blackBoard_Bat.layerMask);

            if(collider2D!=null&&collider2D.CompareTag("Player"))
            {
                //µ÷ÓÃtakedamage
                collider2D.GetComponent<ITakeDamage>().TakeDamage(5);
                
                fsm.SwitchState(StateType.Chase);
            }
        }
    }

    private void Filp()
    {
        if (blackBoard_Bat.transform.position.x < blackBoard_Bat.targetPlayer.position.x)
        {
            blackBoard_Bat.transform.localScale = new Vector3(-3, 3, 3);
        }
        else
        {
            blackBoard_Bat.transform.localScale = new Vector3(3, 3, 3);
        }
    }
}

public class Hit_Bat : IState
{
    public FSM fsm;

    public BlackBoard_Bat blackBoard_Bat;

    public float timer = 0.2f;
    public Hit_Bat(FSM fsm)
    {
        this.fsm = fsm;
        this.blackBoard_Bat = fsm.blackBoard as BlackBoard_Bat;
    }
    public void OnEnter()
    {
        timer = 0.2f;
        blackBoard_Bat.animator.Play("Hit");
        
    }

    public void OnExit()
    {
        
    }

    public void OnFixUpdate()
    {
        
    }

    public void OnUpdate()
    {
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            fsm.SwitchState(StateType.Chase);
        }
    }
}


