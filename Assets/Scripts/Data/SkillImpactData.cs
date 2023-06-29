using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillImpactData
{
    public string type;

    public E_GridSelector_Type selectType;
    public int ImpactDistance;
    public int ImpactCount;

    public int buffId;


    public string str;

    // 技能固定伤害 / buff数值
    public int ImpactValue;

    // 技能倍率伤害 / buff持续时间
    public int ImpactDuration;
}
