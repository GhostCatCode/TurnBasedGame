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

    // 闯关层数
    public int Layers;

    // 闯关时间
    public int time;
}
