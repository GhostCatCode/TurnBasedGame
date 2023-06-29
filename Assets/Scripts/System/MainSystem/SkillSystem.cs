using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SkillSystem : BaseSystem<SkillSystem>
{
    [SerializeField] private SkillData_SO skillDataSO;

    // 通过Id获得技能
    public SkillData GetSkillData(int SkillId)
    {
        return skillDataSO.SkillDataList.Find(x => x.skillId == SkillId)?.Clone();
    }

    public List<SkillData> GetShopSkillList(int cnt)
    {
        List<SkillData> skillDatas = new List<SkillData>();
        while(skillDatas.Count < cnt)
        {
            int index = Random.Range(0, skillDataSO.SkillDataList.Count);
            SkillData skillData = skillDataSO.SkillDataList[index];
            if (skillData.isCanSell)
            {
                skillDatas.Add(skillData);
            }
        }
        return skillDatas;
    }

    // 判定技能是否可以在目标点花费释放
    public bool IsCanSpendToTakeSkill(SkillData data, GridPosition targetPos)
    {
        return IsCanSpendToTakeSkill(data) && IsValidSkillGridPosition(data, targetPos);
    }

    // 判定技能是否可以花费释放
    public bool IsCanSpendToTakeSkill(SkillData data)
    {
        if (data == null) return false;

        CharacterStatus status = data.owner.Status;
        if (!data.owner.IsActionable) return false;
        if (data.coolRemain > 0 || data.costAc > status.ac || data.costSan > status.san) return false;
        if (data.weaponType != E_Weapon_Type.None && data.weaponType != status.weaponType) return false;

        return true;
    }

    // 判定目标点是否可以使用技能
    public bool IsValidSkillGridPosition(SkillData data, GridPosition targetPos)
    {
        List<GridPosition> gridPositions = GetSkillValidGridPosition(data);
        if (gridPositions.Contains(targetPos))
        {
            return true;
        }
        return false;
    }

    //得到技能可以释放的位置
    public List<GridPosition> GetSkillValidGridPosition(SkillData data)
    {
        if (data == null) return null;
        IGridSelector gridSelector = GridSelectorFactory.GetGridSelector(data.selectType);
        GridPosition gridPosition = data.owner.GetGridPosition();
        return gridSelector.GetValidGridPositions(gridPosition, data.selectDistance, data.campType);
    }

    // 得到技能效果目标位置
    public List<GridPosition> GetSkillImpactValidGridPosition(SkillImpactData skillImpact, GridPosition targetPos, Character owner)
    {
        if (skillImpact == null) return null;
        skillImpact.ImpactCount = Mathf.Max(skillImpact.ImpactCount, 1);

        IGridSelector gridSelector = GridSelectorFactory.GetGridSelector(skillImpact.selectType);
        List<GridPosition> validGridPositions = gridSelector.GetValidGridPositions(targetPos, skillImpact.ImpactDistance, owner.Status.campType);
        if (skillImpact.ImpactCount < validGridPositions.Count)
        {
            validGridPositions = validGridPositions.GetRange(0, skillImpact.ImpactCount);
        }
        return validGridPositions;
    }
}
