using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManager : MonoBehaviour
{
    bool isSlicing;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (isSlicing)
        {
            ContinueSlicing();
        }

        void StartSlicing()
        {
            isSlicing = true;
        }

        void StopSlicing()
        {
            isSlicing = false;
        }

        void ContinueSlicing()
        {

        }
    }
}
