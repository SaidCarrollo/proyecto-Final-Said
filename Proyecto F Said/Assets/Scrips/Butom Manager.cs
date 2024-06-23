using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class ButomManager : MonoBehaviour
{
    [SerializeField] private GameObject Bad1;
    [SerializeField] private GameObject God1;
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

    public void Cerrar()
    {
        Bad1.SetActive(false);
    }

    public void Cerrar2()
    {
        God1.SetActive(false);
    }

    public void Gameplay()
    {
        StartCoroutine(PlayGameplayWithEffect());
    }

    private IEnumerator PlayGameplayWithEffect()
    {
        MovementCamera(1.0f, 2.0f, 3.0f);

        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene("Gameplay");
    }

    public void AlMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void MovementCamera(float intensity, float frequency, float time)
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
