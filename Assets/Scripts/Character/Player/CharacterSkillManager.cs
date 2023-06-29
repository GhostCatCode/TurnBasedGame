using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSkillManager : MonoBehaviour
{
    // 加载数据
    [SerializeField] private List<int> skillSaveData;
    public List<int> SkillSaveData => skillSaveData;

    private Character character;
    private List<SkillData> skills;
    public List<SkillData> Skills => skills;
    private UnityAction onComplete;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        // 加载数据
        skills = new List<SkillData>();
        for(int i = 0; i < skillSaveData.Count; i ++)
        {
            Skills.Add(SkillSystem.Instance.GetSkillData(skillSaveData[i]));
        }

        for (int i = 0; i < Skills.Count; i++)
        {
            InitSkill(Skills[i]);
        }

        EventCenter.Instance.AddEventListener<E_Camp_Type>(E_Event_Type.OnPrepareTurn.ToString(), OnPrepareTurn);
    }

    private void InitSkill(SkillData data)
    {
        data.owner = character;
        data.campType = character.Status.campType;
        data.coolRemain = 0;
    }

    public void AddSkill(SkillData data)
    {
        Skills.Add(data);
        skillSaveData.Add(data.skillId);
        InitSkill(data);
        EventCenter.Instance.EventTrigger(E_Event_Type.OnChangedCharacter.ToString());
    }

    //释放技能
    public void DelorySkill(SkillData data, GridPosition targetGridPos, UnityAction onComplete)
    {
        this.onComplete = onComplete;
        data.owner.Status.ac -= data.costAc;
        data.owner.Status.san -= data.costSan;
        data.coolRemain += data.coolTime;
        StartCoroutine(DelorySkill(data, targetGridPos));
    }

    // 随机释放技能
    public void RandomDelorySkill(UnityAction onComplete)
    {
        // 获取目标
        Character targetCharacter = character.GetTargetCharacter();

        if (targetCharacter == null)
        {
            SkillData skill = Skills[0];
            List<GridPosition> testGridPosList = SkillSystem.Instance.GetSkillValidGridPosition(skill);
            GridPosition gridPos = testGridPosList[UnityEngine.Random.Range(0, testGridPosList.Count)];
            character.UpdateFace(gridPos);
            this.onComplete = onComplete;
            skill.owner.Status.ac -= skill.costAc;
            skill.owner.Status.san -= skill.costSan;
            skill.coolRemain += skill.coolTime;
            StartCoroutine(DelorySkill(skill, gridPos));
            return;
        }

        // 获取到达目标的路径
        GridPosition targetGridPos = targetCharacter.GetGridPosition();
        List<GridPosition> aStarFinding = 
            AStarPathfindingSystem.Instance.FindPath(character.GetGridPosition(), targetCharacter.GetGridPosition(), character.GetCampType(), 1);
        
        // 最终选择的技能和位置
        SkillData selectedSkill = null;
        GridPosition selectedGridPos = new GridPosition(0, 0);
        int distance = 1000;

        // 开始尝试
        for (int i = 0; i < Skills.Count; i ++)
        {
            SkillData testSkill = Skills[i];
            if (!SkillSystem.Instance.IsCanSpendToTakeSkill(testSkill)) continue;

            List<GridPosition> testGridPosList = SkillSystem.Instance.GetSkillValidGridPosition(testSkill);
            if (testGridPosList == null) continue;

            if (testSkill.type == E_Skill_Type.Move)
            {
                for (int j = 0; j < testGridPosList.Count; j++)
                {
                    GridPosition testGridPos = testGridPosList[j];
                    if (aStarFinding.Contains(testGridPos))
                    {
                        if (selectedSkill == null || GridPosition.Distance(testGridPos, targetGridPos) < distance)
                        {
                            selectedSkill = testSkill;
                            selectedGridPos = testGridPos;
                            distance = GridPosition.Distance(testGridPos, targetGridPos);
                        }
                        else if (GridPosition.Distance(testGridPos, targetGridPos) == distance &&
                            UnityEngine.Random.Range(0, 1f) > 0.5f)
                        {
                            selectedSkill = testSkill;
                            selectedGridPos = testGridPos;
                            distance = GridPosition.Distance(testGridPos, targetGridPos);
                        }
                    }
                }
            }
            else if (testSkill.type == E_Skill_Type.Attack)
            {
                for (int j = 0; j < testGridPosList.Count; j++)
                {
                    GridPosition testGridPos = testGridPosList[j];
                    if (selectedSkill == null || selectedSkill.type == E_Skill_Type.Move)
                    {
                        selectedSkill = testSkill;
                        selectedGridPos = testGridPos;
                    }
                    else if (UnityEngine.Random.Range(0, 1f) > 0.5f)
                    {
                        selectedSkill = testSkill;
                        selectedGridPos = testGridPos;
                    }
                }
            }
            else if (testSkill.type == E_Skill_Type.Other)
            {
                for (int j = 0; j < testGridPosList.Count; j++)
                {
                    GridPosition testGridPos = testGridPosList[j];
                    if (selectedSkill == null)
                    {
                        selectedSkill = testSkill;
                        selectedGridPos = testGridPos;
                    }
                    else if (UnityEngine.Random.Range(0, 1f) > 0.8f)
                    {
                        selectedSkill = testSkill;
                        selectedGridPos = testGridPos;
                    }
                }
            }
        }

        // 释放技能
        if (selectedSkill != null)
        {
            character.UpdateFace(selectedGridPos);
            this.onComplete = onComplete;
            selectedSkill.owner.Status.ac -= selectedSkill.costAc;
            selectedSkill.owner.Status.san -= selectedSkill.costSan;
            selectedSkill.coolRemain += selectedSkill.coolTime;
            StartCoroutine(DelorySkill(selectedSkill, selectedGridPos));
        }
        else
        {
            targetCharacter = null;
            onComplete?.Invoke();
            return;
        }
    }

    public IEnumerator DelorySkill(SkillData data, GridPosition targetGridPos)
    {
        if (data != null)
        {
            for (int i = 0; i < data.skillImpactDataList.Count; i++)
            {
                SkillImpactData impactData = data.skillImpactDataList[i];
                ISkillImpact impact = SkillImpactFactory.GetSkillImpact(impactData.type);
                yield return impact.DelorySkillImpact(impactData, targetGridPos, data.owner);
            }
        }
        onComplete?.Invoke();
    }

    //技能冷却
    public void CoolTimeDown()
    {
        for (int i = 0; i < Skills.Count; i ++)
        {
            if (Skills[i].coolRemain > 0)
            {
                Skills[i].coolRemain--;
            }
        }
    }

    public void Setup(List<int> list)
    {
        this.skillSaveData = new List<int>(list);
    }

    private void OnPrepareTurn(E_Camp_Type turnType)
    {
        if (turnType == character.Status.campType)
        {
            CoolTimeDown();
        }
    }
}
