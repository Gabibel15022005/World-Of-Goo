using Unity.Mathematics;
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


    private Vector3[] segmentsPos;
    private Vector3[] segmentsV;

    void Start()
    {
        lineRend.positionCount = lenght;
        segmentsPos = new Vector3[lenght];
        segmentsV = new Vector3[lenght];
    }

    void Update()
    {
        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        segmentsPos[0] = targetDir.position;

        for (int i = 1; i < segmentsPos.Length; i++)
        {
            segmentsPos[i] = Vector3.SmoothDamp
            (
            segmentsPos[i], segmentsPos[i - 1] + targetDir.right * targetDist,
            ref segmentsV[i],
            smoothSpeed +1 / trailSpeed
            );
        }

        lineRend.SetPositions(segmentsPos);
    }


}
