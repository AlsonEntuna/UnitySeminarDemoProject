using UnityEngine;
using System.Collections;

public class Goblin : Enemy
{
    protected override void IdleState()
    {
        base.IdleState();
        anim.SetBool("Idle", true);
    }

    protected override void PatrolState()
    {
        base.PatrolState();
    }

    protected override void ChaseState()
    {
        base.ChaseState();

        agent.SetDestination(CharacterReference.transform.position);
    }

    protected override void DeathState()
    {
        base.DeathState();
    }
}
