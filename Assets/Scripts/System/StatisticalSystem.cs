using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticalSystem : BaseSystem<StatisticalSystem>
{
    // 过关数
    private int passLevel;
    public int PassLevel => passLevel;

    // 击杀数
    private int killCnt;
    public int KillCnt => killCnt;

    // 游戏时间
    private float gameTime;
    public float GameTime => gameTime;

    //地图种子
    private int randomSeed;
    public int RandomSeed => randomSeed;

    private StatisticalData statisticalData;

    protected override void Awake()
    {
        base.Awake();
        statisticalData = GameDataMgr.Instance.StatisticalData;
        passLevel = statisticalData.passLevel;
        killCnt = statisticalData.killCnt;
        gameTime = statisticalData.gameTime;
        randomSeed = statisticalData.randomSeed;
    }

    private void Start()
    {
        EventCenter.Instance.AddEventListener<E_Game_Result>(E_Event_Type.OnGameOver.ToString(), OnGameOver);
        EventCenter.Instance.AddEventListener<Character>(E_Event_Type.OnCharacterDestroy.ToString(), OnCharacterDestroy);
        EventCenter.Instance.AddEventListener(E_Event_Type.OnGameLoad.ToString(), OnGameLoad);
    }

    protected override void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<E_Game_Result>(E_Event_Type.OnGameOver.ToString(), OnGameOver);
        EventCenter.Instance.RemoveEventListener<Character>(E_Event_Type.OnCharacterDestroy.ToString(), OnCharacterDestroy);
        EventCenter.Instance.RemoveEventListener(E_Event_Type.OnGameLoad.ToString(), OnGameLoad);
    }

    private void Update()
    {
        if (!GameDataMgr.Instance.IsGameOver)
        {
            gameTime += Time.deltaTime;
        }
    }

    private void OnGameOver(E_Game_Result result)
    {
        if (result == E_Game_Result.GameVictory)
        {
            passLevel += 1;
            randomSeed = UnityEngine.Random.Range(0, (int)(1e9 + 7));
        }
    }

    private void OnCharacterDestroy(Character character)
    {
        if (character.GetCampType() != E_Camp_Type.Player)
        {
            killCnt += 1;
        }
    }

    public void AddRankInfo(string name)
    {
        RankInfo rankInfo = new RankInfo();
        rankInfo.Name = name;
        rankInfo.Layers = passLevel;
        rankInfo.time = (int)gameTime;

        GameDataMgr.Instance.AddRankData(rankInfo);
    }

    private void OnGameLoad()
    {
        StatisticalData statisticalData = new StatisticalData();
        statisticalData.isNotFirst = true;
        statisticalData.killCnt = killCnt;
        statisticalData.randomSeed = randomSeed;
        statisticalData.passLevel = passLevel;
        statisticalData.gameTime = gameTime;
        GameDataMgr.Instance.LoadStatisticalData(statisticalData);
    }
}
