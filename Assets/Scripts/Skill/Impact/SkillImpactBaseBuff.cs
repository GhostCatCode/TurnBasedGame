using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillImpactBaseBuff : ISkillImpact
{
    public IEnumerator DelorySkillImpact(SkillImpactData impactData, GridPosition targetPosition, Character owner)
    {
        List<GridPosition> list = SkillSystem.Instance.GetSkillImpactValidGridPosition(impactData, targetPosition, owner);
        for(int i = 0; i < list.Count; i++)
        {
            if (GridSystem.Instance.TryGetCharacterOnGridPosition(list[i], out Character targetCharacter))
            {
                BuffData buffData = BuffSystem.Instance.GetBuffData(impactData.buffId);
                buffData.Init(impactData.ImpactValue, impactData.ImpactDuration, targetCharacter);
                targetCharacter.BuffManager.AddBuff(buffData);
            }
        }
        yield return null;
    }
}
