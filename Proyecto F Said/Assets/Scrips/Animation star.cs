using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class ButomManager : MonoBehaviour
{
    private CinemachineVirtualCamera CineMachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin CinemachinePerlin;
    private float Timemovement;
    private float TimeTotal;
    private float intensityFirst;

    private void Awake()
    {
        CineMachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        CinemachinePerlin = CineMachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void StartCameraShake(float intensity, float frequency, float time)
    {
        MovementCamera(intensity, frequency, time);
    }
    private void MovementCamera(float intensity, float frequency, float time)
    {
        CinemachinePerlin.m_AmplitudeGain = intensity;
        CinemachinePerlin.m_FrequencyGain = frequency;
        intensityFirst = intensity;
        TimeTotal = time;
        Timemovement = time;
    }

    private void Update()
    {
        if (Timemovement > 0)
        {
            Timemovement -= Time.deltaTime;
            CinemachinePerlin.m_AmplitudeGain = Mathf.Lerp(intensityFirst, 0, 1 - (Timemovement / TimeTotal));
        }
    }
}
