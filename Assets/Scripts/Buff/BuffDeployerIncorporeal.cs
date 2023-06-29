using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDeployerIncorporeal : BaseBuffDeployer
{
    public override void Init(BuffData data)
    {
        base.Init(data);
        EventCenter.Instance.AddEventListener<OnCharacterDamageArgs>(E_Event_Type.OnCharacterDamage.ToString(), TriggerBuff);
        EventCenter.Instance.AddEventListener<E_Camp_Type>(E_Event_Type.OnPrepareTurn.ToString(), TriggerBuff2);
    }

    public override void MergeBuff(BuffData newdata)
    {
        data.buffDuration = data.buffDuration + newdata.buffDuration;
        data.buffValue = Mathf.Max(data.buffValue, newdata.buffValue);
    }

    public override void RemoveBuff()
    {
        base.RemoveBuff();
        EventCenter.Instance.RemoveEventListener<OnCharacterDamageArgs>(E_Event_Type.OnCharacterDamage.ToString(), TriggerBuff);
        EventCenter.Instance.RemoveEventListener<E_Camp_Type>(E_Event_Type.OnPrepareTurn.ToString(), TriggerBuff2);
    }

    private void TriggerBuff(OnCharacterDamageArgs arg)
    {
        Character character = data.owner;
        if (arg.victim == character && arg.value > 1)
        {
            arg.value = 1;
        }
    }

    private void TriggerBuff2(E_Camp_Type campType)
    {
        Character character = data.owner;
        if (character.Status.campType == campType)
        {

            // ≈–∂œbuffœ˚ ß
            data.buffDuration--;
            if (data.buffDuration <= 0)
            {
                character.BuffManager.RemoveBuff(data.buffId);
            }
        }
    }
}
