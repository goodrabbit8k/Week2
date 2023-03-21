using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManager : MonoBehaviour
{
    public Vector3 knifeDirection { get; private set; }
    public float minimumKnifeVelocity = 0.01f;
    public float knifeForce = 5.0f;

    Collider knifeCollider;
    Camera mainCamera;
    TrailRenderer knifeTrail;
    bool isSlicing;

    void Awake()
    {
        knifeCollider = GetComponent<Collider>();
        knifeTrail = GetComponentInChildren<TrailRenderer>();
        mainCamera = Camera.main;
    }

    void OnEnable()
    {
        StopSlicing();    
    }

    void OnDisable()
    {
        StopSlicing();    
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
            knifeCollider.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
            knifeCollider.enabled = false;
        }
        else if (isSlicing)
        {
            ContinueSlicing();
        }
    }

    void StartSlicing()
    {
        transform.position = SetNewPosition();

        isSlicing = true;
        knifeCollider.enabled = true;
        knifeTrail.enabled = true;
        knifeTrail.Clear();
    }

    void StopSlicing()
    {
        isSlicing = false;
        knifeCollider.enabled = false;
        knifeTrail.enabled = false;
    }

    void ContinueSlicing()
    {
        Vector3 mousePosition = SetNewPosition();

        float velocity = knifeDirection.magnitude / Time.deltaTime;

        knifeDirection = mousePosition - transform.position;

        knifeCollider.enabled = velocity > minimumKnifeVelocity;

        transform.position = mousePosition;
    }

    Vector3 SetNewPosition()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        return newPosition;
    }
}
