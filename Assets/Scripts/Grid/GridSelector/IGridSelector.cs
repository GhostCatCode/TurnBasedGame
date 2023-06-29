using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridSelector
{
    // 目标数组由小到大排列
    public List<GridPosition> GetValidGridPositions(GridPosition startPosition, int distance, E_Camp_Type campType);
}
