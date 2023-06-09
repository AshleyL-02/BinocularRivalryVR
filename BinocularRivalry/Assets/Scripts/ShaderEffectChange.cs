using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderEffectChange : MonoBehaviour
{
    public GameObject specialObject;

    public Button basicMatButton;
    public Material BasicMat;

    public Button fullGrayscaleMatButton;
    public Material FullGrayscaleMat;

    public Button grayscaleColorMatButton;
    public Material GrayscaleColorMat;

    public Button HSVMatButton;
    public Material HSVMat;
    public GameObject HSVUI;

    public Button iridescenceMatButton;
    public Material IridescenceMat;

    // Start is called before the first frame update
    void Start()
    {
        basicMatButton.onClick.AddListener(setBasic);
        fullGrayscaleMatButton.onClick.AddListener(setFullGrayscale);
        grayscaleColorMatButton.onClick.AddListener(setGrayscaleColor);
        HSVMatButton.onClick.AddListener(setHSV);
        iridescenceMatButton.onClick.AddListener(setIridescence);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setBasic() {
        specialObject.GetComponent<MeshRenderer>().material = BasicMat;
        HSVUI.SetActive(false);
    }

    public void setFullGrayscale() {
        specialObject.GetComponent<MeshRenderer>().material = FullGrayscaleMat;
        HSVUI.SetActive(false);
    }

    public void setGrayscaleColor() {
        specialObject.GetComponent<MeshRenderer>().material = GrayscaleColorMat;
        HSVUI.SetActive(false);
    }

    public void setHSV() {
        specialObject.GetComponent<MeshRenderer>().material = HSVMat;
        resetHSV();
        HSVUI.SetActive(true);
    }

    private void resetHSV() {
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftHue", 0);
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightHue", 0);

        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftSaturation", 0);
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightSaturation", 0);

        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_LeftValue", 1);
        specialObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_RightValue", 1);
    }

    public void setIridescence() {
        specialObject.GetComponent<MeshRenderer>().material = IridescenceMat;
        HSVUI.SetActive(false);
    }
}
