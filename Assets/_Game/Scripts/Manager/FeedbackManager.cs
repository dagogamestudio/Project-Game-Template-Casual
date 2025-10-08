using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    //Menggunakan Firebase
/*    DatabaseReference dbRef;

    public List<DataFeedback> listFeedback;
    public List<string> listName;

    private Regex validNameRegex = new Regex(@"^[a-zA-Z\s]+$");
    private void Awake()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
        GetData();
    }

    public void GetData()
    {
        StartCoroutine(LoadData());
    }


    IEnumerator LoadData()
    {
        var serverData = dbRef.Child("Feedback").GetValueAsync();
        yield return new WaitUntil(() => serverData.IsCompleted);

        if (serverData.Exception != null)
        {
            Debug.LogError("Gagal ambil data: " + serverData.Exception);
            yield break;
        }

        DataSnapshot snapshot = serverData.Result;

        listFeedback.Clear();

        foreach (DataSnapshot child in snapshot.Children)
        {
            string json = child.GetRawJsonValue();
            DataFeedback p = JsonUtility.FromJson<DataFeedback>(json);

            if (!string.IsNullOrEmpty(p.name) && !string.IsNullOrEmpty(p.feedback))
            {
                listFeedback.Add(new DataFeedback(p.name, p.feedback));

                // ✅ Aturan untuk listName
                if (p.name.Length > 2 && validNameRegex.IsMatch(p.name))
                {
                    string processedName = p.name;

                    if (processedName.Length > 20)
                        processedName = processedName.Substring(0, 20);

                    // 🚫 Cek dulu, jangan tambahkan kalau sudah ada
                    if (!listName.Contains(processedName))
                    {
                        listName.Add(processedName);
                    }
                }

                Debug.Log("Nama: " + p.name + " \nSaran: " + p.feedback);
            }
        }

        Debug.Log("Total Feedback: " + listFeedback.Count);
        Debug.Log("Total Nama Valid: " + listName.Count);
    }*/
}

[Serializable]
public class DataFeedback
{
    public string name;
    public string feedback;

    public DataFeedback(string name, string feedback)
    {
        this.name = name;
        this.feedback = feedback;
    }
}