using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationSelection : MonoBehaviour
{
    public GameObject decorationOption1;
    public GameObject decorationOption2;
    public GameObject decorationOption3;

    public GameObject dottedLine;

    public void paintingOptionOne()
    {
        decorationOption1.SetActive(true);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        dottedLine.SetActive(false);
    }

    public void paintingOptionTwo()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(true);
        decorationOption3.SetActive(false);
        dottedLine.SetActive(false);
    }

    public void paintingOptionThree()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(true);
        dottedLine.SetActive(false);
    }
}
