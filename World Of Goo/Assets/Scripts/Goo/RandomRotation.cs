using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public List<Transform> transforms;
    void Start()
    {
        float randomZ = UnityEngine.Random.Range(0f, 360f);

        foreach (Transform obj in transforms)
        {
            Vector3 startRotation = obj.eulerAngles;
            obj.rotation = Quaternion.Euler(startRotation.x, startRotation.y, startRotation.z + randomZ);
        }
    }
}
