using NUnit.Framework;
using UnityEngine;

public class EndOfLevelTentacles : MonoBehaviour
{
    [SerializeField] EndOfLevel endOfLevel;
    [SerializeField] Transform tentaclesCenter;
    [SerializeField] float slowRotationSpeed = 125;
    [SerializeField] float fastRotationSpeed = 500;
    [SerializeField] float speedChangeRate = 1f; // vitesse d’accélération globale (0 = instant, plus grand = plus lent)

    [Space(30)]
    [Header("When End Of Level")]
    [SerializeField] private Tentacle[] tentaclesB;
    [SerializeField] private Tentacle[] tentaclesW;

    [Space(30)]
    [Header("When End Of Level Black")]
    public float targetDistB;
    public float smoothSpeedB;
    public float trailSpeedB;
    public float wiggleSpeedB;
    public float wiggleMagnitudeB;

    [Space(30)]
    [Header("When End Of Level White")]
    public float targetDistW;
    public float smoothSpeedW;
    public float trailSpeedW;
    public float wiggleSpeedW;
    public float wiggleMagnitudeW;

    // Progression entre état "lent" et état "rapide" (0 → slow, 1 → fast)
    private float transitionT = 0f;
    void Update()
    {
        // avancer ou reculer la transition
        if (endOfLevel.hasFoundGoo)
            transitionT = Mathf.MoveTowards(transitionT, 1f, Time.deltaTime * speedChangeRate);
        else
            transitionT = Mathf.MoveTowards(transitionT, 0f, Time.deltaTime * speedChangeRate);

        // rotation interpolée
        float currentRotationSpeed = Mathf.Lerp(slowRotationSpeed, fastRotationSpeed, transitionT);
        tentaclesCenter.Rotate(0f, 0f, currentRotationSpeed * Time.deltaTime);

        // interpolation des valeurs pour chaque groupe
        InterpolateTentaclesValues(tentaclesB, transitionT, targetDistB, smoothSpeedB, trailSpeedB, wiggleSpeedB, wiggleMagnitudeB);
        InterpolateTentaclesValues(tentaclesW, transitionT, targetDistW, smoothSpeedW, trailSpeedW, wiggleSpeedW, wiggleMagnitudeW);
    }

    void InterpolateTentaclesValues(
        Tentacle[] list, float t,
        float targetDist, float smoothSpeed, float trailSpeed, float wiggleSpeed, float wiggleMagnitude)
    {
        foreach (Tentacle tentacle in list)
        {
            tentacle.targetDist = Mathf.Lerp(tentacle.initialTargetDist, targetDist, t);
            tentacle.smoothSpeed = Mathf.Lerp(tentacle.initialSmoothSpeed, smoothSpeed, t);
            tentacle.trailSpeed = Mathf.Lerp(tentacle.initialTrailSpeed, trailSpeed, t);
            tentacle.wiggleSpeed = Mathf.Lerp(tentacle.initialWiggleSpeed, wiggleSpeed, t);
            tentacle.wiggleMagnitude = Mathf.Lerp(tentacle.initialWiggleMagnitude, wiggleMagnitude, t);
        }
    }
    


}