using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BuffDeployerAttackRecovery : BaseBuffDeployer
{
    public override void Init(BuffData data)
    {
        base.Init(data);
        EventCenter.Instance.AddEventListener<OnCharacterDamageArgs>(E_Event_Type.OnCharacterDamage.ToString(), TriggerBuff);
    }

    public override void MergeBuff(BuffData newdata)
    {
        data.buffDuration = Mathf.Max(data.buffDuration, newdata.buffDuration);
        data.buffValue = Mathf.Max(data.buffValue, newdata.buffValue);
    }

    public override void RemoveBuff()
    {
        base.RemoveBuff();
        EventCenter.Instance.RemoveEventListener<OnCharacterDamageArgs>(E_Event_Type.OnCharacterDamage.ToString(), TriggerBuff);
    }

    private void TriggerBuff(OnCharacterDamageArgs arg)
    {
        Character character = data.owner;
        if (arg.maker == character)
        {
            character.RecoverHp(character, data.buffValue);

            data.buffDuration--;
            if (data.buffDuration <= 0)
            {
                character.BuffManager.RemoveBuff(data.buffId);
            }
        }
    }
}
