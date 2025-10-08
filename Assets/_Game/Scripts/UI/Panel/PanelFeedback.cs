using TMPro;
using UnityEngine;

public class PanelFeedback : MonoBehaviour
{
    public DataFeedback dataFeedback;

    public TMP_InputField inputName;
    public TMP_InputField inputFeedback;

    //DatabaseReference dbRef;

    private void Awake()
    {
        //dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SubmitFeedback()
    {
        dataFeedback.name = inputName.text;
        dataFeedback.feedback = inputFeedback.text;

        if (string.IsNullOrEmpty(dataFeedback.name) || string.IsNullOrEmpty(dataFeedback.feedback))
        {
            CoreManager.Instance.panelConfirmation.SetInformation("Data Kosong!", "Masukan nama dan saran, jangan kosong ya.");
            return;
        }

        string json = JsonUtility.ToJson(dataFeedback);
        //dbRef.Child("Feedback").Push().SetRawJsonValueAsync(json);

        inputName.text = "";
        inputFeedback.text = "";

        if (Application.internetReachability == NetworkReachability.NotReachable)
            CoreManager.Instance.panelConfirmation.SetInformation("Terjadi Kesalahan", "Harap Aktifkan Data Internet");
        else
            CoreManager.Instance.panelConfirmation.SetInformation("Saran Terkirim", "Terima kasih yaa buat sarannya, Nanti akan kami update lagi gamenya");
    }
}
