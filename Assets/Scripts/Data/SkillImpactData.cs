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

    // ���̶ܹ��˺� / buff��ֵ
    public int ImpactValue;

    // ���ܱ����˺� / buff����ʱ��
    public int ImpactDuration;
}
