using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorationSelection : MonoBehaviour
{
    public GameObject decorationOption1;
    public GameObject decorationOption2;
    public GameObject decorationOption3;

    //public GameObject dottedLine;

    public Image decoration;

    public void paintingOptionOne()
    {
        decorationOption1.SetActive(true);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);
    }

    public void paintingOptionTwo()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(true);
        decorationOption3.SetActive(false);
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);
    }

    public void paintingOptionThree()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(true);
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);
    }
}
