using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BuffDeployerCounterattack : BaseBuffDeployer
{
    public override void Init(BuffData data)
    {
        base.Init(data);
        EventCenter.Instance.AddEventListener<OnCharacterDamageArgs>(E_Event_Type.OnCharacterDamage.ToString(), TriggerBuff);
    }

    public override void MergeBuff(BuffData newdata)
    {
        data.buffDuration = data.buffDuration + newdata.buffDuration;
        data.buffValue = Mathf.Max(data.buffValue , newdata.buffValue);
    }

    public override void RemoveBuff()
    {
        base.RemoveBuff();
        EventCenter.Instance.RemoveEventListener<OnCharacterDamageArgs>(E_Event_Type.OnCharacterDamage.ToString(), TriggerBuff);
    }

    private void TriggerBuff(OnCharacterDamageArgs arg)
    {
        Character character = data.owner;
        if (arg.victim == character && arg.maker != null && GridPosition.Distance(character.GetGridPosition(), arg.maker.GetGridPosition()) <= 2)
        {
            MonoMgr.Instance.StartCoroutine(Counterattack(character, arg.maker));
            data.buffDuration--;
            if (data.buffDuration <= 0)
            {
                character.BuffManager.RemoveBuff(data.buffId);
            }
        }
    }

    private IEnumerator Counterattack(Character owner, Character target)
    {
        yield return new WaitForSeconds(0.1f);

        owner.UpdateFace(target.transform.position);
        owner.ChangeAnimation(E_CharacterAnimation_Type.Attack);
        target.Damage(owner, data.buffValue);
    }
}
