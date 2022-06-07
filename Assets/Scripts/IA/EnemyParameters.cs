using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameters : MonoBehaviour
{
    public enum MonsterType
    {
        Wizard,
        Skeleton,
        Slime,
        RollingStone,
        Minotaur
    }

    public enum MonsterState
    {
        Patrolling = 8,
        Chasing = 15
    }
}
