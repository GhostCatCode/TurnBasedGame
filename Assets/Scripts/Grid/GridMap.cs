using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class GridMap
{
    private Vector2Int size;
    private Vector2 cellSize;
    private GridObject[,] map;

    public GridMap(Vector2Int size, Vector2 cellsize)
    {
        this.size = size;
        this.cellSize = cellsize;
        map = new GridObject[size.x, size.y];
    }

    public GridMap(GridData gridData) : this(gridData.size, gridData.cellSize)
    {
        for (int i = 0; i < gridData.GridInfoList.Count; i++)
        {
            GridInfo gridInfo = gridData.GridInfoList[i];
            GridPosition gridPosition = gridInfo.gridPosition;
            this.map[gridPosition.x, gridPosition.y] = new GridObject(this, gridPosition);
            //更新基础数据,物品和敌人由GridSystem更新
        }
    }

    public Vector3Int GetTilePosition(GridPosition gridPosition)
    {
        return new Vector3Int(gridPosition.x, gridPosition.y, 0);
    }

    public Vector2 GetWorldPosition(GridPosition gridPosition)
    {
        Vector2 worldPosition = new Vector2(gridPosition.x * cellSize.x,
            gridPosition.y * cellSize.y);
        return worldPosition;
    }

    public GridPosition GetGridPosition(Vector2 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x / cellSize.x);
        int y = Mathf.RoundToInt(worldPosition.y / cellSize.y);

        GridPosition gridPosition = new GridPosition(x, y);
        return gridPosition;
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        if(IsInRangeGridPosition(gridPosition))
        {
            return map[gridPosition.x, gridPosition.y];
        }
        return null;
    }

    public bool IsInRangeGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < size.x && gridPosition.y >= 0 && gridPosition.y < size.y;
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return GetGridObject(gridPosition) != null;
    }

    public bool IsSideWall(GridPosition gridPosition)
    {
        if (!IsValidGridPosition(gridPosition)) return false;

        List<GridPosition> list = gridPosition.Get8NeighborGridPosition();
        for(int i = 0; i < list.Count; i++)
        {
            if (!IsValidGridPosition(list[i])) return true;
        }
        return false;
    }
}
