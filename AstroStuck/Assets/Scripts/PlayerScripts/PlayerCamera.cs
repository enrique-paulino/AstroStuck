using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    [SerializeField] private Transform _camera;
    private Vector3 offset;

    private void Start() {
        offset = _camera.position - transform.position;
    }

    void LateUpdate() {
        Vector3 targetCamPos = transform.position + offset;
        _camera.position = Vector3.Lerp(_camera.position, targetCamPos, 5f * Time.deltaTime);
    }
}
