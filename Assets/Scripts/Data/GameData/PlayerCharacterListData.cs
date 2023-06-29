using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterListData
{
    // 角色列表最大容量
    public int maxCnt;
    public List<PlayerCharacterData> list;

    public bool isNotFirst;
}

[Serializable]
public class PlayerCharacterData
{
    public CharacterStatus status;
    public List<int> skillSaveData;
}
