using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridSelector
{
    // Ŀ��������С��������
    public List<GridPosition> GetValidGridPositions(GridPosition startPosition, int distance, E_Camp_Type campType);
}
