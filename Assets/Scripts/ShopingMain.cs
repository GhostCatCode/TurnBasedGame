using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopingMain : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(GameDataMgr.Instance.PlayerBagData.list.Count);
    }
}
