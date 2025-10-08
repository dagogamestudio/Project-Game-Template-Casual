using UnityEngine;
using UnityEngine.UI;

public class ButtonScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private void Start() => GetComponent<Button>().onClick.AddListener(ChangeScene);
    public void ChangeScene() => ASyncSceneLoader.Instance.ChangeScene(sceneName);
}
