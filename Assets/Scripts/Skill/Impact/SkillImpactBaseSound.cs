using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillImpactBaseSound : ISkillImpact
{
    public IEnumerator DelorySkillImpact(SkillImpactData impactData, GridPosition targetPosition, Character owner)
    {
        yield return null;
        MusicMgr.Instance.PlaySound(impactData.str, false, null);
    }
}
