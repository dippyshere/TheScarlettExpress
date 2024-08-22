using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorationSelection : MonoBehaviour
{
    public GameObject decorationOption1;
    public GameObject decorationOption2;
    public GameObject decorationOption3;
    public GameObject decorationOption4;
    public GameObject decorationOption5;

    //public GameObject dottedLine;

    public Image decoration;

    [SerializeField] private ProfileSystem.Variable decorationSaveKey = ProfileSystem.Variable.Bed1Painting1;

    private void Start()
    {
        int decorationSelection = ProfileSystem.Get<int>(decorationSaveKey);
        switch (decorationSelection)
        {
            case 1:
                paintingOptionOne();
                break;
            case 2:
                paintingOptionTwo();
                break;
            case 3:
                paintingOptionThree();
                break;
            case 4:
                paintingOptionFour();
                break;
            case 5:
                paintingOptionFive();
                break;
        }
    }

    public void paintingOptionOne()
    {
        decorationOption1.SetActive(true);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 1);
    }

    public void paintingOptionTwo()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(true);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 2);
    }

    public void paintingOptionThree()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(true);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 3);
    }

    public void paintingOptionFour()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(true);
        decorationOption5.SetActive(false);
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 4);
    }

    public void paintingOptionFive()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(true);
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 5);
    }
}
