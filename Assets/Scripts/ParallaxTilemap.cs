using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParallaxTilemap : MonoBehaviour
{

    [SerializeField] private Vector2 parallaxMultiplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPos = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPos;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier.x, deltaMovement.y * parallaxMultiplier.y);
        lastCameraPos = cameraTransform.position;
    }
}

