using UnityEngine;

public class GooTentaclesBehaviour : MonoBehaviour
{
    [SerializeField] private Goo goo;
    [SerializeField] private Tentacle[] tentacles;


    [Header("When dragged")]
    public float targetDist;
    public float smoothSpeed;
    public float trailSpeed;
    public float wiggleSpeed;
    public float wiggleMagnitude;

    void Update()
    {
        if (goo == null) return;

        if (goo.IsDragging || goo.isEnding)
        {
            ChangeTentaclesValues(targetDist, smoothSpeed, trailSpeed, wiggleSpeed, wiggleMagnitude);
        }
        else
        {
            ChangeTentaclesValues(tentacles[0].initialTargetDist, tentacles[0].initialSmoothSpeed, tentacles[0].initialTrailSpeed, tentacles[0].initialWiggleSpeed, tentacles[0].initialWiggleMagnitude);
        }

    }

    void ChangeTentaclesValues(float newTargetDist, float newSmoothSpeed, float newTrailSpeed, float newWiggleSpeed, float newWiggleMagnitude)
    {  
        foreach (Tentacle tentacle in tentacles)
        {
            tentacle.targetDist = newTargetDist;
            tentacle.smoothSpeed= newSmoothSpeed;
            tentacle.trailSpeed = newTrailSpeed;
            tentacle.wiggleSpeed = newWiggleSpeed;
            tentacle.wiggleMagnitude = newWiggleMagnitude;
        }

    }
}
