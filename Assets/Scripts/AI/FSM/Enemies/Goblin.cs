using UnityEngine;
using System.Collections;

/// <summary>
/// Controls  the goblin's AI behaviour
/// </summary>
public class Goblin : Enemy
{
    protected override void IdleState()  // Override the Idle State
    {
        base.IdleState();
        timeElapsed += Time.deltaTime; // Begin couting down for the duration
        if (timeElapsed >= Idleduration)  // If the idleduration is met then transition to Patrol State
        {
            CurrentState = State.Patrol;  // Set the current state to Patrol State
            anim.SetBool("Locomotion", true);   // Set the animator data
            wanderpointCounter++;               // increment the counter
            if (wanderpointCounter >= WanderPoints.Count) wanderpointCounter = 0;     // If the max value of the counter is reached the reset back to 0
            timeElapsed = 0.0f;      // Reset timeElapsed to 0
        }
    }

    protected override void PatrolState()
    {
        agent.SetDestination(WanderPoints[wanderpointCounter].transform.position); // Assign the destination for the NavMesh Agent
        if (agent.remainingDistance < agent.stoppingDistance)      // If the wanderpoint is reached transition back to idle state
        {
            anim.SetBool("Locomotion", false);   // set animator data
            CurrentState = State.Idle;           // change the current state to idle state
        }
        base.PatrolState();
    }

    protected override void ChaseState()
    {
        base.ChaseState();
        agent.SetDestination(CharacterReference.transform.position);   // set the destination of the NavMesh Agent to the character reference
        if (Vector3.Distance(gameObject.transform.position, CharacterReference.transform.position) >= MaxChaseDistance) // If the character is way too far from the enemy then transition back to patrol
            CurrentState = State.Patrol;    // set the current state back to patrol state
    }

    protected override void DeathState()
    {
        base.DeathState();
        agent.Stop();  // stop the NavMesh Agent since the enemy is already dead
    }
}
