using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSelectorSelf : IGridSelector
{
    public List<GridPosition> GetValidGridPositions(GridPosition startPosition, int distance, E_Camp_Type campType)
    {
        return new List<GridPosition>() { startPosition };
    }
}
