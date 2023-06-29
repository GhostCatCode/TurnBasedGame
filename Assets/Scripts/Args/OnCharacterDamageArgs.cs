using System.Collections;
using UnityEngine;

public class OnCharacterDamageArgs
{
    // 攻击者
    public Character maker;
    // 受击者
    public Character victim;
    // 伤害值
    public int value;

    public OnCharacterDamageArgs(Character maker, Character victim, int value)
    {
        this.maker = maker;
        this.victim = victim;
        this.value = value;
    }
}