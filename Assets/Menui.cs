using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menui : MonoBehaviour
{
    private FPScontroller fpsController;
    private Canvas menui;
    void Start()
    {
        menui = GetComponent<Canvas>();
        fpsController = FindObjectOfType<FPScontroller>();
    }
    public void Back()
    {
        menui.enabled = !menui.enabled;
    }

    public void Sensitivity(float sens)
    {
        fpsController.sensitivity = sens;
    }

    public void SensitivityAds(float sens)
    {
        fpsController.sensitivityADS = sens;
    }
}
