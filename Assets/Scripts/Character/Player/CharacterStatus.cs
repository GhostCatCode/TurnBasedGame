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

    //装备的武器类型
    public E_Weapon_Type weaponType;
    public int weaponId;

    //最大生命值
    public int maxHp;
    public int hp;

    //最大行动点
    public int maxAc;
    public int ac;

    //最大理智值
    public int maxSan;
    public int san;

    //攻击力
    public int atk;
    //防御力
    public int def;

    // 临时攻击力
    public int TempAtk;
    // 临时伤害倍率
    public float TempAtkMul;

    public CharacterStatus Clone()
    {
        return (CharacterStatus)this.MemberwiseClone();
    }
}
