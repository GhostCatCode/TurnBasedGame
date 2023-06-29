using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfo : MonoBehaviour
{
    public Text txtInfo;

    public void Setup(string text)
    {
        this.txtInfo.text = text;
    }
}
