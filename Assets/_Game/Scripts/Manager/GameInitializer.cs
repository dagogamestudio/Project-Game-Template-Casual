using System.Collections;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(InitManager());
    }
    private IEnumerator InitManager()
    {
        SaveManager.Instance.Initialize();
        yield return null;

        GameManager.Instance.Initialize();
        yield return null;

        ShopManager.Instance.Initialize();

    }
}
