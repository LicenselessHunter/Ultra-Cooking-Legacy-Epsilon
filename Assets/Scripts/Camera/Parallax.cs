using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplier;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private Vector3 deltaMovement;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void FixedUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        float parallaxEffectMultiplier = .5f;
        transform.position += deltaMovement * parallaxEffectMultiplier;
        lastCameraPosition = cameraTransform.position;
    }
}