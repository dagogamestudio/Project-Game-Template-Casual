using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Linq;

public class CheckVersion : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private PanelAnimation panelUpdate;
    [SerializeField] private TMP_Text textDescriptionUpdate;
    [SerializeField] private AutoSizePrefferedText textDetailUpdate;

    [Header("Data")]
    [SerializeField] private string updateDescription = "";

    [Header("Link External")]
    [SerializeField] private string linkgame = "";
    [SerializeField] private string jsonURL = "";

    void Start()
    {
        if (string.IsNullOrEmpty(jsonURL)) return;

        StartCoroutine(GetData(jsonURL));
    }

    IEnumerator GetData(string url)
    {
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            string[] lines = req.downloadHandler.text.Trim().Split('\n'); // Pisahkan per baris

            if (lines.Length == 0) yield break;

            string newVersion = lines[0].Trim();
            string currentVersion = Application.version.Trim();

            try
            {
                Version localVer = new Version(currentVersion);
                Version serverVer = new Version(newVersion);

                if (localVer < serverVer)
                {
                    panelUpdate.OpenPanel();
                    textDescriptionUpdate.text = $"{updateDescription} <color=#00CC00>v{newVersion}</color>?";

                    string changelog = string.Join("\n", lines.Skip(1).ToArray());
                    textDetailUpdate.ChangeText(changelog);

                    if (textDetailUpdate.PrefferedText.preferredHeight >= 375)
                    {
                        textDetailUpdate.GetComponent<RectTransform>().pivot = new(0.5f, 1);
                    }
                    else
                    {
                        textDetailUpdate.GetComponent<RectTransform>().pivot = new(0.5f, 0.5f);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Gagal memproses versi: " + e.Message);
            }
        }
    }

    public void UpdateGame()
    {
        Application.OpenURL(linkgame);
    }
}