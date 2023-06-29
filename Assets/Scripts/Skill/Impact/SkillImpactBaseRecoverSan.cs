using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillImpactBaseRecoverSan : ISkillImpact
{
    public IEnumerator DelorySkillImpact(SkillImpactData impactData, GridPosition targetPosition, Character owner)
    {
        List<GridPosition> list = SkillSystem.Instance.GetSkillImpactValidGridPosition(impactData, targetPosition, owner);

        for (int i = 0; i < list.Count; i++)
        {
            if (GridSystem.Instance.TryGetCharacterOnGridPosition(list[i], out Character targetCharacter))
            {
                int recoverSan =  impactData.ImpactValue;
                targetCharacter.RecoverSan(owner, recoverSan);
            }
        }
        yield return null;
    }
}
