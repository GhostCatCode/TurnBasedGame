using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterListData
{
    // ��ɫ�б��������
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
