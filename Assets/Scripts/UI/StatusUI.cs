using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private Text txtName;
    [SerializeField] private Text txtHp;
    [SerializeField] private Text txtAC;
    [SerializeField] private Text txtSan;
    [SerializeField] private Text txtATK;
    [SerializeField] private Text txtDEF;

    private CharacterStatus status;

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener(E_Event_Type.OnChangedCharacter.ToString(), OnChangedCharacter);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(E_Event_Type.OnChangedCharacter.ToString(), OnChangedCharacter);
    }

    public void Setup(CharacterStatus status)
    {
        this.status = status;
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        txtName.text = status.name;
        txtHp.text = status.hp + " /" + status.maxHp;
        txtAC.text = status.ac + " /" + status.maxAc;
        txtSan.text = status.san + " /" + status.maxSan;
        txtATK.text = status.atk.ToString();
        txtDEF.text = status.def.ToString();
    }

    private void OnChangedCharacter()
    {
        UpdateStatus();
    }
}
