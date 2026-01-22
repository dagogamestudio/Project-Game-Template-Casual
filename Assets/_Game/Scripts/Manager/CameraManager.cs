using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    private void Awake()
    {
         instance = this;
    }
    public CinemachineVirtualCamera menuCam;
    public CinemachineVirtualCamera GameCam;

    public void ChangeCamera(bool isGameplay)
    {
        menuCam.Priority = isGameplay ? 0 : 10;
        GameCam.Priority = isGameplay ? 10 : 0;
    }
}
