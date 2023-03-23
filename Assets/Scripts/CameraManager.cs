using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public float speedRotationDollyCamera = 0.5f;
    public CinemachineVirtualCamera cameraWithDolly;

    private bool playerWin;
    private CinemachineTrackedDolly cinemachineTrackedDolly;

    private void Awake()
    {
        cinemachineTrackedDolly = cameraWithDolly.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    private void Start()
    {
        playerWin = false;
    }

    public void PlayerWin()
    {
        cameraWithDolly.Priority = 100; // Se elige un valor grande para que la cámara con Dolly Track tome la preferencia
        playerWin = true;
    }

    private void Update()
    {
        if (playerWin)
            cinemachineTrackedDolly.m_PathPosition += speedRotationDollyCamera * Time.deltaTime; // Con esto la cámara rota por el Dolly Track
    }
}
