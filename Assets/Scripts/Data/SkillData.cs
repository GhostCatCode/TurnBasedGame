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

    // ���ܽ���
    [TextArea]
    public string skillIntroduction;

    // Ҫ�����������
    public E_Weapon_Type weaponType;

    public int coolTime;
    public int coolRemain;

    public int costAc;
    public int costSan;

    //���ܿ�ѡ��Ŀ������ͺͷ�Χ
    public E_GridSelector_Type selectType;
    public int selectDistance;

    //���ܵ���Ӫ : ��ҵ��˻����, ��������
    [HideInInspector] public E_Camp_Type campType;
    //�ͷż��ܵĶ���
    [HideInInspector] public Character owner;
    //���ܵ��ͷ�λ��
    [HideInInspector] public GridPosition targetGridPos;

    public List<SkillImpactData> skillImpactDataList;

    public bool isCanSell;
    public int price;

    public SkillData Clone()
    {
        return (SkillData)this.MemberwiseClone();
    }
}
