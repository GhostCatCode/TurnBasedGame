using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSelectorVisibleCanStandable : IGridSelector
{
    public List<GridPosition> GetValidGridPositions(GridPosition startPosition, int distance, E_Camp_Type campType)
    {
        Vector2 startWorldPos = GridSystem.Instance.GetWorldPosition(startPosition);
        List<GridPosition> gridPositions = new List<GridPosition>();

        for (int x = -distance; x <= distance; x++)
        {
            for (int y = -distance; y <= distance; y++)
            {
                GridPosition testGridPos = startPosition + new GridPosition(x, y);

                if (Mathf.Abs(x) + Mathf.Abs(y) > distance) continue;
                if (!GridSystem.Instance.IsCanStandable(testGridPos)) continue;

                Vector2 testWorldPos = GridSystem.Instance.GetWorldPosition(testGridPos);
                Vector2 shootDir = (testWorldPos - startWorldPos).normalized;

                if (Physics2D.Raycast(startWorldPos, shootDir, Vector2.Distance(startWorldPos, testWorldPos) - 0.8f,
                    GridSystem.Instance.GetWallLayerMask())) continue;

                gridPositions.Add(testGridPos);
            }
        }
        gridPositions.Sort((x, y) => GridPosition.Distance(startPosition, x) >= GridPosition.Distance(startPosition, y) ? 1 : -1);
        return gridPositions;
    }
}
