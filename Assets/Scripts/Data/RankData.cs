using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankData
{
    public List<RankInfo> list;
    public bool isNotFirst;
}

public class RankInfo
{
    public string Name;

    // ���ز���
    public int Layers;

    // ����ʱ��
    public int time;
}
