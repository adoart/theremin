using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour {
    private AudioController _audioController;

    private Transform _transform;

    // Start is called before the first frame update
    void Start() {
        _audioController = GetComponentInParent<AudioController>();
        _transform = transform;
    }
    private void OnTriggerStay(Collider other) {
        Vector3 position = _transform.position;
        Debug.DrawLine(position, other.ClosestPoint(position));
        float distance = Vector3.SqrMagnitude(position - other.ClosestPoint(position));
        _audioController.SetVolume(distance);
    }
}
