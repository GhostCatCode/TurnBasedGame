using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreateSystem : BaseSystem<GridCreateSystem>
{
    System.Random prng;
    public GridData CreateGripData(int seed)
    {

        GridData gridData= new GridData();
        prng = new System.Random(seed);
        int level = GameDataMgr.Instance.StatisticalData.passLevel;
        int weight = prng.Next(level * 2 + 15, level * 2 + 15);
        int height = prng.Next(level * 2 + 10, level * 2 + 15);
        gridData.size = new Vector2Int(weight, height);
        gridData.cellSize = new Vector2(1f, 1f);
        gridData.GridInfoList = new List<GridInfo>();


        //创建房间大小位置
        List<Room> roomList = CreateRoom(gridData.size);
        // 创建走廊
        List<GridInfo> corridor = CreateCorridor(roomList);
        gridData.GridInfoList.AddRange(corridor);

        for (int i = 0; i < roomList.Count; i++)
        {
            Room room = roomList[i];
            Vector2Int origin = room.origin;
            Vector2Int size = room.size;

            //生成房间内物体
            if (prng.NextDouble() < 0.5f)
            {
                DecorateRoom(room, E_Room_Type.ChestRoom);
            }
            else
            {
                DecorateRoom(room, E_Room_Type.EnemyRoom);
            }
        }

        //生成玩家出生房间
        int playerRoomNum = prng.Next(0, roomList.Count);
        Room playerRoom = roomList[playerRoomNum];
        DecorateRoom(playerRoom, E_Room_Type.playerRoom);
        gridData.playerGridPosList = GetPlayerGridPosList(playerRoom);

        // 生成终点
        int testCnt = 0;
        int destinationRoomNum = prng.Next(0, roomList.Count);
        while (destinationRoomNum == playerRoomNum && testCnt< 1000)
        {
            destinationRoomNum = prng.Next(0, roomList.Count);
            testCnt++;
        }
        DecorateRoom(roomList[destinationRoomNum], E_Room_Type.DestinationRoom);


        //保存房间
        for (int i = 0; i < roomList.Count; i ++)
        {
            gridData.GridInfoList.AddRange(roomList[i].GridInfoList);
        }
        return gridData;
    }

    //按类型装饰房间
    public void DecorateRoom(Room room, E_Room_Type roomType)
    {
        Vector2Int origin = room.origin;
        Vector2Int size = room.size;

        room.GridInfoList.Clear();
        // 生成空地
        for (int j = 0; j < size.x; j++)
        {
            for (int k = 0; k < size.y; k++)
            {
                room.GridInfoList.Add(new GridInfo(
                    new GridPosition(origin.x + j, origin.y + k), E_Grid_Type.Space));
            }
        }

        if (roomType == E_Room_Type.ChestRoom)
        {
            room.roomType = E_Room_Type.ChestRoom;
            int chestNum = prng.Next(1, room.Area / 36 + 2);
            for (int j = 0; j < chestNum; j++)
            {
                GridPosition gridPosition = RandomGridPosition(room);
                room.GridInfoList.Find(x => x.gridPosition == gridPosition).gridType = E_Grid_Type.Chest;
            }
            int EnemyNum = prng.Next(1, room.Area / 64 + 1);
            for (int j = 0; j < EnemyNum; j++)
            {
                GridPosition gridPosition = RandomGridPosition(room);
                room.GridInfoList.Find(x => x.gridPosition == gridPosition).gridType = E_Grid_Type.Enemy;
            }
        }
        else if (roomType == E_Room_Type.EnemyRoom)
        {
            room.roomType = E_Room_Type.EnemyRoom;
            int chestNum = prng.Next(0, room.Area / 64 + 1);
            for (int j = 0; j < chestNum; j++)
            {
                GridPosition gridPosition = RandomGridPosition(room);
                room.GridInfoList.Find(x => x.gridPosition == gridPosition).gridType = E_Grid_Type.Chest;
            }
            int EnemyNum = prng.Next(2, room.Area / 64 + 2);
            for (int j = 0; j < EnemyNum; j++)
            {
                GridPosition gridPosition = RandomGridPosition(room);
                room.GridInfoList.Find(x => x.gridPosition == gridPosition).gridType = E_Grid_Type.Enemy;
            }
        }
        else if (roomType == E_Room_Type.DestinationRoom)
        {
            room.roomType = E_Room_Type.DestinationRoom;
            int EnemyNum = prng.Next(2, room.Area / 64 + 2);
            for (int j = 0; j < EnemyNum; j++)
            {
                GridPosition gridPosition = RandomGridPosition(room);
                room.GridInfoList.Find(x => x.gridPosition == gridPosition).gridType = E_Grid_Type.Enemy;
            }
            GridPosition DestinationGridPos = RandomGridPosition(room);
            room.GridInfoList.Find(x => x.gridPosition == DestinationGridPos).gridType = E_Grid_Type.Destination;
        }
    }

    //创建房间
    private List<Room> CreateRoom(Vector2Int gridSize)
    {
        List<Room> list = new List<Room>();
        Queue<Room> queue= new Queue<Room>();
        queue.Enqueue(new Room(new Vector2Int(0, 0), gridSize));

        while (queue.Count > 0)
        {
            Room room = queue.Dequeue();
            Vector2Int size = room.size;

            if (prng.NextDouble() < 0.5f)
            {
                if (size.x >= 12)
                {
                    TryHorizontalCutRoom(room, queue);
                }
                else if (size.y >= 12)
                {
                    TryVerticalCutRoom(room, queue);
                }
                else
                {
                    list.Add(room);
                }
            }
            else
            {
                if (size.y >= 12)
                {
                    TryVerticalCutRoom(room, queue);
                }
                else if (size.x >= 12)
                {
                    TryHorizontalCutRoom(room, queue);
                }
                else
                {
                    list.Add(room);
                }
            }
        }
        return list;
    }

    // 尝试切割房间
    public void TryHorizontalCutRoom(Room room, Queue<Room> queue)
    {
        Vector2Int origin = room.origin;
        Vector2Int size = room.size;

        int sizeX = prng.Next(6, size.x - 6);
        queue.Enqueue(new Room(origin, new Vector2Int(sizeX, size.y)));
        queue.Enqueue(new Room(origin + new Vector2Int(sizeX + 1, 0), new Vector2Int(size.x - sizeX - 1, size.y)));
    }

    public void TryVerticalCutRoom(Room room, Queue<Room> queue)
    {
        Vector2Int origin = room.origin;
        Vector2Int size = room.size;

        int sizeY = prng.Next(6, size.y - 6);
        queue.Enqueue(new Room(origin, new Vector2Int(size.x, sizeY)));
        queue.Enqueue(new Room(origin + new Vector2Int(0, sizeY + 1), new Vector2Int(size.x, size.y - sizeY - 1)));
    }

    // 创建过道/门
    private List<GridInfo> CreateCorridor(List<Room> roomList)
    {
        List<GridInfo> corridor = new List<GridInfo>();
        for (int i = 0; i < roomList.Count; i++)
        {
            for (int j = 0; j < roomList.Count; j ++)
            {
                Room a = roomList[i];
                Room b = roomList[j];

                if (i == j) continue;
                if (a.adjoiningRooms.Contains(b)) continue;

                if ((a.MaxX + 1 == b.MinX) || (b.MaxX + 1 == a.MinX))
                {
                    if (Mathf.Max(a.MinY, b.MinY) <= Mathf.Min(a.MaxY, b.MaxY) - 2)
                    {
                        a.adjoiningRooms.Add(b);
                        b.adjoiningRooms.Add(a);

                        int x = (a.MaxX > b.MaxX) ? b.MaxX : a.MaxX;
                        int MinY = Mathf.Max(a.MinY, b.MinY);
                        int MaxY = Mathf.Min(a.MaxY, b.MaxY);
                        int y = (MinY + MaxY) / 2;

                        corridor.Add(new GridInfo(new GridPosition(x, y), E_Grid_Type.VerticalDoor));
                        corridor.Add(new GridInfo(new GridPosition(x, y + 1), E_Grid_Type.Space));
                        corridor.Add(new GridInfo(new GridPosition(x, y - 1), E_Grid_Type.Space));
                    }
                }
                else if (a.MaxY + 1 == b.MinY || b.MaxY + 1 == a.MinY)
                {
                    if (Mathf.Max(a.MinX, b.MinX) <= Mathf.Min(a.MaxX, b.MaxX) - 2)
                    {
                        a.adjoiningRooms.Add(b);
                        b.adjoiningRooms.Add(a);

                        int y = (a.MaxY > b.MaxY) ? b.MaxY : a.MaxY;
                        int MinX = Mathf.Max(a.MinX, b.MinX);
                        int MaxX = Mathf.Min(a.MaxX, b.MaxX);
                        int x = (MinX + MaxX) / 2;

                        corridor.Add(new GridInfo(new GridPosition(x, y), E_Grid_Type.HorizontalDoor));
                        corridor.Add(new GridInfo(new GridPosition(x + 1, y), E_Grid_Type.Space));
                        corridor.Add(new GridInfo(new GridPosition(x - 1, y), E_Grid_Type.Space));
                    }
                }
            }
        }
        return corridor;
    }

    // 是否是相邻房间
    public bool IsAdjoining(Room a, Room b)
    {
        if ((a.MaxX + 1 == b.MinX) || (b.MaxX + 1 == a.MinX))
        {
            if (Mathf.Max(a.MinY, b.MinY) <= Mathf.Min(a.MaxY, b.MaxY) - 2)
                return true;
        }
        else if (a.MaxY + 1 == b.MinY || b.MaxY + 1 == a.MinY)
        {
            if (Mathf.Max(a.MinX, b.MinX) <= Mathf.Min(a.MaxX, b.MaxX) - 2)
                return true;
        }
        return false;
    }

    //随机获得一个位置
    public GridPosition RandomGridPosition(Room room)
    {
        Vector2Int origin = room.origin;
        Vector2Int size = room.size;

        int x = prng.Next(2, size.x - 2);
        int y = prng.Next(2, size.y - 2);
        return new GridPosition(origin.x + x, origin.y + y);
    }

    // 获取玩家生成点
    public List<GridPosition> GetPlayerGridPosList(Room room)
    {
        List<GridPosition> list = new List<GridPosition>();
        GridPosition gridPosition = new GridPosition(room.Center.x, room.Center.y);
        list.Add(gridPosition);
        list.AddRange(gridPosition.Get8NeighborGridPosition());
        return list;
    }
}
