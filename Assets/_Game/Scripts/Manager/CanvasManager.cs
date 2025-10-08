using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelGameplay;
    public GameObject panelFinish;

    public TextMeshProUGUI textMoney;


    public void ShowPanel(string panelName)
    {
/*        foreach (var panel in listPanels)
        {
            if (panel.panelName == panelName)
            {
                panel.panelObject.OpenPanel();
                break;
            }
        }*/
    }

    public void HidePanel(string panelName)
    {
/*        foreach (var panel in listPanels)
        {
            if (panel.panelName == panelName)
            {
                panel.panelObject.ClosePanel();
                break;
            }
        }*/
    }

    public void SetTextMoney(int price)
    {
        textMoney.text = Helper.TurnToIDRValue(price);
    }
}

[Serializable]
public class PanelGame
{
    public string panelName;
    public PanelAnimation panelObject;
}