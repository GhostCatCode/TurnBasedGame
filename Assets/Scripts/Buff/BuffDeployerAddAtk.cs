using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDeployerAddAtk : BaseBuffDeployer
{
    public override void Init(BuffData data)
    {
        base.Init(data);

        // ¼Ó¹¥»÷
        data.owner.Status.atk += data.buffValue;

        EventCenter.Instance.AddEventListener<E_Camp_Type>(E_Event_Type.OnTurnEnd.ToString(), TriggerBuff);
    }

    public override void MergeBuff(BuffData newdata)
    {
        data.owner.Status.atk -= data.buffValue;

        data.buffDuration = Mathf.Max(data.buffDuration, newdata.buffDuration);
        data.buffValue = Mathf.Max(data.buffValue, newdata.buffValue);

        data.owner.Status.atk += data.buffValue;
    }

    public override void RemoveBuff()
    {
        // ¼õ¹¥»÷
        data.owner.Status.atk -= data.buffValue;

        EventCenter.Instance.RemoveEventListener<E_Camp_Type>(E_Event_Type.OnTurnEnd.ToString(), TriggerBuff);
    }

    public void TriggerBuff(E_Camp_Type campType)
    {
        Character character = data.owner;
        if (character.Status.campType == campType)
        {

            // ÅÐ¶ÏbuffÏûÊ§
            data.buffDuration--;
            if (data.buffDuration <= 0)
            {
                character.BuffManager.RemoveBuff(data.buffId);
            }
        }
    }
}
