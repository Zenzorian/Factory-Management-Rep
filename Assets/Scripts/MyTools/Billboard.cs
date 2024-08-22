using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _cameraTransform;

    void Start()
    {
        // »щем основную камеру в сцене
        _cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Ќаправл€ем текст к камере
        // "Vector3.up" используетс€, чтобы обеспечить правильную ориентацию относительно оси Y
        transform.LookAt(transform.position + _cameraTransform.rotation * Vector3.forward, _cameraTransform.rotation * Vector3.up);
    }
}