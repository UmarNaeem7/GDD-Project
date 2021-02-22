using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{
    public Material blueMaterial;
    public Material filterPaperMaterial;
    private GameObject magnet;
    private GameObject[] ironFillings;
    private GameObject[] separatedFillings;
    private int attached = 0;
    private bool stopAttraction = false;
    private bool magExists = false;
    private bool end = false;

    private static int count = 0;
    private int variation = 1;

    public Button applyButton;
    public Button removeButton;

    private struct Filling
    {
        public GameObject g;
        public int id;
        public int iteration;

        public Filling(GameObject G, int ID, int iter)
        {
            g = G;
            id = ID;
            iteration = iter;
        }
    }

    private List<Filling> fillingList = new List<Filling>();

    // Start is called before the first frame update
    void Start()
    {
        applyButton.interactable = false;
        removeButton.interactable = false;

        ironFillings = GameObject.FindGameObjectsWithTag("filling");
        separatedFillings = GameObject.FindGameObjectsWithTag("separated");
        foreach (var g in separatedFillings)
        {
            g.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!magExists)
        {
            magnet = GameObject.FindGameObjectWithTag("Magnet");
            if (magnet != null)
            {
                Debug.Log("magnet found");
                magExists = true;
                applyButton.interactable = true;
            }
        }

        if (variation == 6)
        {
            SceneManager.LoadScene(4);
        }

        if (!stopAttraction && magExists && !end)
        {
            ironFillings = GameObject.FindGameObjectsWithTag("filling");
            foreach (var g in ironFillings)
            {
                if (attached < 5 && isInMagneticField(g))
                {
                    attached++;
                    var f = new Filling(g, count%5, variation);
                    fillingList.Add(f);
                    count++;
                    attachToMagnet(f);
                    if (attached == 5)
                    {
                        stopAttraction = true;
                        variation++;
                    }
                }
            }
        }

        if (stopAttraction && magExists && !end)
        {
            foreach (var f in fillingList)
            {
                if (f.iteration == variation-1)
                {
                    attachToMagnet(f);
                } 
            }
        }
    }

    private bool isInMagneticField(GameObject g)
    {
        var v = g.transform.position - magnet.transform.position;
        v.z = 0;
        float distance = v.magnitude;
        //float distance = Vector3.Distance(g.transform.position, magnet.transform.position);
        if (distance <= 0.04)
        {
            Debug.Log("Distance  = " + distance);
            return true;
        }
        else
            return false;
    }

    private void attachToMagnet(Filling f)
    {
        float a = 0.007f;
        float b = 0f;

        switch (f.id)
        {
            case 0:
                b = -0.004f;
                break;
            case 1:
                b = -0.002f;
                break;
            case 2:
                b = 0f;
                break;
            case 3:
                b = 0.002f;
                break;
            case 4:
                b = 0.004f;
                break;
            default:
                break;
        }

        
        f.g.transform.position = new Vector3(magnet.transform.position.x + b, magnet.transform.position.y - 0.05f,
            magnet.transform.position.z);
    }

    public void ApplyButtonClick()
    {
        magnet.GetComponent<Renderer>().material = filterPaperMaterial;
        applyButton.interactable = false;
        if (!removeButton.interactable)
            removeButton.interactable = true;
    }

    public void RemoveButtonClick()
    {
        magnet.GetComponent<Renderer>().material = blueMaterial;

        removeButton.interactable = false;
        if (!applyButton.interactable)
            applyButton.interactable = true;

        if (stopAttraction)
        {
            stopAttraction = false;

            for (int i=(variation-2)*5;i< (variation - 1) * 5;i++)
            {
                separatedFillings[i].gameObject.SetActive(true);
            }

            foreach (var f in fillingList)
            {
                Destroy(f.g);
            }

            fillingList.Clear();

            attached = 0;
        }
    }
}
