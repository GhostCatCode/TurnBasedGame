using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr : BaseManager<SceneMgr>
{
    public void LoadScene(string name, UnityAction fun)
    {
        SceneManager.LoadScene(name);
        fun?.Invoke();
    }

    public void LoadSceneAsyn(string name, UnityAction fun)
    {
        MonoMgr.Instance.StartCoroutine(ReallyLoadSceneAsyn(name, fun));
    }

    private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction fun)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        while (!ao.isDone)
        {
            EventCenter.Instance.EventTrigger("SceneUpdateProgress", ao.progress);
            yield return ao.progress;
        }

        PoolMgr.Instance.Clear();
        fun?.Invoke();
    }
}
