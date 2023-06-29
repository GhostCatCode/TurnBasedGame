using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuffDeployer
{
    public BuffData data;

    // 相同buff叠加
    public abstract void MergeBuff(BuffData newdata);

    // 移除buff
    public virtual void RemoveBuff()
    {
        // TODO: 从玩家身上移除buff
    }

    // 初始化 加事件
    public virtual void Init(BuffData data)
    {
        this.data = data;
    }
}
