using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GridPosition gridPosition;
    private int gCost;
    private int hCost;
    private int fCost;
    private PathNode cameFromNode;

    public PathNode(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
        SetGCost(int.MaxValue);
        SetHCost(0);
        CalculateFCost();
        ResetCameFromPathNode();
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    public int GetGCost()
    {
        return gCost;
    }
    public int GetHCost()
    {
        return hCost;
    }
    public int GetFCost()
    {
        return fCost;
    }

    public void SetGCost(int gCost)
    {
        this.gCost = gCost;
    }

    public void SetHCost(int hCost)
    {
        this.hCost = hCost;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void ResetCameFromPathNode()
    {
        cameFromNode = null;
    }

    public void SetCameFromPathNode(PathNode pathNode)
    {
        cameFromNode = pathNode;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public PathNode GetCameFromNode()
    {
        return cameFromNode;
    }

    public static int Distance(PathNode left, PathNode right)
    {
        return GridPosition.Distance(left.gridPosition, right.gridPosition);
    }
}
