using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameMain : BaseSystem<GameMain>
{
    public int seed;

    

    private void Start()
    {
        GameDataMgr.Instance.SetIsGameOver(false);
        GridData gridData = GridCreateSystem.Instance.CreateGripData(GameDataMgr.Instance.StatisticalData.randomSeed);
        GridSystem.Instance.CreateGrid(gridData);
        MusicMgr.Instance.PlayerBkMusic("BkMusic2");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameFailure();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameVictory();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerBagSystem.Instance.GetGold(100);
        }
    }

    public void GameVictory()
    {
        GameDataMgr.Instance.SetIsGameOver(true);
        EventCenter.Instance.EventTrigger<E_Game_Result>(E_Event_Type.OnGameOver.ToString(), E_Game_Result.GameVictory);
        UIMgr.Instance.ShowPanel<GameVictoryPanel>();
    }

    public void GameFailure()
    {
        GameDataMgr.Instance.SetIsGameOver(true);
        EventCenter.Instance.EventTrigger<E_Game_Result>(E_Event_Type.OnGameOver.ToString(), E_Game_Result.GameFailure);
        UIMgr.Instance.ShowPanel<GameFailurePanel>();
    }
}
