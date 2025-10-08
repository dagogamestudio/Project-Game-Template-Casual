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
        GameManager.Instance.Initialize();
        yield return null;

        SaveManager.Instance.Initialize();
        yield return null;

        ShopManager.Instance.Initialize();

    }
}
