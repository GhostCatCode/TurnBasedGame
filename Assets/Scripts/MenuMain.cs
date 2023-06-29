using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    private void Start()
    {
        UIMgr.Instance.ShowPanel<LoginPanel>();
        UIMgr.Instance.ShowPanel<SettingPanel>();
        MusicMgr.Instance.PlayerBkMusic("BkMusic1");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIMgr.Instance.HidePanel<LoginPanel>();
            SceneManager.LoadScene(1);
        }
    }
}
