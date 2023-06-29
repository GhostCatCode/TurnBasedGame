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

    // 武器ID
    public int weaponId;

    //装备种类
    public E_Weapon_Type weaponType;

    // 可以装备的角色
    public E_Character_Type characterType;

    // 装备后的角色图片
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
