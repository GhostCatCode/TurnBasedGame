using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillImpact
{
    public IEnumerator DelorySkillImpact(SkillImpactData impactData, GridPosition targetPosition, Character owner);
}
