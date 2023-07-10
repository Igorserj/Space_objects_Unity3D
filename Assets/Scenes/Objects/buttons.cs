using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
   public Camera MainCamera;
   public Camera Camera2;
   public GameObject SolarSphere;
    private bool zoomed = false;
    public PostProcessVolume volumeCamera;
    public PostProcessVolume volumeCamera2;

    public void changeView()
    {
       MainCamera.enabled = !MainCamera.enabled;
       Camera2.enabled = !Camera2.enabled;
       Debug.Log("You have clicked the button!");
   }
    public void sunSwitch() {
        SolarSphere.GetComponent<Renderer>().enabled = !SolarSphere.GetComponent<Renderer>().enabled;
    }
    public void zoomOut() {
        MainCamera.transform.position = new Vector3(960, 560, -(SolarSphere.transform.localScale.x * 2f));
    }
    public void zoomIn()
    {
        if (zoomed == false)
        {
            MainCamera.transform.position = new Vector3(960, 560, -(SolarSphere.transform.localScale.x * 1.4f));
            Camera2.transform.position = new Vector3(960, 540 + SolarSphere.transform.localScale.y * 1.4f, 0);
            zoomed = true;
        }
        else
        {
            MainCamera.transform.position = new Vector3(960, 560, -(SolarSphere.transform.localScale.x * 2f));
            Camera2.transform.position = new Vector3(960, 540 + SolarSphere.transform.localScale.y * 2f, 0);
            zoomed = false;
        }
    }
    public void postProcess()
    {
        volumeCamera.enabled = !volumeCamera.enabled;
        volumeCamera2.enabled = !volumeCamera2.enabled;
    }
    public void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            GameObject.Find("Pause").GetComponentInChildren<Text>().text = "▶";
        }
        else
        {
            Time.timeScale = 1;
            GameObject.Find("Pause").GetComponentInChildren<Text>().text = "▮▮";
        }
    }
}
