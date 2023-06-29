using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSystem: BaseSystem<GridSystem>
{
    private List<GridPosition> playerGridPosList;
    [SerializeField] private TileBase backGroundTile;
    [SerializeField] private TileBase unStandableTile;

    [SerializeField] private Tilemap backGroundMap;
    [SerializeField] private Tilemap unStandableMap;

    private GridMap gridMap;

    [SerializeField] private LayerMask wallLayerMask;

    // �Ƿ����վ�� : ��վ������û��
    public bool IsCanStandable(GridPosition gridPosition)
    {
        GridObject gridObject = gridMap.GetGridObject(gridPosition);
        if (gridObject != null)
        {
            return (!gridObject.IsUnStandable) && (gridObject.GetCharacter() == null);
        }
        return false;
    }

    // �����Ƿ��������
    public void SetIsUnStandable(GridPosition gridPosition, bool isUnStandable)
    {
        GridObject gridObject = gridMap.GetGridObject(gridPosition);
        if (gridObject != null)
        {
            gridObject.SetIsUnStandable(isUnStandable);
            if(isUnStandable)
            {
                unStandableMap.SetTile(gridMap.GetTilePosition(gridPosition), unStandableTile);
            }
            else
            {
                unStandableMap.SetTile(gridMap.GetTilePosition(gridPosition), null);
            }
        }
    }

    public LayerMask GetWallLayerMask()
    {
        return wallLayerMask;
    }
    public List<GridPosition> GetPlayerGridPosList()
    {
        return playerGridPosList;
    }

    #region ���û�����Ʒ

    public bool TryGetInteractableOnGridPosition(GridPosition gridPosition, out IInteractable interactable)
    {
        interactable = GetInteractableOnGridPosition(gridPosition);
        return interactable != null;
    }

    public IInteractable GetInteractableOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridMap.GetGridObject(gridPosition);
        if (gridObject != null)
        {
            return gridObject.GetInteractable();
        }
        return null;
    }

    public void SetInteractableOnGridPosition(GridPosition gridPosition, IInteractable interactable)
    {
        gridMap.GetGridObject(gridPosition).SetInteractable(interactable);
    }

    public void ClearInteractableOnGridPosition(GridPosition gridPosition)
    {
        gridMap.GetGridObject(gridPosition).SetInteractable(null);
    }

    #endregion

    #region ���ý�ɫ
    // ���Ի�ý�ɫ : �Ϸ����н�ɫ
    public bool TryGetCharacterOnGridPosition(GridPosition gridPosition, out Character character)
    {
        character = GetCharacterOnGridPosition(gridPosition);
        return character != null;
    }

    public Character GetCharacterOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridMap.GetGridObject(gridPosition);
        if (gridObject != null)
        {
            return gridObject.GetCharacter();
        }
        return null;
    }

    public void SetCharacterOnGridPosition(GridPosition gridPosition, Character character)
    {
        gridMap.GetGridObject(gridPosition).SetCharacter(character);
    }

    public void ClearCharacterOnGridPosition(GridPosition gridPosition)
    {
        gridMap.GetGridObject(gridPosition).SetCharacter(null);
    }

    // �ƶ���ɫ
    public void MoveCharacterOnGridPosition(GridPosition startPos, GridPosition endPos, Character character)
    {
        if (GetCharacterOnGridPosition(startPos) == character)
        {
            ClearCharacterOnGridPosition(startPos);
        }
        SetCharacterOnGridPosition(endPos, character);
    }

    #endregion

    public Vector3Int GetTilePosition(GridPosition gridPosition) => gridMap.GetTilePosition(gridPosition);
    public GridPosition GetGridPosition(Vector2 worldPosition) => gridMap.GetGridPosition(worldPosition);
    public Vector2 GetWorldPosition(GridPosition gridPosition) => gridMap.GetWorldPosition(gridPosition);
    public bool IsValidGridPosition(GridPosition gridPosition) => gridMap.IsValidGridPosition(gridPosition);

    #region ������ͼ
    public void CreateGrid(GridData gridData)
    {
        //����GridSystem
        gridMap = new GridMap(gridData);
        playerGridPosList = gridData.playerGridPosList;
        // ��������
        for (int i = 0; i < gridData.GridInfoList.Count; i++)
        {
            backGroundMap.SetTile(gridMap.GetTilePosition(gridData.GridInfoList[i].gridPosition), backGroundTile);
        }

        //������ǽ
        for (int x = 0; x < gridData.size.x; x++)
        {
            for (int y = 0; y < gridData.size.y; y++)
            {
                GridPosition gridPosition = new GridPosition(x, y);
                if (gridMap.IsSideWall(gridPosition))
                {
                    unStandableMap.SetTile(gridMap.GetTilePosition(gridPosition),unStandableTile);
                    gridMap.GetGridObject(gridPosition).SetIsUnStandable(true);
                }
            }
        }

        //��������ͽ�ɫ
        for (int i = 0; i < gridData.GridInfoList.Count; i++)
        {
            GridInfo gridInfo = gridData.GridInfoList[i];
            if (gridInfo.gridType == E_Grid_Type.HorizontalDoor)
            {
                ResMgr.Instance.LoadAsync<GameObject>("SceneItem/HorizontalDoor", obj =>
                {
                    obj.transform.position = GetWorldPosition(gridInfo.gridPosition);
                });
            }
            else if (gridInfo.gridType == E_Grid_Type.VerticalDoor)
            {
                ResMgr.Instance.LoadAsync<GameObject>("SceneItem/VerticalDoor", obj =>
                {
                    obj.transform.position = GetWorldPosition(gridInfo.gridPosition);
                });
            }
            else if (gridInfo.gridType == E_Grid_Type.Chest)
            {
                ResMgr.Instance.LoadAsync<GameObject>("SceneItem/Chest", obj =>
                {
                    obj.transform.position = GetWorldPosition(gridInfo.gridPosition);
                });
            }
            else if (gridInfo.gridType == E_Grid_Type.Destination)
            {
                ResMgr.Instance.LoadAsync<GameObject>("SceneItem/Destination", obj =>
                {
                    obj.transform.position = GetWorldPosition(gridInfo.gridPosition);
                });
            }
            else if (gridInfo.gridType == E_Grid_Type.Enemy)
            {
                CharacterSystem.Instance.RandomCreateEnemy(gridInfo.gridPosition);
            }
        }

        // ������Ҳٿؽ�ɫ
        CharacterSystem.Instance.CreatePlayerCharacter();
    }

    // ��յ�ͼ
    public void ClearGrid()
    {
        gridMap = null;
        backGroundMap.ClearAllTiles();
        unStandableMap.ClearAllTiles();
    }
    #endregion
}
