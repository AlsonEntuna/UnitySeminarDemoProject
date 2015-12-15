using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float Idleduration;
    public float PatrolSpeed;
    public float ChaseSpeed;
    public GameObject CharacterReference;

    public Health Health { get; protected set; }

    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Dead
    }

    public State CurrentState;
    protected NavMeshAgent agent;
    protected Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!agent)
            gameObject.AddComponent<NavMeshAgent>();

        Health = GetComponent<Health>();
        if (!Health)
            throw new UnityException("No Health Component Added");

        anim = GetComponent<Animator>();
        if (!anim)
            throw new MissingComponentException("No Animator Component Found");

        CurrentState = State.Idle;
    }

    bool callOnce = true;
    void Update()
    {
        switch (CurrentState)
        {
            case State.Idle: IdleState(); break;
            case State.Patrol: PatrolState(); break;
            case State.Chase: ChaseState(); break;
            case State.Dead: DeathState(); break;
        }

        if (Health.IsDead && callOnce)
        {
            CurrentState = State.Dead;
            callOnce = false;
        }
    }

    protected virtual void IdleState() { }
    protected virtual void PatrolState() { }
    protected virtual void ChaseState() { }
    protected virtual void DeathState(){ }

}
