using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private string soundName;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlaySFX(soundName);
        });  
    }
}
