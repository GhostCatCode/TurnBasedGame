using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterStatus
{
    public string name;
    public string imgName;
    public E_Camp_Type campType;
    public E_Character_Type characterType;
    public bool isPlayerController;
    public int viewDistance;

    //װ������������
    public E_Weapon_Type weaponType;
    public int weaponId;

    //�������ֵ
    public int maxHp;
    public int hp;

    //����ж���
    public int maxAc;
    public int ac;

    //�������ֵ
    public int maxSan;
    public int san;

    //������
    public int atk;
    //������
    public int def;

    // ��ʱ������
    public int TempAtk;
    // ��ʱ�˺�����
    public float TempAtkMul;

    public CharacterStatus Clone()
    {
        return (CharacterStatus)this.MemberwiseClone();
    }
}
