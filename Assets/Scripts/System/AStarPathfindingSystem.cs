using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfindingSystem : BaseSystem<AStarPathfindingSystem>
{
    public List<GridPosition> FindPath(GridPosition startGridPosition, GridPosition endGridPosition, E_Camp_Type campType, int EulerDistance)
    {
        List<PathNode> openList = new List<PathNode>();
        List<GridPosition> closeList = new List<GridPosition>();

        PathNode startNode = new PathNode(startGridPosition);

        startNode.SetGCost(0);
        startNode.SetHCost(GridPosition.Distance(startGridPosition, endGridPosition));
        startNode.CalculateFCost();
        openList.Add(startNode);
        closeList.Add(startNode.GetGridPosition());

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostPathNode(openList);
            if (GridPosition.Distance(currentNode.GetGridPosition(), endGridPosition) <= EulerDistance)
            {
                return CalculatePath(currentNode);
            }

            openList.Remove(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closeList.Contains(neighbourNode.GetGridPosition()))
                {
                    continue;
                }

                //没人或者有队友就可以走
                if (!GridSystem.Instance.IsCanStandable(neighbourNode.GetGridPosition()))
                {
                    if (GridSystem.Instance.TryGetCharacterOnGridPosition(neighbourNode.GetGridPosition(), out Character character))
                    {
                        if (character.Status.campType != campType)
                        {
                            closeList.Add(neighbourNode.GetGridPosition());
                            continue;
                        }
                    }
                    else
                    {
                        closeList.Add(neighbourNode.GetGridPosition());
                        continue;
                    }
                }

                int tentativeGCost = currentNode.GetGCost() +
                    GridPosition.Distance(currentNode.GetGridPosition(), neighbourNode.GetGridPosition());

                if (tentativeGCost < neighbourNode.GetGCost())
                {
                    neighbourNode.SetCameFromPathNode(currentNode);
                    neighbourNode.SetGCost(tentativeGCost);
                    neighbourNode.SetHCost(GridPosition.Distance(neighbourNode.GetGridPosition(), endGridPosition));
                    neighbourNode.CalculateFCost();
                }

                openList.Add(neighbourNode);
                closeList.Add(neighbourNode.GetGridPosition());
            }
        }
        return new List<GridPosition>();
    }

    private PathNode GetLowestFCostPathNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostPathNode = pathNodeList[0];
        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].GetFCost() < lowestFCostPathNode.GetFCost())
            {
                lowestFCostPathNode = pathNodeList[i];
            }
        }
        return lowestFCostPathNode;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        GridPosition gridPosition = currentNode.GetGridPosition();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                if (Mathf.Abs(x) + Mathf.Abs(y) > 1) continue;

                GridPosition neibourPosition = gridPosition + new GridPosition(x, y);
                if (!GridSystem.Instance.IsValidGridPosition(neibourPosition)) continue;

                neighbourList.Add(new PathNode(neibourPosition));
            }
        }
        return neighbourList;
    }

    private List<GridPosition> CalculatePath(PathNode endNode)
    {
        List<GridPosition> pathGridPositionList = new List<GridPosition>();
        pathGridPositionList.Add(endNode.GetGridPosition());
        PathNode currentNode = endNode;
        while (currentNode.GetCameFromNode() != null)
        {
            pathGridPositionList.Add(currentNode.GetCameFromNode().GetGridPosition());
            currentNode = currentNode.GetCameFromNode();
        }
        pathGridPositionList.Reverse();
        return pathGridPositionList;
    }
}
