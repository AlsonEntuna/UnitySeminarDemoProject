using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public float Idleduration;  // The duration of how long will the enemy be in Idle State
    public float PatrolSpeed;   //  How fast will be the enemy be patroling
    public float ChaseSpeed;    // How fast will the enemy be chasing
    public float MaxChaseDistance;     // The max distance between the enemy and the player for the enemy to change state back to patrol
    public GameObject CharacterReference;   // The reference for the character

    [Header("WanderPoints")]
    public List<Transform> WanderPoints = new List<Transform>();  // A list of wanderpoints for the navmesh agent to follow through
    [SerializeField]protected int wanderpointCounter = -1;        // The counter for the wanderpoints to iterate 
    [SerializeField]protected float timeElapsed;                  // Elapsed time that the IdleState will be executing

    public Health Health { get; protected set; }  // Reference to the Health Component

    public enum State // Enum State for the Enemies
    {
        Idle,
        Patrol,
        Chase,
        Dead
    }

    public State CurrentState;     // Current State of the Enemy
    protected NavMeshAgent agent;   // Navigation Mesh Component of the GameObject
    protected Animator anim;         // Animator coponent                                               

    void Start()
    {
        // Set up the reference for each component
        agent = GetComponent<NavMeshAgent>();
        if (!agent)
            gameObject.AddComponent<NavMeshAgent>();

        Health = GetComponent<Health>();
        if (!Health)
            throw new UnityException("No Health Component Added");

        anim = GetComponent<Animator>();
        if (!anim)
            throw new MissingComponentException("No Animator Component Found");

        CurrentState = State.Idle;    // Set the current stat of the enemy to IdleState
    }

    bool callOnce = true;
    void Update()
    {
        switch (CurrentState)   // Switch Statement for the condition of the Transition of States
        {
            case State.Idle: IdleState(); break;
            case State.Patrol: PatrolState(); break;
            case State.Chase: ChaseState(); break;
            case State.Dead: DeathState(); break;
        }

        if (Health.IsDead && callOnce)  // If the health reaches 0 // Call Once algorithm is used
        {
            anim.SetTrigger("Dead");
            CurrentState = State.Dead;
            callOnce = false;
        }
    }

    protected virtual void IdleState() { }   // Virtual Function for Idle State
    protected virtual void PatrolState()     // Virtual Function for Patrol State
    {
        // Assign NavMesh Agent Information
        agent.speed = PatrolSpeed;
    }
    protected virtual void ChaseState()   // Virtual Function  for Chase State
    {
        // Assign NavMesh Agent Information
        agent.speed = ChaseSpeed;
    }
    protected virtual void DeathState(){ }     // Virtual Function for Death state

}
