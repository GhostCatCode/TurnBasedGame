using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBagData
{
    //背包最大容量
    public int maxCnt;
    public List<ItemSaveData> list;

    // 金钱数
    public int goldCnt;

    public bool isNotFirst;
}

public class ItemSaveData
{
    public int id;
    public int cnt;
}
