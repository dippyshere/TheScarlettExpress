#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public class DecorationSelection : MonoBehaviour
{
    //public GameObject dottedLine;

    public Image decoration;
    public GameObject decorationOption1;
    public GameObject decorationOption2;
    public GameObject decorationOption3;
    public GameObject decorationOption4;
    public GameObject decorationOption5;
    public GameObject decorationOption6;

    [SerializeField] ProfileSystem.Variable decorationSaveKey = ProfileSystem.Variable.Bed1Painting1;

    void Start()
    {
        int decorationSelection = ProfileSystem.Get<int>(decorationSaveKey);
        switch (decorationSelection)
        {
            case 1:
                paintingOptionOne();
                break;
            case 2:
                PaintingOptionTwo();
                break;
            case 3:
                PaintingOptionThree();
                break;
            case 4:
                PaintingOptionFour();
                break;
            case 5:
                PaintingOptionFive();
                break;
            case 6:
                PaintingOptionSix();
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
        if (decorationOption6 != null)
        {
            decorationOption6.SetActive(false);
        }
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 1);
    }

    public void PaintingOptionTwo()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(true);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        if (decorationOption6 != null)
        {
            decorationOption6.SetActive(false);
        }
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 2);
    }

    public void PaintingOptionThree()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(true);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        if (decorationOption6 != null)
        {
            decorationOption6.SetActive(false);
        }
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 3);
    }

    public void PaintingOptionFour()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(true);
        decorationOption5.SetActive(false);
        if (decorationOption6 != null)
        {
            decorationOption6.SetActive(false);
        }
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 4);
    }

    public void PaintingOptionFive()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(true);
        if (decorationOption6 != null)
        {
            decorationOption6.SetActive(false);
        }
        //dottedLine.SetActive(false);

        decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 5);
    }

    public void PaintingOptionSix()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        if (decorationOption6 != null)
        {
            decorationOption6.SetActive(true);
        }

        decoration.color = new Color(1, 1, 1, 0);
        ProfileSystem.Set(decorationSaveKey, 6);
    }
}