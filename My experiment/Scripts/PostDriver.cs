using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostDriver : MonoBehaviour
{
    public Text text;
    public Image evaporation;
    public Image sublimation;
    public Text text1;
    public Button okButton;
    public Text statement1;
    public Text part1;
    public Text part2;
    public Text part3;
    public Button magnetismButton;
    public Button evaporationButton;
    public Button sublimationButton;
    public Text statement2;
    public Text statement3;
    public Text statement4;
    public Text lesson;
    public Text scoreTitle;
    public Text scoreStatement;

    private int count = 0;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        evaporation.enabled = false;
        sublimation.enabled = false;
        text1.enabled = false;
        statement1.enabled = false;
        part1.enabled = false;
        part2.enabled = false;
        part3.enabled = false;

        statement2.enabled = false;
        lesson.enabled = false;
        statement3.enabled = false;
        statement4.enabled = false;

        scoreTitle.enabled = false;
        scoreStatement.enabled = false;

        magnetismButton.gameObject.SetActive(false);
        evaporationButton.gameObject.SetActive(false);
        sublimationButton.gameObject.SetActive(false);
    }

    public void OkButtonClick()
    {
        switch (count)
        {
            case 0:
                Destroy(text.gameObject);
                evaporation.enabled = true;
                break;
            case 1:
                Destroy(evaporation.gameObject);
                sublimation.enabled = true;
                break;
            case 2:
                Destroy(sublimation.gameObject);
                text1.enabled = true;
                break;
            case 3:
                Destroy(text1.gameObject);
                statement1.enabled = true;
                part1.enabled = true;
                magnetismButton.gameObject.SetActive(true);
                evaporationButton.gameObject.SetActive(true);
                sublimationButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(false);
                break;
            case 4:
                Destroy(statement2.gameObject);
                statement3.enabled = true;
                statement4.enabled = true;
                break;
            case 5:
                Destroy(statement3.gameObject);
                Destroy(statement4.gameObject);
                Destroy(lesson.gameObject);
                Destroy(okButton.gameObject);
                scoreTitle.enabled = true;
                scoreStatement.enabled = true;
                scoreStatement.text = "You scored " + score*10 + "/30.";
                break;
            default:
                break;
        }
        count++;
    }

    public void MagnetismButtonClick()
    {
        if (part1.enabled)
        {
            part1.enabled = false;
            part2.enabled = true;
            return;
        }

        if (part2.enabled)
        {
            part2.enabled = false;
            part3.enabled = true;
            return;
        }

        if (part3.enabled)
        {
            transitionToLesson();
            score++;
            return;
        }
    }

    public void EvaporationButtonClick()
    {

        if (part1.enabled)
        {
            part1.enabled = false;
            part2.enabled = true;
            score++;
            return;
        }

        if (part2.enabled)
        {
            part2.enabled = false;
            part3.enabled = true;
            return;
        }

        if (part3.enabled)
        {
            transitionToLesson();
            return;
        }
    }

    public void SublimationButtonClick()
    {
        if (part1.enabled)
        {
            part1.enabled = false;
            part2.enabled = true;
            return;
        }

        if (part2.enabled)
        {
            part2.enabled = false;
            part3.enabled = true;
            score++;
            return;
        }

        if (part3.enabled)
        {
            transitionToLesson();
            return;
        }
    }

    public void transitionToLesson()
    {
        statement2.enabled = true;
        lesson.enabled = true;
        okButton.gameObject.SetActive(true);
        Destroy(statement1.gameObject);
        Destroy(part1.gameObject);
        Destroy(part2.gameObject);
        Destroy(part3.gameObject);
        Destroy(magnetismButton.gameObject);
        Destroy(evaporationButton.gameObject);
        Destroy(sublimationButton.gameObject);
    }
}

