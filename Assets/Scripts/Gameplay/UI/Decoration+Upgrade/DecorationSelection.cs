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

    public void paintingOptionOne()
    {
        decorationOption1.SetActive(true);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);
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
    }
}
