using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolData
{
    public GameObject fatherObj;
    public Stack<GameObject> poolStack;

    public PoolData(GameObject obj, GameObject poolObj)
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.SetParent(poolObj.transform);
        poolStack = new Stack<GameObject>();
    }

    public void PushObj(GameObject obj)
    {
        poolStack.Push(obj);
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(fatherObj.transform);
    }

    public GameObject GetObj()
    {
        GameObject obj = poolStack.Pop();
        obj.gameObject.SetActive(true);
        obj.transform.SetParent(null);
        return obj;
    }
}

public class PoolMgr : BaseManager<PoolMgr>
{
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

    private GameObject poolObj;

    public void GetObj(string name, UnityAction<GameObject> callBack)
    {
        if (poolDic.ContainsKey(name) && poolDic[name].poolStack.Count > 0)
        {
            callBack(poolDic[name].GetObj());
        }
        else
        {
            ResMgr.Instance.LoadAsync<GameObject>(name, (o) =>
            {
                o.name = name;
                callBack(o);
            });
        }
    }

    public void PushObj(string name, GameObject obj)
    {
        if (poolObj == null)
            poolObj = new GameObject("Pool");

        if (!poolDic.ContainsKey(name))
        {
            poolDic.Add(name, new PoolData(obj, poolObj));
        }
        poolDic[name].PushObj(obj);
    }

    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
