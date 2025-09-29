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


    [Header("Before change")]
    private float oldTargetDist;
    private float oldSmoothSpeed;
    private float oldTrailSpeed;
    private float oldWiggleSpeed;
    private float oldWiggleMagnitude;

    void Start()
    {
        GetOldTentacleValues();
    }

    void Update()
    {
        if (goo == null) return;

        if (goo.IsDragging)
        {
            ChangeTentaclesValues(targetDist, smoothSpeed, trailSpeed, wiggleSpeed, wiggleMagnitude);
        }
        else
        {
            ChangeTentaclesValues(oldTargetDist, oldSmoothSpeed, oldTrailSpeed, oldWiggleSpeed, oldWiggleMagnitude);
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

    void GetOldTentacleValues()
    {
        oldTargetDist = tentacles[0].targetDist;
        oldSmoothSpeed = tentacles[0].smoothSpeed;
        oldTrailSpeed = tentacles[0].trailSpeed;
        oldWiggleSpeed = tentacles[0].wiggleSpeed;
        oldWiggleMagnitude = tentacles[0].wiggleMagnitude;
    }
}
