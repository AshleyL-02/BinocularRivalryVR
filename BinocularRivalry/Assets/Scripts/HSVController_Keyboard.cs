using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSVController : MonoBehaviour
{
    // keep track of which eye is being modified
    public int i = 0;

    // keey track of specific adjustments for left and right eye hsv
    public float[] leftEyeAdjustments = { 0, 1, 1 };
    public float[] rightEyeAdjustments = { 0, 1, 1 };

    // step size for adjusting HSV
    public float stepSize = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // press space to switch eyes
        if (Input.GetKeyDown(KeyCode.Space)) {
            i = (i + 1) % 2;
        }

        // press 1 to decrease hue, press 2 to increase hue
        if (Input.GetKey(KeyCode.Alpha1)) {
            if (i == 0) {
                leftEyeAdjustments[0] -= stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftHue", leftEyeAdjustments[0]);
            } 
            else {
                rightEyeAdjustments[0] -= stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightHue", rightEyeAdjustments[0]);
            }
        }
        if (Input.GetKey(KeyCode.Alpha2)) {
            if (i == 0) {
                leftEyeAdjustments[0] += stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftHue", leftEyeAdjustments[0]);
            } 
            else {
                rightEyeAdjustments[0] += stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightHue", rightEyeAdjustments[0]);
            }
        }

        // press 3 to decrease saturation, press 4 to increase saturation
        if (Input.GetKey(KeyCode.Alpha3)) {
            if (i == 0) {
                leftEyeAdjustments[1] -= stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftSaturation", leftEyeAdjustments[1]);
            } 
            else {
                rightEyeAdjustments[1] -= stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightSaturation", rightEyeAdjustments[1]);
            }
        }
        if (Input.GetKey(KeyCode.Alpha4)) {
            if (i == 0) {
                leftEyeAdjustments[1] += stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftSaturation", leftEyeAdjustments[1]);
            } 
            else {
                rightEyeAdjustments[1] += stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightSaturation", rightEyeAdjustments[1]);
            }
        }

        // press 5 to decrease value, press 6 to increase value
        if (Input.GetKey(KeyCode.Alpha5)) {
            if (i == 0) {
                leftEyeAdjustments[2] -= stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftValue", leftEyeAdjustments[2]);
            } 
            else {
                rightEyeAdjustments[2] -= stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightValue", rightEyeAdjustments[2]);
            }
        }
        if (Input.GetKey(KeyCode.Alpha6)) {
            if (i == 0) {
                leftEyeAdjustments[2] += stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftValue", leftEyeAdjustments[2]);
            } 
            else {
                rightEyeAdjustments[2] += stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightValue", rightEyeAdjustments[2]);
            }
        }
    }
}
