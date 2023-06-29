using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillImpactBaseMove : ISkillImpact
{
    public IEnumerator DelorySkillImpact(SkillImpactData impactData, GridPosition targetPos, Character owner)
    {
        List<GridPosition> list = AStarPathfindingSystem.Instance.FindPath(owner.GetGridPosition(), targetPos, owner.Status.campType, 0);
        float moveTime = 0.2f;

        Transform ownerTrans = owner.transform;
        for(int i = 0; i < list.Count; i++)
        {
            Vector2 startPos = owner.transform.position;
            Vector2 endPos = GridSystem.Instance.GetWorldPosition(list[i]);
            owner.UpdateFace(endPos);
            for (float timer = 0; timer < 1; timer += Time.deltaTime / moveTime)
            {
                ownerTrans.position = Vector2.Lerp(startPos, endPos, timer);
                yield return null;
            }
            ownerTrans.position = endPos;
        }
        owner.UpdateGridPosition();
    }
}
