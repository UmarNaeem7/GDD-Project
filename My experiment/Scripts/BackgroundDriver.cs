using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackgroundDriver : MonoBehaviour
{
    public Image definition;
    public Image difference;
    public Image magnet;
    public Image magnetism;
    public Text text;
    public Text text1;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        difference.enabled = false;
        magnet.enabled = false;
        magnetism.enabled = false;
        text.enabled = false;
        text1.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OkButtonClick()
    {
        switch (count)
        {
            case 0:
                definition.enabled = false;
                difference.enabled = true;
                break;
            case 1:
                difference.enabled = false;
                magnet.enabled = true;
                break;
            case 2:
                magnet.enabled = false;
                magnetism.enabled = true;
                break;
            case 3:
                magnetism.enabled = false;
                text.enabled = true;
                text1.enabled = true;
                break;
            case 4:
                SceneManager.LoadScene(3);
                break;
            default:
                break;
        }
        count++;
    }
}
