using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform followTgt;
    public Transform lookTgt;

    public float followRate;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followTgt != null && lookTgt != null)
        {
            transform.position = Vector3.Lerp(transform.position, followTgt.position, followRate);
            transform.LookAt(lookTgt);
        }
    }

    public void InitializeCamera(Transform follow, Transform look)
    {
        followTgt = follow;
        lookTgt = look;
    }
}
