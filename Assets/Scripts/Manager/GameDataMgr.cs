using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr : BaseManager<GameDataMgr>
{
    private bool isGameOver;
    public bool IsGameOver => isGameOver;

    private MusicData musicData;
    public MusicData MusicData => musicData;

    private PlayerBagData playerBagData;
    public PlayerBagData PlayerBagData => playerBagData;

    private PlayerCharacterListData playerCharacterListData;
    public PlayerCharacterListData PlayerCharacterListData => playerCharacterListData;

    private RankData rankData;
    public RankData RankData => rankData;

    private StatisticalData statisticalData;
    public StatisticalData StatisticalData => statisticalData;

    //读取数据
    public GameDataMgr()
    {
        // 初始化音乐数据
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        if (!musicData.isNotFirst)
        {
            InitMusicData();
        }
        // 初始化排行榜数据
        rankData = JsonMgr.Instance.LoadData<RankData>("RankData");
        if (!rankData.isNotFirst)
        {
            InitRankData();
        }
        // 初始化背包数据
        playerBagData = JsonMgr.Instance.LoadData<PlayerBagData>("PlayerBagData");
        if (!playerBagData.isNotFirst)
        {
            InitPlayerBagData();
        }
        // 初始化玩家角色列表数据
        playerCharacterListData = JsonMgr.Instance.LoadData<PlayerCharacterListData>("PlayerCharacterListData");
        if (!playerCharacterListData.isNotFirst)
        {
            InitPlayerCharacterListData();
        }
        // 初始化统计数据
        statisticalData = JsonMgr.Instance.LoadData<StatisticalData>("StatisticalData");
        if (!statisticalData.isNotFirst)
        {
            InitStatisticalData();
        }
    }

    // 重置游戏数据
    public void InitGame()
    {
        InitPlayerBagData();
        InitPlayerCharacterListData();
        InitStatisticalData();
    }


    public void InitMusicData()
    {
        musicData.isNotFirst = true;
        musicData.isPlayerMusic = true;
        musicData.isPlayerSound = true;
        musicData.MusicVolume = 0.6f;
        musicData.SoundVolume = 0.6f;
    }
    public void InitRankData()
    {
        rankData.isNotFirst = true;
        rankData.list = new List<RankInfo>();
    }
    public void InitPlayerBagData()
    {
        playerBagData.isNotFirst = true;
        playerBagData.maxCnt = 8;
        playerBagData.goldCnt = 100;
        playerBagData.list = new List<ItemSaveData>();
    }
    public void InitPlayerCharacterListData()
    {
        playerCharacterListData.isNotFirst = true;
        playerCharacterListData.maxCnt = 4;
        playerCharacterListData.list = new List<PlayerCharacterData>();
    }
    public void InitStatisticalData()
    {
        statisticalData.isNotFirst = true;
        statisticalData.randomSeed = Random.Range(0, (int)(1e9 + 7));
        statisticalData.passLevel = 0;
        statisticalData.killCnt = 0;
        statisticalData.gameTime = 0f;
    }

    public void LoadPlayerBagData(PlayerBagData playerBagData)
    {
        this.playerBagData = playerBagData;
    }
    public void LoadPlayerCharacterListData(PlayerCharacterListData playerCharacterListData)
    {
        this.playerCharacterListData = playerCharacterListData;
    }
    public void LoadStatisticalData(StatisticalData statisticalData)
    {
        this.statisticalData = statisticalData;
    }

    public void LoadData()
    {
        EventCenter.Instance.EventTrigger(E_Event_Type.OnGameLoad.ToString());
        JsonMgr.Instance.SaveData(musicData, "MusicData");
        JsonMgr.Instance.SaveData(playerBagData, "PlayerBagData");
        JsonMgr.Instance.SaveData(playerCharacterListData, "PlayerCharacterListData");
        JsonMgr.Instance.SaveData(statisticalData, "StatisticalData");
    }

    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    public void SetIsGameOver(bool isGameOver)
    {
        this.isGameOver = isGameOver;
    }

    public void AddRankData(RankInfo rankInfo)
    {
        rankData.list.Add(rankInfo);
        rankData.list.Sort((x, y) =>
        {
            if (x.Layers == y.Layers)
            {
                return x.time < y.time ? -1 : 1;
            }
            return x.Layers > y.Layers ? -1 : 1;
        });
        JsonMgr.Instance.SaveData(rankData, "RankData");
    }
}
