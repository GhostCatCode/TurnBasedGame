using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuffDeployer
{
    public BuffData data;

    // ��ͬbuff����
    public abstract void MergeBuff(BuffData newdata);

    // �Ƴ�buff
    public virtual void RemoveBuff()
    {
        // TODO: ����������Ƴ�buff
    }

    // ��ʼ�� ���¼�
    public virtual void Init(BuffData data)
    {
        this.data = data;
    }
}
