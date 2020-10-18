using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxSwitcher : MonoBehaviour
{
    public Material skybox;
    public void SwitchSkybox()
    {
        RenderSettings.skybox = skybox;
    }
}
