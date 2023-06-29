using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillImpactCharge : ISkillImpact
{
    public IEnumerator DelorySkillImpact(SkillImpactData impactData, GridPosition targetPosition, Character owner)
    {
        float moveTime = 0.2f;
        Transform ownerTrans = owner.transform;
        Vector2 startPos = owner.transform.position;
        Vector2 endPos = GridSystem.Instance.GetWorldPosition(targetPosition);
        owner.UpdateFace(endPos);
        for (float timer = 0; timer < 1; timer += Time.deltaTime / moveTime)
        {
            ownerTrans.position = Vector2.Lerp(startPos, endPos, timer);
            yield return null;
        }
        owner.UpdateGridPosition();
    }
}
