using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour
{
    public event UnityAction updateEvent;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        updateEvent?.Invoke();

        if (Input.GetMouseButtonDown(0))
        {
            MusicMgr.Instance.PlaySound("click", false, null);
        }
    }

    public void AddUpdateListener(UnityAction action)
    {
        updateEvent += action;
    }

    public void RemoveUpdateListener(UnityAction action)
    {
        updateEvent -= action;
    }
}
