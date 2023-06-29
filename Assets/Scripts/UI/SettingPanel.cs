using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button btnSetting;
    public Button btnBack;

    protected override void Init()
    {
        btnSetting.onClick.AddListener(() =>
        {
            UIMgr.Instance.ShowPanel<SettingMusicPanel>();
        });

        btnBack.onClick.AddListener(() =>
        {
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                UIMgr.Instance.ShowPanel<TipPanel>(E_UI_Layer.System, (tip) =>
                {
                    tip.Setup("确认返回主菜单？", null, () =>
                    {
                        GameDataMgr.Instance.SetIsGameOver(true);
                        SceneMgr.Instance.LoadScene("MainMenuScene", null);
                    });
                });
            }
            else
            {
                UIMgr.Instance.ShowPanel<TipPanel>(E_UI_Layer.System, (tip) =>
                {
                    tip.Setup("确认退出游戏？", null, () =>
                    {
                        Application.Quit();
                    });
                });
            }
        });
    }
}
