using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillImpactBasePrefab : ISkillImpact
{
    public IEnumerator DelorySkillImpact(SkillImpactData impactData, GridPosition targetPosition, Character owner)
    {
        List<GridPosition> list = SkillSystem.Instance.GetSkillImpactValidGridPosition(impactData, targetPosition, owner);
        for (int i = 0; i < list.Count; i++)
        {
            GameObject obj = ResMgr.Instance.Load<GameObject>("SkillImpactPrefab/" + impactData.str);
            obj.transform.position = GridSystem.Instance.GetWorldPosition(list[i]);
            GameObject.Destroy(obj, 1f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
