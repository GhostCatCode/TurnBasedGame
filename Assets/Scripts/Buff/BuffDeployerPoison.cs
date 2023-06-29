using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDeployerPoison : BaseBuffDeployer
{
    public override void Init(BuffData data)
    {
        base.Init(data);
        EventCenter.Instance.AddEventListener<E_Camp_Type>(E_Event_Type.OnPrepareTurn.ToString(), TriggerBuff);
    }

    public override void MergeBuff(BuffData newdata)
    {
        data.buffDuration = Mathf.Max(data.buffDuration, newdata.buffDuration);
        data.buffValue = Mathf.Max(data.buffValue + newdata.buffValue);
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
            character.ChangeAnimation(E_CharacterAnimation_Type.Poison);
            character.Damage(null, data.buffValue);

            // ≈–∂œbuffœ˚ ß
            data.buffValue --;
            if (data.buffValue <= 0)
            {
                character.BuffManager.RemoveBuff(data.buffId);
            }
        }
    }
}
