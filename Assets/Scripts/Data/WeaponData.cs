using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponData
{
    public WeaponData()
    {
        weaponType = E_Weapon_Type.None;
        characterType = E_Character_Type.None;
    }

    public string name;

    // ����ID
    public int weaponId;

    //װ������
    public E_Weapon_Type weaponType;

    // ����װ���Ľ�ɫ
    public E_Character_Type characterType;

    // װ����Ľ�ɫͼƬ
    public string imgName;

    public int AddAtk;
    public int AddDef;
    public int AddMaxHP;
    public int AddMaxSan;

    public WeaponData Clone()
    {
        return (WeaponData)this.MemberwiseClone();
    }
}
