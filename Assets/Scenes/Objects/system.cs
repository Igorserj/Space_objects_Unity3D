using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class system : MonoBehaviour
{
public GameObject solar;
public GameObject spaceObject;
    public Camera MainCamera;
    public Camera Camera2;
    public Text text;
    public Text text2;
    public Text legendText;
    public PostProcessVolume volumeCamera;
    public PostProcessVolume volumeCamera2;
    public GameObject legendButton;
    public Canvas canvas;

    private GameObject[] so;
    private float[] radiusses;
    private float[] masses;
    private int quantity = 1;
    private Color[] soColor;
    public Transform customPivot;
private float rotSpeed = 5f;
private static float sunRadius = 109.3f;
private float deltaDistance = 0;
private string[] legend = new string[7];
    private Color[] legendColor = new Color[7];
private enum types {
Asteroidan,
Mercurian,
Subterran,
Terran,
Superterranrran,
Neptunian,
Jovian 
}

    private static float soRadius = 1;
    private static float soMass = 0;

    void Start()
    {

        Factory();
        legendFactory();
        Camera2.enabled = false;
        MainCamera.transform.position = new Vector3(960, 560, -(sunRadius * 2f));
        Camera2.transform.position = new Vector3(960, 540 + sunRadius * 2f, 0);
    }

void Update()
{
solar.transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
        for (int i = 0; i < quantity; i++)
        {
            float dist = Vector3.Distance(solar.transform.position, so[i].transform.position);
            so[i].transform.RotateAround(customPivot.position, Vector3.up, 10 * solar.transform.localScale.x/dist * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MainCamera.enabled = !MainCamera.enabled;
            Camera2.enabled = !Camera2.enabled;
        }
        else if (Input.GetKeyDown(KeyCode.O)) {
            volumeCamera.enabled = !volumeCamera.enabled;
            volumeCamera2.enabled = !volumeCamera2.enabled;
        }
}

void Factory() {
Solar();
SpaceObject();
}
void Solar() {
solar.transform.position = new Vector3(960, 540, 0);
solar.transform.localScale = new Vector3(sunRadius, sunRadius, sunRadius);
//solar.GetComponent<Renderer>().material.color = Color.yellow;
}

    void SpaceObject() {
        quantity = Random.Range(1, 10);
        so = new GameObject[quantity];
        deltaDistance = 0.6f * sunRadius;
        text.text = "Mass\n";
        text2.text = "Radius\n";
        float previousRadius = deltaDistance;
        massClass();
        for (int i = 0; i < quantity; i++)
        {
            text.text += masses[i].ToString() + "\n";
            text2.text += radiusses[i].ToString() + "\n";
            so[i] = Instantiate(spaceObject, new Vector3(solar.transform.position.x + previousRadius + radiusses[i] * 1.6f, solar.transform.position.y, solar.transform.position.z), Quaternion.identity);
            so[i].GetComponent<Renderer>().material.color = soColor[i];
            Debug.Log(masses[i].ToString());
            so[i].transform.localScale = new Vector3(radiusses[i], radiusses[i], radiusses[i]);
            previousRadius += radiusses[i] * 1.6f;
        }

    }

    void massClass() {
        radiusses = new float[quantity];
        masses = new float[quantity];
        soColor = new Color[quantity];
        for (int i = 0; i < quantity; i++)
        {
            var type = (types)Random.Range(0, 6);
            switch (type)
            {
                case types.Asteroidan:
                    soRadius = Random.Range(0f, 0.03f);
                    soMass = Random.Range(0f, 0.00001f);
                    soColor[i] = Color.gray;
                    radiusses[i] = soRadius;
                    masses[i] = soMass;
                    legendBuilder("Asteroidan", soColor[i]);
                    break;
                case types.Mercurian:
                    soRadius = Random.Range(0.03f, 0.7f);
                    soMass = Random.Range(0.00001f, 0.1f);
                    soColor[i] = Color.red;
                    radiusses[i] = soRadius;
                    masses[i] = soMass;
                    legendBuilder("Mercurian", soColor[i]);
                    break;
                case types.Subterran:
                    soRadius = Random.Range(0.5f, 1.2f);
                    soMass = Random.Range(0.1f, 0.5f);
                    soColor[i] = new Color(0.588f, 0.294f, 0f);
                    radiusses[i] = soRadius;
                    masses[i] = soMass;
                    legendBuilder("Subterran", soColor[i]);
                    break;
                case types.Terran:
                    soRadius = Random.Range(0.8f, 1.9f);
                    soMass = Random.Range(0.5f, 2f);
                    soColor[i] = Color.green;
                    radiusses[i] = soRadius;
                    masses[i] = soMass;
                    legendBuilder("Terran", soColor[i]);
                    break;
                case types.Superterranrran:
                    soRadius = Random.Range(1.3f, 3.3f);
                    soMass = Random.Range(2f, 10f);
                    soColor[i] = Color.cyan;
                    radiusses[i] = soRadius;
                    masses[i] = soMass;
                    legendBuilder("Superterran", soColor[i]);
                    break;
                case types.Neptunian:
                    soRadius = Random.Range(2.1f, 5.7f);
                    soMass = Random.Range(10f, 50f);
                    soColor[i] = Color.blue;
                    radiusses[i] = soRadius;
                    masses[i] = soMass;
                    legendBuilder("Neptunian", soColor[i]);
                    break;
                case types.Jovian:
                    soRadius = Random.Range(3.5f, 27f);
                    soMass = Random.Range(50f, 5000f);
                    soColor[i] = Color.white;
                    radiusses[i] = soRadius;
                    masses[i] = soMass;
                    legendBuilder("Jovian", soColor[i]);
                    break;
            }
        }
        for (int i = 0; i < quantity - 1; i++) sorting();
    }
    void sorting() {
        if (so.Length >= 2)
        {
            for (int i = 0; i < so.Length - 1; i++)
            {
                if (masses[i] < masses[i + 1]) { }
                else
                {
                    float mass = masses[i];
                    float radius = radiusses[i];
                    Color color = soColor[i];
                    masses[i] = masses[i + 1];
                    masses[i + 1] = mass;
                    radiusses[i] = radiusses[i + 1];
                    radiusses[i + 1] = radius;
                    soColor[i] = soColor[i + 1];
                    soColor[i + 1] = color;
                }
            }
        }
    }
    void legendBuilder(string type, Color color) {
        for (int j = 0; j < legend.Length; j++)
        {
            if (legend[j] != type && legend[j] == null)
            {
                legend[j] = type;
                legendColor[j] = color;
                j = legend.Length;
            }
            else if (legend[j] == type) {
                j = legend.Length;
            }
        }
    }
    void legendFactory() {
        int dist = 0;
        //GameObject so;
        for (int j = 0; j < legend.Length; j++)
        {
            if (legend[j] == null) { j = legend.Length; }
            else
            {
                dist += 150;
                GameObject so = Instantiate(legendButton);
                so.transform.SetParent(canvas.transform, false);
                so.transform.position = new Vector3(legendButton.transform.position.x + dist, legendButton.transform.position.y, legendButton.transform.position.z);
                legendText.text = (legend[j]);
                legendText.color = legendColor[j];
                Debug.Log(legend[j]);
            }
        }
    }
}
