using TMPro;
using UnityEngine;

public class PanelWaveCompleted : MonoBehaviour
{
    public TextMeshProUGUI textWaveCompleted;
    public TextMeshProUGUI textNextWave;

    public void SetText(int nextWave)
    {
        gameObject.SetActive(true);
        textWaveCompleted.text = $"Wave {nextWave - 1}\nCompleted";
        textNextWave.text = $"Wave {nextWave}";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
