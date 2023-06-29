using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillImpactBaseAttack : ISkillImpact
{
    public IEnumerator DelorySkillImpact(SkillImpactData impactData, GridPosition targetPosition, Character owner)
    {
        List<GridPosition> list = SkillSystem.Instance.GetSkillImpactValidGridPosition(impactData, targetPosition, owner);
        for (int i = 0; i < list.Count; i++)
        {
            if (GridSystem.Instance.TryGetCharacterOnGridPosition(list[i], out Character targetCharacter))
            {
                int damage = (int)(owner.Status.atk * impactData.ImpactDuration / 100 + impactData.ImpactValue);
                targetCharacter.Damage(owner, damage);
            }
        }
        yield return null;
    }
}
