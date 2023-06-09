using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSVController_VR : MonoBehaviour
{
    // keep track of which eye is being modified
    public int i = 0;

    // keey track of specific adjustments for left and right eye hsv
    public float[] leftEyeAdjustments = { 0, 1, 1 };
    public float[] rightEyeAdjustments = { 0, 1, 1 };

    // step size and bounds for adjusting HSV
    public float stepSize = 0.05f;

    // controllers
    UnityEngine.XR.InputDevice leftHandDevice;
    UnityEngine.XR.InputDevice rightHandDevice;



    // Start is called before the first frame update
    void Start()
    {
        // set left hand controller
        List<UnityEngine.XR.InputDevice> leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        leftHandDevice = leftHandDevices[0];

        // set right hand controller
        List<UnityEngine.XR.InputDevice> rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        rightHandDevice = rightHandDevices[0];
    }

    // Update is called once per frame
    void Update()
    {
        bool temp;

        // press left trigger to switch to left eye
        if (leftHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out temp) && temp) {
            i = 0;
            Debug.Log("left trigger");
        }

        // press right trigger to switch to right eye
        if (rightHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out temp) && temp) {
            i = 1;
            Debug.Log("right trigger");
        }

        // press left A to decrease hue, press right A to increase hue
        if (leftHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out temp) && temp) {
            if (i == 0) {
                leftEyeAdjustments[0] -= stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftHue", leftEyeAdjustments[0]);
            } 
            else {
                rightEyeAdjustments[0] -= stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightHue", rightEyeAdjustments[0]);
            }
            Debug.Log("left A");
        }
        if (rightHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out temp) && temp) {
            if (i == 0) {
                leftEyeAdjustments[0] += stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftHue", leftEyeAdjustments[0]);
            } 
            else {
                rightEyeAdjustments[0] += stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightHue", rightEyeAdjustments[0]);
            }
            Debug.Log("right A");
        }

        // press left B to decrease saturation, press right B to increase saturation
        if (leftHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out temp) && temp) {
            if (i == 0) {
                leftEyeAdjustments[1] -= stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftSaturation", leftEyeAdjustments[1]);
            } 
            else {
                rightEyeAdjustments[1] -= stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightSaturation", rightEyeAdjustments[1]);
            }
            Debug.Log("left B");
        }
        if (rightHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out temp) && temp) {
            if (i == 0) {
                leftEyeAdjustments[1] += stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftSaturation", leftEyeAdjustments[1]);
            } 
            else {
                rightEyeAdjustments[1] += stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightSaturation", rightEyeAdjustments[1]);
            }
            Debug.Log("right B");
        }

        // press left grip to decrease value, press right grip to increase value
        if (leftHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out temp) && temp) {
            if (i == 0) {
                leftEyeAdjustments[2] -= stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftValue", leftEyeAdjustments[2]);
            } 
            else {
                rightEyeAdjustments[2] -= stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightValue", rightEyeAdjustments[2]);
            }
            Debug.Log("left grip");
        }
        if (rightHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out temp) && temp) {
            if (i == 0) {
                leftEyeAdjustments[2] += stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftValue", leftEyeAdjustments[2]);
            } 
            else {
                rightEyeAdjustments[2] += stepSize;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightValue", rightEyeAdjustments[2]);
            }
            Debug.Log("right grip");
        }
    }
}
