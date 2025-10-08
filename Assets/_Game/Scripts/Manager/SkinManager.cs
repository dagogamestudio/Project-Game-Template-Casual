using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    public Transform transformSkin;
    public Transform transformAcc;

    [Header("Skins")]
    public List<GameObject> listPrefabSkin;
    private GameObject currentSkin;

    [Header("Accessories")]
    public List<GameObject> listPrefabAcc;
    private GameObject currentAcc;


    public void Initialize()
    {
        //SelectedSkin(shop)
    }

    public void SelectedSkin(int skinIndex)
    {
        if (currentSkin != null)
            Destroy(currentSkin);

        currentSkin = Instantiate(listPrefabSkin[skinIndex], transformSkin);
        currentSkin.transform.localPosition = Vector3.zero;
        currentSkin.transform.localRotation = Quaternion.identity;
    }

    public void SelectedAcc(int accIndex)
    {
        if (currentAcc != null)
            Destroy(currentAcc);

        currentAcc = Instantiate(listPrefabAcc[accIndex], transformAcc);
        currentAcc.transform.localPosition = Vector3.zero;
    }
}
