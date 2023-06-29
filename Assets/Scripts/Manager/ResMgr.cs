using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResMgr : BaseManager<ResMgr>
{
    public T Load<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else
            return res;
    }

    public void LoadAsync<T>(string name, UnityAction<T> callBack) where T : Object
    {
        MonoMgr.Instance.StartCoroutine(ReallyLoadAsync(name, callBack));
    }

    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callBack) where T : Object
    {
        ResourceRequest req = Resources.LoadAsync<T>(name);
        yield return req;

        if (req.asset is GameObject)
            callBack(GameObject.Instantiate(req.asset) as T);
        else
            callBack(req.asset as T);
    }
}
