using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerLoc1 : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    [SerializeField] float Speed = 2.0f;
    [SerializeField] Vector3 offset;

    void LateUpdate()
    {
            Vector3 desiredPos = target.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, Speed);
            transform.position = new Vector3(transform.position.x, desiredPos.y, smoothedPos.z);
    }

}
