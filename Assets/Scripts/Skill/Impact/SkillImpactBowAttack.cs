using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillImpactBowAttack : ISkillImpact
{
    public IEnumerator DelorySkillImpact(SkillImpactData impactData, GridPosition targetPosition, Character owner)
    {
        owner.ChangeAnimation(E_CharacterAnimation_Type.Attack2);
        yield return null;

        List<GridPosition> list = SkillSystem.Instance.GetSkillImpactValidGridPosition(impactData, targetPosition, owner);
        for (int i = 0; i < list.Count; i++)
        {
            if (GridSystem.Instance.TryGetCharacterOnGridPosition(list[i], out Character targetCharacter))
            {
                PoolMgr.Instance.GetObj("SkillImpactPrefab/Arrow", (obj) =>
                {
                    MusicMgr.Instance.PlaySound("swish_2", false, null);
                    SkillImpactPrefabArrow arrow = obj.GetComponent<SkillImpactPrefabArrow>();
                    arrow.Setup(owner.transform.position, targetCharacter.transform.position, () =>
                    {
                        MusicMgr.Instance.PlaySound("swish_4", false, null);
                        int damage = (int)(owner.Status.atk * impactData.ImpactDuration / 100 + impactData.ImpactValue);
                        targetCharacter.Damage(owner, damage);
                    });
                });
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
