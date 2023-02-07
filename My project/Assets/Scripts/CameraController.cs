using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float cameraSpeed=5f;

    private void LateUpdate()
    {

        Vector3 newPlayerPosition = playerTransform.position + offset;
        transform.position = Vector3.Lerp(transform.position, newPlayerPosition, cameraSpeed*Time.deltaTime);
    }

}
