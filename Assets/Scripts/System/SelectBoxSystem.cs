using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

// 选择技能后, 显示可选择范围
public class SelectBoxSystem : BaseSystem<SelectBoxSystem>
{
    private List<GridPosition> validGridPositions;
    [SerializeField] private TileBase canSelectBoxTile;
    [SerializeField] private TileBase selectBoxTile;
    [SerializeField] private Tilemap canSelectBoxMap;
    [SerializeField] private Tilemap selectBoxMap;

    private void Start()
    {
        EventCenter.Instance.AddEventListener<SkillData>(E_Event_Type.OnSelectSkill.ToString(), OnSelectSkill);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventCenter.Instance.RemoveEventListener<SkillData>(E_Event_Type.OnSelectSkill.ToString(), OnSelectSkill);
    }

    private void Update()
    {
        if (validGridPositions != null && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GridPosition mouseGridPosition = GridSystem.Instance.GetGridPosition(mousePosition);

            if (validGridPositions.Contains(mouseGridPosition))
            {
                CharacterActionSystem.Instance.SelectedCharacter?.UpdateFace(mouseGridPosition);
                ShowSelectBox(new List<GridPosition> { mouseGridPosition });
            }
            else
            {
                ClearSelectBox();
            }
        }
    }

    public void ShowCanSelectBox(List<GridPosition> gridPositions)
    {
        ClearCanSelectBox();
        if (gridPositions != null)
        {
            for (int i = 0; i < gridPositions.Count; i++)
            {
                canSelectBoxMap.SetTile(GridSystem.Instance.GetTilePosition(gridPositions[i]), canSelectBoxTile);
            }
        }
    }

    public void ShowSelectBox(List<GridPosition> gridPositions)
    {
        ClearSelectBox();
        if (gridPositions != null)
        {
            for (int i = 0; i < gridPositions.Count; i++)
            {
                selectBoxMap.SetTile(GridSystem.Instance.GetTilePosition(gridPositions[i]), selectBoxTile);
            }
        }
    }

    public void ClearCanSelectBox()
    {
        if (canSelectBoxMap != null)
        {
            canSelectBoxMap.ClearAllTiles();
        }
    }

    public void ClearSelectBox()
    {
        if (selectBoxMap != null)
        {
            selectBoxMap.ClearAllTiles();
        }
    }

    private void OnSelectSkill(SkillData data)
    {
        if (data != null && data.skillId != 0)
        {
            validGridPositions = SkillSystem.Instance.GetSkillValidGridPosition(data);
            ShowCanSelectBox(validGridPositions);
        }
        else
        {
            validGridPositions = null;
            ClearCanSelectBox();
            ClearSelectBox();
        }
    }

}
