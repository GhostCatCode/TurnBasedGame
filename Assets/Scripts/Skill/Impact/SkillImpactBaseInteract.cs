using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillImpactBaseInteract : ISkillImpact
{
    public IEnumerator DelorySkillImpact(SkillImpactData impactData, GridPosition targetPosition, Character owner)
    {
        List<GridPosition> list = SkillSystem.Instance.GetSkillImpactValidGridPosition(impactData, targetPosition, owner);
        for (int i = 0; i < list.Count; i++)
        {
            if (GridSystem.Instance.TryGetInteractableOnGridPosition(list[i], out IInteractable interactable))
            {
                interactable.Interact(owner);
            }
        }
        yield return null;
    }
}
