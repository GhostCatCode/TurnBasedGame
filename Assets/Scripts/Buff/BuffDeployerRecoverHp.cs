using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDeployerRecoverHp : BaseBuffDeployer
{
    public override void Init(BuffData data)
    {
        base.Init(data);
        EventCenter.Instance.AddEventListener<E_Camp_Type>(E_Event_Type.OnPrepareTurn.ToString(), TriggerBuff);
    }

    public override void MergeBuff(BuffData newdata)
    {
        data.buffDuration = Mathf.Max(data.buffDuration, newdata.buffDuration);
        data.buffValue = Mathf.Max(data.buffValue, newdata.buffValue);
    }

    public override void RemoveBuff()
    {
        EventCenter.Instance.RemoveEventListener<E_Camp_Type>(E_Event_Type.OnPrepareTurn.ToString(), TriggerBuff);
    }

    public void TriggerBuff(E_Camp_Type campType)
    {
        Character character = data.owner;
        if (character.Status.campType == campType)
        {
            character.RecoverHp(null, data.buffValue);

            // ≈–∂œbuffœ˚ ß
            data.buffDuration--;
            if (data.buffDuration <= 0)
            {
                character.BuffManager.RemoveBuff(data.buffId);
            }
        }
    }
}
