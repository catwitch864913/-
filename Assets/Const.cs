using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    None = 0,
    Player,
    Enemy,
}
public enum Enemy_Type
{
    Mozu = 1,
    MozuLeader,
    Fairy,
    FairyLeader,
}

public enum InKitchenEnemy
{
    SB,
    Normal,
    Up01,
    Up02,
    down01,
    down02,
}
public enum EnemyState
{
    Idle,
    Chase,
    Patrol,
    Attack,
    Die,
}

public enum BossState
{
    Idle,
    Chase,
    Attack,
    Skill,
    Die,
}
public enum BossState2
{
    Idle,
    Chase,
    Attack,
    Skill,
    Call,
    Die,
}
public enum BossHurtOrNoHurt
{
    未受過傷,
    受過傷,
}

//此Enum使用在於被召喚時控制當前召喚數量以及剪除的數量
public enum Monster_type
{
    Default,
    假房間怪物,
    魔廚房怪物
}