#region

using DialogueEditor;
using UnityEngine;

#endregion

public class Eve : MonoBehaviour
{
    public GameObject eveDrawing;
    public GameObject panelDialogue;
    public GameObject panelOptions;
    public GameObject shopEve;

    public NPCConversation chihuahuaDialogue;

    private void Start()
    {
        ConversationManager.Instance.StartConversation(chihuahuaDialogue);
    }

    public void ShowPainting()
    {
        eveDrawing.SetActive(true);
    }

    public void HidePainting()
    {
        eveDrawing.SetActive(false);
    }

    public void ActivateShopEve()
    {
        shopEve.SetActive(true);
        panelDialogue.SetActive(false);
        panelOptions.SetActive(false);
        MapTest.Instance.AbleToLeaveStation();
    }
}