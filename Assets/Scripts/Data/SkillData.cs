using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillData
{
    public string name;
    public int skillId;
    public E_Skill_Type type;

    // 技能介绍
    [TextArea]
    public string skillIntroduction;

    // 要求的武器类型
    public E_Weapon_Type weaponType;

    public int coolTime;
    public int coolRemain;

    public int costAc;
    public int costSan;

    //技能可选择目标的类型和范围
    public E_GridSelector_Type selectType;
    public int selectDistance;

    //技能的阵营 : 玩家敌人互相打, 中立都打
    [HideInInspector] public E_Camp_Type campType;
    //释放技能的对象
    [HideInInspector] public Character owner;
    //技能的释放位置
    [HideInInspector] public GridPosition targetGridPos;

    public List<SkillImpactData> skillImpactDataList;

    public bool isCanSell;
    public int price;

    public SkillData Clone()
    {
        return (SkillData)this.MemberwiseClone();
    }
}
