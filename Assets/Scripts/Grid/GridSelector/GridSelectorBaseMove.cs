using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSelectorBaseMove : IGridSelector
{
    public List<GridPosition> GetValidGridPositions(GridPosition startPosition, int distance, E_Camp_Type campType)
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();
        

        HashSet<GridPosition> close = new HashSet<GridPosition>();
        Queue<GridPosition> check = new Queue<GridPosition>();

        check.Enqueue(startPosition);
        close.Add(startPosition);

        while (check.Count > 0)
        {
            GridPosition position = check.Dequeue();

            List<GridPosition> neibourPositions = position.Get4NeighborGridPosition();
            for (int i = 0; i < neibourPositions.Count; i++)
            {
                GridPosition testGridPosition = neibourPositions[i];

                if (close.Contains(testGridPosition)) continue;
                if (GridPosition.Distance(testGridPosition, startPosition) > distance)
                {
                    close.Add(testGridPosition);
                    continue;
                }

                // 有队友,可以通过,不可站立
                if (GridSystem.Instance.TryGetCharacterOnGridPosition(testGridPosition, out Character character))
                {
                    if (character.Status.campType == campType)
                    {
                        check.Enqueue(testGridPosition);
                        close.Add(testGridPosition);
                    }
                    continue;
                }
                if (!GridSystem.Instance.IsCanStandable(testGridPosition))
                {
                    close.Add(testGridPosition);
                    continue;
                }
                validGridPositions.Add(testGridPosition);
                check.Enqueue(testGridPosition);
                close.Add(testGridPosition);
            }
        }

        validGridPositions.Sort((x, y) => GridPosition.Distance(startPosition, x) >= GridPosition.Distance(startPosition, y) ? 1 : -1);
        return validGridPositions;
    }
}
