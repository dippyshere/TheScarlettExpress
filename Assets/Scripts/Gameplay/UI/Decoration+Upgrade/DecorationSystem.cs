#region

using UnityEngine;

#endregion

public class DecorationSystem : MonoBehaviour
{
    public GameObject painting1Camera;

    public GameObject painting1UI;
    public GameObject painting2Camera;
    public GameObject painting2UI;

    public GameObject rugCamera;
    public GameObject rugUI;

    public GameObject sideviewButton;
    public GameObject sterlingButton;
    public GameObject otherSideviewButton;
    public GameObject otherSterlingButton;

    public GameObject otherRoomCamera;
    public GameObject otherRoomUI;
    public GameObject otherRoomButton;

    public GameObject thisRoomCamera;
    public GameObject thisRoomUI;
    public GameObject thisRoomButton;

    public void Painting1()
    {
        painting1UI.SetActive(true);
        painting1Camera.SetActive(true);
        sideviewButton.SetActive(false);
        sterlingButton.SetActive(false);

        otherRoomButton.SetActive(false);
        thisRoomButton.SetActive(false);
    }

    public void ExitPainting1()
    {
        painting1UI.SetActive(false);
        painting1Camera.SetActive(false);
        sideviewButton.SetActive(true);
        sterlingButton.SetActive(true);

        otherRoomButton.SetActive(true);
    }

    public void Painting2()
    {
        painting2UI.SetActive(true);
        painting2Camera.SetActive(true);
        sideviewButton.SetActive(false);
        sterlingButton.SetActive(false);

        otherRoomButton.SetActive(false);
        thisRoomButton.SetActive(false);
    }

    public void ExitPainting2()
    {
        painting2UI.SetActive(false);
        painting2Camera.SetActive(false);
        sideviewButton.SetActive(true);
        sterlingButton.SetActive(true);

        otherRoomButton.SetActive(true);
    }

    public void Rug()
    {
        rugUI.SetActive(true);
        rugCamera.SetActive(true);
        sideviewButton.SetActive(false);
        sterlingButton.SetActive(false);

        otherRoomButton.SetActive(false);
        thisRoomButton.SetActive(false);
    }

    public void ExitRug()
    {
        rugUI.SetActive(false);
        rugCamera.SetActive(false);
        sideviewButton.SetActive(true);
        sterlingButton.SetActive(true);

        otherRoomButton.SetActive(true);
    }

    public void GoToOtherRoom()
    {
        otherRoomButton.SetActive(false);
        otherRoomUI.SetActive(true);
        otherRoomCamera.SetActive(true);

        thisRoomButton.SetActive(true);
        thisRoomUI.SetActive(false);
        thisRoomCamera.SetActive(false);

        sideviewButton.SetActive(false);
        sterlingButton.SetActive(false);
        otherSideviewButton.SetActive(true);
        otherSterlingButton.SetActive(true);
    }
}