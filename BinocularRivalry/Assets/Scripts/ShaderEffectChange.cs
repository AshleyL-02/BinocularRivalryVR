using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderEffectChange : MonoBehaviour
{
    public Camera cam;
    public List<Shader> availableShaders;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetShader();
    }

    void SetShader()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cam.SetReplacementShader(availableShaders[0], "RenderType");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cam.SetReplacementShader(availableShaders[1], "RenderType");
        }
    }
}
