using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationSystem : MonoBehaviour
{
    public GameObject painting1Camera;
    public GameObject painting2Camera;
    public GameObject rugCamera;

    public GameObject painting1UI;
    public GameObject painting2UI;
    public GameObject rugUI;

    public GameObject exitButton;

    public GameObject paintingBlocks;
    public GameObject rugBlocks;

    public void Painting1()
    {
        painting1UI.SetActive(true);
        painting1Camera.SetActive(true);
        exitButton.SetActive(false);
        paintingBlocks.SetActive(true);
    }

    public void ExitPainting1()
    {
        painting1UI.SetActive(false);
        painting1Camera.SetActive(false);
        exitButton.SetActive(true);
        paintingBlocks.SetActive(false);
    }

    public void Painting2()
    {
        painting2UI.SetActive(true);
        painting2Camera.SetActive(true);
        exitButton.SetActive(false);
        paintingBlocks.SetActive(true);
    }

    public void ExitPainting2()
    {
        painting2UI.SetActive(false);
        painting2Camera.SetActive(false);
        exitButton.SetActive(true);
        paintingBlocks.SetActive(false);
    }

    public void Rug()
    {
        rugUI.SetActive(true);
        rugCamera.SetActive(true);
        exitButton.SetActive(false);
        rugBlocks.SetActive(true);
    }

    public void ExitRug()
    {
        rugUI.SetActive(false);
        rugCamera.SetActive(false);
        exitButton.SetActive(true);
        rugBlocks.SetActive(false);
    }
}
