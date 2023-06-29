using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuffData
{
    public string buffName;
    public int buffId;

    public bool isNegative;
    public E_buff_Type type;

    public int buffValue;
    public int buffDuration;
    // buffËùÓÐÕß
    [HideInInspector] public Character owner;

    public void Init(int buffValue, int buffDuration, Character owner)
    {
        this.buffValue = buffValue;
        this.buffDuration = buffDuration;
        this.owner = owner;
    }

    public BuffData Clone()
    {
        return (BuffData)this.MemberwiseClone();
    }
}
