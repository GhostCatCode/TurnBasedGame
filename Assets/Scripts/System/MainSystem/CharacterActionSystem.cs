using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class CharacterActionSystem : BaseSystem<CharacterActionSystem>
{
    private Character selectedCharacter;
    public Character SelectedCharacter => selectedCharacter;
    private SkillData selectedSkill;
    public SkillData SelectedSkill => selectedSkill;

    private bool isBusy;

    private void Start()
    {
        EventCenter.Instance.AddEventListener<E_Camp_Type>("OnTurnStart", OnTurnStart);
        EventCenter.Instance.AddEventListener<E_Camp_Type>("OnTurnEnd", OnTurnEnd);

        UIMgr.Instance.ShowPanel<CharacterActionPanel>();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventCenter.Instance.RemoveEventListener<E_Camp_Type>("OnTurnStart", OnTurnStart);
        EventCenter.Instance.RemoveEventListener<E_Camp_Type>("OnTurnEnd", OnTurnEnd);

        UIMgr.Instance.HidePanel<CharacterActionPanel>();
    }

    private void Update()
    {
        if (isBusy) return;

        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GridPosition mouseGridPosition = GridSystem.Instance.GetGridPosition(mousePosition);

            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (TryHandleSelectedSkill()) return;
            if (TryHandleCharacterSelection()) return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryHandleSelecteNextCharacter();
        }
    }

    //尝试释放技能
    private bool TryHandleSelectedSkill()
    {
        if (selectedSkill == null || selectedSkill.skillId == 0) return false;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GridPosition mouseGridPosition = GridSystem.Instance.GetGridPosition(mousePosition);
        if (!SkillSystem.Instance.IsCanSpendToTakeSkill(selectedSkill, mouseGridPosition)) return false;

        //成功释放技能: 设置忙碌状态, 隐藏提示(?), 取消技能选择, 释放技能
        SetBusy();
        selectedCharacter.DelorySkill(selectedSkill, mouseGridPosition, () =>
        {
            selectedCharacter.SetIsActionable(false);
            ClearSelectedCharacter();
            ClearBusy();
        });
        return true;
    }

    //尝试使用选择的道具 : 由道具调用,需要判空
    public void TryHandleSelectedItem(ItemData data)
    {
        if (isBusy) return;
        if (data == null || data.cnt <= 0) return;
        if (selectedCharacter == null) return;
        //TODO:尝试使用道具

        switch (data.type)
        {
            case E_Item_Type.expendable:
                break;
            case E_Item_Type.weapon:
                WeaponData weapon = ItemSystem.Instance.GetWeaponData(data.weaponId);
                if ( weapon != null && selectedCharacter.Status.characterType == weapon.characterType )
                {
                    UIMgr.Instance.ShowPanel<TipPanel>(E_UI_Layer.System, (tip) =>
                    {
                        tip.Setup("确认装备" + data.name + "?", null, () =>
                        {
                            selectedCharacter.EquipWeapons(weapon);
                            data.cnt--;
                            EventCenter.Instance.EventTrigger(E_Event_Type.OnItemChanged.ToString());
                        });
                    });
                }
                break;
            case E_Item_Type.Skill:
                MusicMgr.Instance.PlaySound("drink", false, null);
                SetBusy();
                SkillData skillData = SkillSystem.Instance.GetSkillData(data.SkillId);
                skillData.owner = selectedCharacter;
                skillData.campType = selectedCharacter.Status.campType;
                selectedCharacter.DelorySkill(skillData, selectedCharacter.GetGridPosition(), ClearBusy);
                data.cnt--;
                EventCenter.Instance.EventTrigger(E_Event_Type.OnItemChanged.ToString());
                break;
        }

    }

    private void TryHandleSelecteNextCharacter()
    {
        List<Character> list = CharacterSystem.Instance.PlayerList;
        List<Character> canList = new List<Character>();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].IsActionable)
            {
                canList.Add(list[i]);
            }
        }

        if (selectedCharacter != null )
        {
            for(int i = 0; i < canList.Count; i++)
            {
                if (canList[i] == selectedCharacter)
                {
                    if ( i == canList.Count - 1 )
                    {
                        SetSelectedCharacter(canList[0]);
                        CameraSystem.Instance.SetCameraPosition(canList[0].transform.position);
                    }
                    else
                    {
                        SetSelectedCharacter(canList[i + 1]);
                        CameraSystem.Instance.SetCameraPosition(canList[i + 1].transform.position);
                    }
                    break;
                }
            }
        }
        else
        {
            if ( canList.Count > 0 )
            {
                SetSelectedCharacter(canList[0]);
                CameraSystem.Instance.SetCameraPosition(canList[0].transform.position);
            }
        }
    }

    //尝试更换角色
    private bool TryHandleCharacterSelection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GridPosition mouseGridPosition = GridSystem.Instance.GetGridPosition(mousePosition);

        if (GridSystem.Instance.TryGetCharacterOnGridPosition(mouseGridPosition, out Character character))
        {
            if (character.Status.isPlayerController)
            {
                if (character == selectedCharacter)
                {
                    ClearSelectedCharacter();
                }
                else
                {
                    SetSelectedCharacter(character);
                }

                return true;
            }
        }
        return false;
    }

    //设置选择的技能
    public void SetSelectedSkill(SkillData data)
    {
        if (data == null || SkillSystem.Instance.IsCanSpendToTakeSkill(data))
        {
            selectedSkill = data;
            EventCenter.Instance.EventTrigger<SkillData>(E_Event_Type.OnSelectSkill.ToString(), selectedSkill);
        }
    }

    public void ClearSelectedSkill()
    {
        selectedSkill = null;
        EventCenter.Instance.EventTrigger<SkillData>(E_Event_Type.OnSelectSkill.ToString(), selectedSkill);
    }

    // 设置选择的角色
    public void SetSelectedCharacter(Character character)
    {
        ClearSelectedSkill();
        selectedCharacter = character;
        EventCenter.Instance.EventTrigger<Character>(E_Event_Type.OnSelectCharacter.ToString(), selectedCharacter);

        if (character.IsActionable)
        {
            SetSelectedSkill(character.SkillManager.Skills[0]);
        }
    }

    public void ClearSelectedCharacter()
    {
        ClearSelectedSkill();
        selectedCharacter = null;
        EventCenter.Instance.EventTrigger<Character>(E_Event_Type.OnSelectCharacter.ToString(), selectedCharacter);
    }

    private void ClearBusy()
    {
        isBusy = false;
    }

    private void SetBusy()
    {
        isBusy = true;
    }

    private void OnTurnStart(E_Camp_Type currentTurn)
    {
        if (currentTurn == E_Camp_Type.Player)
        {
            ClearBusy();
        }
    }

    private void OnTurnEnd(E_Camp_Type currentTurn)
    {
        if (currentTurn == E_Camp_Type.Player)
        {
            ClearSelectedCharacter();
            SetBusy();
        }
    }
}
