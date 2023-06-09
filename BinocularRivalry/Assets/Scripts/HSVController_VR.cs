using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSVController_VR : MonoBehaviour
{
    // object to update
    public GameObject specialObject;

    // keep track of specific adjustments for left and right eye hsv
    public float[] leftEyeAdjustments = { 0, 1, 1 };
    public float[] rightEyeAdjustments = { 0, 1, 1 };

    // step size and bounds for adjusting HSV
    public float stepSize = 0.025f;
    public float maxHue = 1;
    public float maxSaturation = 2;
    public float maxValue = 2;

    public Button leftHueButton;
    public Button rightHueButton;

    public Button leftSaturationButton;
    public Button rightSaturationButton;

    public Button leftValueButton;
    public Button rightValueButton;

    public Button resetButton;


    // Start is called before the first frame update
    void Start()
    {
        leftHueButton.onClick.AddListener(updateLeftHue);
        rightHueButton.onClick.AddListener(updateRightHue);

        leftSaturationButton.onClick.AddListener(updateLeftSaturation);
        rightSaturationButton.onClick.AddListener(updateRightSaturation);

        leftValueButton.onClick.AddListener(updateLeftValue);
        rightValueButton.onClick.AddListener(updateRightValue);

        resetButton.onClick.AddListener(resetHSV);
        resetHSV();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void resetHSV() {
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftHue", 0);
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightHue", 0);

        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftSaturation", 0);
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightSaturation", 0);

        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftValue", 1);
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightValue", 1);
    }

    public void updateLeftHue() {
        leftEyeAdjustments[0] = (leftEyeAdjustments[0] + stepSize) % maxHue;
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftHue", leftEyeAdjustments[0]);
    }

    public void updateRightHue() {
        rightEyeAdjustments[0] = (rightEyeAdjustments[0] + stepSize) % maxHue;
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightHue", rightEyeAdjustments[0]);
    }

    public void updateLeftSaturation() {
        leftEyeAdjustments[1] = (leftEyeAdjustments[1] + stepSize) % maxSaturation;
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftSaturation", leftEyeAdjustments[1]);
    }

    public void updateRightSaturation() {
        rightEyeAdjustments[1] = (rightEyeAdjustments[1] + stepSize) % maxSaturation;
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightSaturation", rightEyeAdjustments[1]);
    }

    public void updateLeftValue() {
        leftEyeAdjustments[2] = (leftEyeAdjustments[2] + stepSize) % maxValue;
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftValue", leftEyeAdjustments[2]);
    }

    public void updateRightValue() {
        rightEyeAdjustments[2] = (rightEyeAdjustments[2] + stepSize) % maxValue;
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightValue", rightEyeAdjustments[2]);
    }

}
