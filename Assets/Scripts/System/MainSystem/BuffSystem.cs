using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : BaseSystem<BuffSystem>
{
    [SerializeField] private BuffData_SO buffDataSO;

    public BuffData GetBuffData(int buffId)
    {
        return buffDataSO.BuffDataList.Find(x => x.buffId == buffId)?.Clone();
    }
}
