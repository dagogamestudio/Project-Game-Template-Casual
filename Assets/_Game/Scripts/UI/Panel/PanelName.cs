using TMPro;
using UnityEngine;

public class PanelName : MonoBehaviour
{
    public TMP_InputField inputName;

    private PanelAnimation panel;

    private void Start()
    {
        panel = GetComponent<PanelAnimation>();
    }

    public void SetPlayerName()
    {
        string name = inputName.text;
        if (string.IsNullOrEmpty(name))
        {
            CoreManager.Instance.ShowNotif("Nama Tidak Boleh Kosong!", 3);
            return;
        }
        else
        {
            PlayerPrefs.SetString(DataString.PlayerName, name);
            CoreManager.Instance.ShowNotif($"Selamat Bermain, {name}", 3);
            panel.ClosePanel();
        }
    }
}
