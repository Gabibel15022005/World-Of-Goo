using System.Collections;
using UnityEngine;

public class ShockWaveManager : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] private float shockWaveTime = 0.75f;
    private static int _waveDistanceFromCenter = Shader.PropertyToID("_WaveDistanceFromCenter");
    private static int _shockWaveStrength = Shader.PropertyToID("_ShockWaveStrength");

    public void StartShockWave(float shockWaveTimer, float shockWaveStrength)
    {
        shockWaveTime = shockWaveTimer;
        StartCoroutine(ShockWaveAction(-0.1f, 1f, shockWaveStrength));
    }

    private IEnumerator ShockWaveAction(float startPos, float endPos, float shockWaveStrength)
    {
        material.SetFloat(_waveDistanceFromCenter, startPos);
        material.SetFloat(_shockWaveStrength, shockWaveStrength);

        float lerpedAmount;
        float elapsedTime = 0;

        while (elapsedTime < shockWaveTime)
        {
            elapsedTime += Time.deltaTime;
            lerpedAmount = Mathf.Lerp(startPos, endPos, elapsedTime / shockWaveTime);

            material.SetFloat(_waveDistanceFromCenter, lerpedAmount);
            yield return null;
        }


    }
}
