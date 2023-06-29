using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingMusicPanel : BasePanel
{
    public Toggle togBkMusic;
    public Slider SliderBkMusic;
    public Text txtbkMusic;

    public Toggle togSound;
    public Slider SliderSound;
    public Text txtSound;

    public Button btnBack;

    protected override void Init()
    {
        togBkMusic.onValueChanged.AddListener((isOn) =>
        {
            GameDataMgr.Instance.MusicData.isPlayerMusic = isOn;
            MusicMgr.Instance.ChangeBkValue(isOn ? GameDataMgr.Instance.MusicData.MusicVolume : 0);
        });

        togSound.onValueChanged.AddListener((isOn) =>
        {
            GameDataMgr.Instance.MusicData.isPlayerSound = isOn;
            MusicMgr.Instance.ChangeSoundValue(isOn ? GameDataMgr.Instance.MusicData.SoundVolume : 0);
        });

        SliderBkMusic.onValueChanged.AddListener((value) =>
        {
            txtbkMusic.text = ((int)(value * 100)).ToString();
            GameDataMgr.Instance.MusicData.MusicVolume = value;
            MusicMgr.Instance.ChangeBkValue(GameDataMgr.Instance.MusicData.isPlayerMusic? value : 0);
        });

        SliderSound.onValueChanged.AddListener(value =>
        {
            txtSound.text = ((int)(value * 100)).ToString();
            GameDataMgr.Instance.MusicData.SoundVolume = value;
            MusicMgr.Instance.ChangeSoundValue(GameDataMgr.Instance.MusicData.isPlayerSound ? value : 0);
        });



        btnBack.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<SettingMusicPanel>();
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        MusicData musicData = GameDataMgr.Instance.MusicData;
        togBkMusic.isOn = musicData.isPlayerMusic;
        togSound.isOn = musicData.isPlayerSound;
        SliderBkMusic.value = (float)musicData.MusicVolume;
        SliderSound.value = (float)musicData.SoundVolume;
        txtbkMusic.text = ((int)(musicData.MusicVolume * 100)).ToString();
        txtSound.text = ((int)(musicData.SoundVolume * 100)).ToString();
    }

    public override void HideMe(UnityAction callBack)
    {
        GameDataMgr.Instance.SaveMusicData();
        base.HideMe(callBack);
    }
}
