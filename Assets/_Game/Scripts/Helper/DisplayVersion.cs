using TMPro;
using UnityEngine;

public class DisplayVersion : MonoBehaviour
{
    [SerializeField] private TMP_Text versionText;
    private void Start() => versionText.text = $"v{Application.version}";
}