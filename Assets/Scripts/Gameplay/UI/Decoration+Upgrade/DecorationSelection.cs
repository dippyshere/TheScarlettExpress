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
    public GameObject decorationOption7;
    public GameObject decorationOption8;
    public GameObject decorationOption9;
    public GameObject decorationOption10;
    public GameObject decorationOption11;
    public GameObject decorationOption12;
    public GameObject decorationOption13;
    public GameObject decorationOption14;
    public GameObject decorationOption15;

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
            case 7:
                PaintingOptionSeven();
                break;
            case 8:
                PaintingOptionEight();
                break;
            case 9:
                PaintingOptionNine();
                break;
            case 10:
                PaintingOptionTen();
                break;
            case 11:
                PaintingOptionEleven();
                break;
            case 12:
                PaintingOptionTwelve();
                break;
            case 13:
                PaintingOptionThirteen();
                break;
            case 14:
                PaintingOptionFourteen();
                break;
            case 15:
                PaintingOptionFifteen();
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
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }
        //dottedLine.SetActive(false);

        // decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 1);
    }

    public void PaintingOptionTwo()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(true);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }
        //dottedLine.SetActive(false);

        // decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 2);
    }

    public void PaintingOptionThree()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(true);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }
        //dottedLine.SetActive(false);

        // decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 3);
    }

    public void PaintingOptionFour()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(true);
        decorationOption5.SetActive(false);
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }
        //dottedLine.SetActive(false);

        // decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 4);
    }

    public void PaintingOptionFive()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(true);
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }
        //dottedLine.SetActive(false);

        // decoration.color = new Color(1, 1, 1, 0);

        ProfileSystem.Set(decorationSaveKey, 5);
    }

    public void PaintingOptionSix()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        decorationOption6.SetActive(true);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }

        // decoration.color = new Color(1, 1, 1, 0);
        ProfileSystem.Set(decorationSaveKey, 6);
    }

    public void PaintingOptionSeven()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(true);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }

        // decoration.color = new Color(1, 1, 1, 0);
        ProfileSystem.Set(decorationSaveKey, 7);
    }

    public void PaintingOptionEight()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(true);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }

        // decoration.color = new Color(1, 1, 1, 0);
        ProfileSystem.Set(decorationSaveKey, 8);
    }

    public void PaintingOptionNine()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(true);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }

        // decoration.color = new Color(1, 1, 1, 0);
        ProfileSystem.Set(decorationSaveKey, 9);
    }

    public void PaintingOptionTen()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(true);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }

        // decoration.color = new Color(1, 1, 1, 0);
        ProfileSystem.Set(decorationSaveKey, 10);
    }

    public void PaintingOptionEleven()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(true);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }

        // decoration.color = new Color(1, 1, 1, 0);
        ProfileSystem.Set(decorationSaveKey, 11);
    }

    public void PaintingOptionTwelve()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(true);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }

        // decoration.color = new Color(1, 1, 1, 0);
        ProfileSystem.Set(decorationSaveKey, 12);
    }

    public void PaintingOptionThirteen()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(true);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }

        // decoration.color = new Color(1, 1, 1, 0);
        ProfileSystem.Set(decorationSaveKey, 13);
    }

    public void PaintingOptionFourteen()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(true);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(false);
        }

        // decoration.color = new Color(1, 1, 1, 0);
        ProfileSystem.Set(decorationSaveKey, 14);
    }

    public void PaintingOptionFifteen()
    {
        decorationOption1.SetActive(false);
        decorationOption2.SetActive(false);
        decorationOption3.SetActive(false);
        decorationOption4.SetActive(false);
        decorationOption5.SetActive(false);
        decorationOption6.SetActive(false);
        decorationOption7.SetActive(false);
        decorationOption8.SetActive(false);
        decorationOption9.SetActive(false);
        decorationOption10.SetActive(false);
        decorationOption11.SetActive(false);
        decorationOption12.SetActive(false);
        decorationOption13.SetActive(false);
        decorationOption14.SetActive(false);

        if (decorationOption15 != null)
        {
            decorationOption15.SetActive(true);
        }

        // decoration.color = new Color(1, 1, 1, 0);
        ProfileSystem.Set(decorationSaveKey, 15);
    }

}