using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankInfoUI : MonoBehaviour
{
    private RankInfo rankInfo;
    public Text txtRanking;
    public Text txtName;
    public Text txtLayer;
    public Text txtTime;

    public void SetUp(RankInfo rankInfo, int index)
    {
        this.rankInfo = rankInfo;
        txtRanking.text = index.ToString();
        txtName.text = rankInfo.Name;
        txtLayer.text = rankInfo.Layers + "≤„";
        txtTime.text = GetTimeString(rankInfo.time);
    }

    private string GetTimeString(int t)
    {
        string time = "";
        if (t > 3600)
        {
            time = time + (t / 3600) + " ±";
            t = t / 3600;
        }
        if ( t > 60)
        {
            time = time + (t / 60) + "∑÷";
            t = t / 60;
        }
        time = time + t + "√Î";
        return time;
    }
}
