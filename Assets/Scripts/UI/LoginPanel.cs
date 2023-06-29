using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    public Button btnStart;
    public Button btnContinue;
    public Button btnRank;
    public Button btnQuit;

    protected override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<LoginPanel>();
            GameDataMgr.Instance.InitGame();
            SceneMgr.Instance.LoadScene("GameScene", null);
        });
        btnContinue.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<LoginPanel>();
            SceneMgr.Instance.LoadScene("GameScene", null);
        });
        btnRank.onClick.AddListener(() =>
        {
            UIMgr.Instance.ShowPanel<RankPanel>();
        });
        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
