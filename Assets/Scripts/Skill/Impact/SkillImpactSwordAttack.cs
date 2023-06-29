using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillImpactSwordAttack : ISkillImpact
{
    public IEnumerator DelorySkillImpact(SkillImpactData impactData, GridPosition targetPosition, Character owner)
    {
        owner.ChangeAnimation(E_CharacterAnimation_Type.Attack);
        yield return null;

        List<GridPosition> list = SkillSystem.Instance.GetSkillImpactValidGridPosition(impactData, targetPosition, owner);
        for (int i = 0; i < list.Count;i++)
        {
            if (GridSystem.Instance.TryGetCharacterOnGridPosition(list[i], out Character targetCharacter))
            {
                MusicMgr.Instance.PlaySound("attack", false, null);
                int damage = (int)(owner.Status.atk * impactData.ImpactDuration / 100 + impactData.ImpactValue);
                targetCharacter.Damage(owner, damage);
            }
        }
    }
}
