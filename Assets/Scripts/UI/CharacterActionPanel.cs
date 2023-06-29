using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterActionPanel : BasePanel
{
    public Transform skillListParent;

    public Button btnTurnEnd;

    public Character selectedCharacter;

    [SerializeField] private List<ButtonSkill> skillList;

    public StatusUI statusUI;

    protected override void Update()
    {
        base.Update();
        if ( TurnSystem.Instance?.CurrentTurn == E_Camp_Type.Player)
        {
            btnTurnEnd.interactable = true;
        }
        else
        {
            btnTurnEnd.interactable = false;
        }
    }

    protected override void Init()
    {
        // 技能
        EventCenter.Instance.AddEventListener<Character>(E_Event_Type.OnSelectCharacter.ToString(), OnSelectCharacter);
        EventCenter.Instance.AddEventListener(E_Event_Type.OnSkillChanged.ToString(), OnSkillChanged);
        skillListParent.gameObject.SetActive(false);
        statusUI.gameObject.SetActive(false);

        // 回合结束按钮
        btnTurnEnd.onClick.AddListener(() =>
        {
            TurnSystem.Instance.EndTurn(E_Camp_Type.Player);
        });

        // 技能栏隐藏
        for (int i = 0; i < skillList.Count; i++)
        {
            skillList[i].gameObject.SetActive(false);
        }
        skillListParent.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<Character>(E_Event_Type.OnSelectCharacter.ToString(), OnSelectCharacter);
        EventCenter.Instance.RemoveEventListener(E_Event_Type.OnSkillChanged.ToString(), OnSkillChanged);
    }

    private void OnSkillChanged()
    {
        UpdateSkills();
    }

    private void OnSelectCharacter(Character character)
    {
        selectedCharacter = character;
        UpdateSkills();
        UpdateStatusUI();
    }

    private void UpdateSkills()
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            skillList[i].gameObject.SetActive(false);
        }
        skillListParent.gameObject.SetActive(false);

        if (selectedCharacter != null)
        {
            List<SkillData> skills = selectedCharacter.SkillManager.Skills;
            if (skills != null && skills.Count > 0)
            {
                skillListParent.gameObject.SetActive(true);
                for (int i = 0; i < skills.Count; i++)
                {
                    if (i > skillList.Count - 1) return;
                    skillList[i].gameObject.SetActive(true);
                    skillList[i].SetInfo(skills[i]);
                }
            }
        }
    }

    private void UpdateStatusUI()
    {
        if (selectedCharacter != null)
        {
            statusUI.gameObject.SetActive(true);
            statusUI.Setup(selectedCharacter.Status);
        }
        else
        {
            statusUI.gameObject.SetActive(false);
        }
    }
}
