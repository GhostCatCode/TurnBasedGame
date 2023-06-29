using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridData
{
    public Vector2Int size;
    public Vector2 cellSize;
    public List<GridPosition> playerGridPosList; //玩家生成地点
    public List<GridInfo> GridInfoList;
}

public class Room
{
    public Vector2Int origin;
    public Vector2Int size;
    public E_Room_Type roomType;
    public List<Room> adjoiningRooms = new List<Room>(); //相邻的房间
    public List<GridInfo> GridInfoList = new List<GridInfo>();  //房间格子

    public Room() { }
    public Room(Vector2Int origin, Vector2Int size)
    {
        this.origin = origin;
        this.size = size;
    }

    public int MinX => origin.x;
    public int MinY => origin.y;
    public int MaxX => origin.x + size.x;
    public int MaxY => origin.y + size.y;
    public int Area => size.x * size.y;
    public Vector2Int Center => new Vector2Int(origin.x + size.x / 2, origin.y + size.y / 2);
}

[Serializable]
public class GridInfo
{
    public GridPosition gridPosition;
    public E_Grid_Type gridType;

    public GridInfo(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }
    public GridInfo(GridPosition gridPosition, E_Grid_Type gridType) : this(gridPosition)
    {
        this.gridType = gridType;
    }
}
