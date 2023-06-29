using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    public string name;
    public int itemId;
    public string spriteName;

    // 物品介绍
    [TextArea]
    public string introduction;

    public E_Item_Type type;
    public int MaxCnt;

    public int weaponId;
    public int SkillId;

    // 所属背包
    [HideInInspector] public ItemBag bag;
    // 所属位置
    [HideInInspector] public int bagIndex;
    // 当前数量
    public int cnt;

    public bool isCanSell;
    public int price;

    public ItemData Clone()
    {
        return (ItemData)this.MemberwiseClone();
    }
}
