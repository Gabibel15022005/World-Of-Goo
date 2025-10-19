using System.ComponentModel;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public int lenght = 3;
    public LineRenderer lineRend;


    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;
    public float trailSpeed;


    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;
    public bool randomWiggle = false;
    public float wiggleOffset = 0;
    private float wiggleSpeedMult;
    private float wiggleMagnitudeMult;


    [HideInInspector] public float initialTargetDist;
    [HideInInspector] public float initialSmoothSpeed;
    [HideInInspector] public float initialTrailSpeed;
    [HideInInspector] public float initialWiggleMagnitude;
    [HideInInspector] public float initialWiggleSpeed;


    private Vector3[] segmentsPos;
    private Vector3[] segmentsV;

    void Start()
    {
        initialTargetDist = targetDist;
        initialSmoothSpeed = smoothSpeed;
        initialTrailSpeed = trailSpeed;
        initialWiggleSpeed = wiggleSpeed;
        initialWiggleMagnitude = wiggleMagnitude;


        lineRend.positionCount = lenght;
        segmentsPos = new Vector3[lenght];
        segmentsV = new Vector3[lenght];

        if (randomWiggle)
        {
            wiggleSpeedMult = UnityEngine.Random.Range(0.8f, 1.2f);
            wiggleMagnitudeMult = UnityEngine.Random.Range(0.8f, 1.2f);
            wiggleOffset = UnityEngine.Random.Range(0f, 2 * Mathf.PI);
        }    

        ResetPosition();
    }

    void Update()
    {
        float currentAngle;

        if (randomWiggle)
            currentAngle = Mathf.Sin(Time.time * wiggleSpeed * wiggleSpeedMult + wiggleOffset) * wiggleMagnitude * wiggleMagnitudeMult;
        else
            currentAngle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude;

        wiggleDir.localRotation = Quaternion.Euler(0, 0, currentAngle);

        segmentsPos[0] = targetDir.position;

        for (int i = 1; i < segmentsPos.Length; i++)
        {
            segmentsPos[i] = Vector3.SmoothDamp
            (
            segmentsPos[i], segmentsPos[i - 1] + targetDir.right * targetDist,
            ref segmentsV[i],
            smoothSpeed + 1 / trailSpeed
            );
        }

        lineRend.SetPositions(segmentsPos);
    }

    void ResetPosition()
    {
        segmentsPos[0] = targetDir.position;

        for (int i = 1; i < lenght; i++)
        {
            segmentsPos[i] = segmentsPos[i - 1] + targetDir.right * targetDist;
        }

        lineRend.SetPositions(segmentsPos);
    }


}
