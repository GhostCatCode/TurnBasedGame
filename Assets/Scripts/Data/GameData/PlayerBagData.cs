using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBagData
{
    //�����������
    public int maxCnt;
    public List<ItemSaveData> list;

    // ��Ǯ��
    public int goldCnt;

    public bool isNotFirst;
}

public class ItemSaveData
{
    public int id;
    public int cnt;
}
