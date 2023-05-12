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
    �����L��,
    ���L��,
}

//��Enum�ϥΦb��Q�l��ɱ����e�l��ƶq�H�ΰŰ����ƶq
public enum Monster_type
{
    Default,
    ���ж��Ǫ�,
    �]�p�ЩǪ�
}