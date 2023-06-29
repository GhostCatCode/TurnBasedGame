using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GridPosition : IEquatable<GridPosition>
{
    public static GridPosition up = new GridPosition(0, 1);
    public static GridPosition down = new GridPosition(0, -1);
    public static GridPosition left = new GridPosition(1, 0);
    public static GridPosition right = new GridPosition(-1, 0);

    public int x;
    public int y;

    public GridPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               y == position.y;
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public override string ToString()
    {
        return "GridPosition(" + x + ", " + y + ")";
    }

    public static bool operator ==(GridPosition left, GridPosition right)
    {
        return left.x == right.x && left.y == right.y;
    }

    public static bool operator !=(GridPosition left, GridPosition right)
    {
        return !(left == right);
    }

    public static GridPosition operator +(GridPosition left, GridPosition right)
    {
        return new GridPosition(left.x + right.x, left.y + right.y);
    }

    public static GridPosition operator -(GridPosition left, GridPosition right)
    {
        return new GridPosition(left.x - right.x, left.y - right.y);
    }

    public static int Distance(GridPosition left, GridPosition right)
    {
        return Mathf.Abs(left.x - right.x) + Mathf.Abs(left.y - right.y);
    }

    public List<GridPosition> Get8NeighborGridPosition()
    {
        List<GridPosition> neighbors = new List<GridPosition>();
        for (int x = -1; x <= 1; x ++)
        {
            for (int y = -1; y <= 1; y ++)
            {
                if (x == 0 && y == 0) continue;
                neighbors.Add(this + new GridPosition(x, y));
            }
        }
        return neighbors;
    }

    public List<GridPosition> Get4NeighborGridPosition()
    {
        List<GridPosition> neighbors = new List<GridPosition>();
        neighbors.Add(this + GridPosition.up);
        neighbors.Add(this + GridPosition.down);
        neighbors.Add(this + GridPosition.left);
        neighbors.Add(this + GridPosition.right);
        return neighbors;
    }
}
