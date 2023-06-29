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

    // ��Ʒ����
    [TextArea]
    public string introduction;

    public E_Item_Type type;
    public int MaxCnt;

    public int weaponId;
    public int SkillId;

    // ��������
    [HideInInspector] public ItemBag bag;
    // ����λ��
    [HideInInspector] public int bagIndex;
    // ��ǰ����
    public int cnt;

    public bool isCanSell;
    public int price;

    public ItemData Clone()
    {
        return (ItemData)this.MemberwiseClone();
    }
}
