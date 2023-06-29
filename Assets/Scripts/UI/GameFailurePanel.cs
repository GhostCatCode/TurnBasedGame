using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFailurePanel : BasePanel
{
    [SerializeField] private InputField inputField;
    [SerializeField] private Button btnRecord;
    [SerializeField] private Button btnBack;

    protected override void Init()
    {
        btnRecord.onClick.AddListener(() =>
        {
            if (inputField.text == string.Empty)
            {
                StatisticalSystem.Instance.AddRankInfo("Œﬁ√˚ œ");
            }
            else
            {
                StatisticalSystem.Instance.AddRankInfo(inputField.text);
            }

            PoolMgr.Instance.Clear();
            GameDataMgr.Instance.InitGame();
            UIMgr.Instance.HidePanel<GameFailurePanel>();
            SceneMgr.Instance.LoadScene("MainMenuScene", null);
        });

        btnBack.onClick.AddListener(() =>
        {
            PoolMgr.Instance.Clear();
            GameDataMgr.Instance.InitGame();
            UIMgr.Instance.HidePanel<GameFailurePanel>();
            SceneMgr.Instance.LoadScene("MainMenuScene", null);
        });
    }
}
