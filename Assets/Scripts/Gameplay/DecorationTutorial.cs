using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationTutorial : MonoBehaviour
{
    float _money;

    public NPCConversation chihuahuaWelcome;

    public GameObject table1Highlight;
    public GameObject otherTableBlocks;
    public GameObject clipboardUI;
    public GameObject upgradesTab;
    public GameObject mainTab;
    public GameObject upgradeBlocks;

    //decoration dialogue
    public NPCConversation beginDecoratingTutorial;
    public NPCConversation choosingDecorationTutorial;
    public NPCConversation endDecoratingTutorial;
    //public NPCConversation suggestionDialogue;

    //upgrade dialogue
    public NPCConversation beginUpgradeTutorial;
    public NPCConversation upgradesTabDialogue;
    public NPCConversation upgradedChairDialogue;

    // Start is called before the first frame update
    void Start()
    {
        //ProfileSystem.ClearProfile();
        if (!ProfileSystem.Get<bool>(ProfileSystem.Variable.DecoratingTutorialDone) && !ProfileSystem.Get<bool>(ProfileSystem.Variable.DecoratingTutorialStarted) 
            && !ProfileSystem.Get<bool>(ProfileSystem.Variable.UpgradeTutorialDone))
        {
            ConversationManager.Instance.StartConversation(chihuahuaWelcome);
        }

        _money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);

        if (ProfileSystem.Get<bool>(ProfileSystem.Variable.UpgradeTutorialDone))
        {
            upgradeBlocks.SetActive(false);
        }
    }

    private void Update()
    {
        _money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);

        if (!ProfileSystem.Get<bool>(ProfileSystem.Variable.UpgradeTutorialDone))
        {
            otherTableBlocks.SetActive(true);
        }

        if (ProfileSystem.Get<bool>(ProfileSystem.Variable.StartedUpgradeTutorial) && !ProfileSystem.Get<bool>(ProfileSystem.Variable.UpgradeTutorialDone))
        {
            upgradeBlocks.SetActive(true);
        }

        if (ProfileSystem.Get<bool>(ProfileSystem.Variable.UpgradeTutorialDone))
        {
            upgradeBlocks.SetActive(false);
        }
    }

    //upgrade tutorial

    public void StartUpgradeDialogue()
    {
        Invoke(nameof(ActivateUpgradeDialogue), 0.3f);
        //ConversationManager.Instance.StartConversation(beginUpgradeTutorial);
    }

    public void ActivateUpgradeDialogue()
    {
        ConversationManager.Instance.StartConversation(beginUpgradeTutorial);
    }

    public void OpenedUpgradeTab()
    {
        if (!ProfileSystem.Get<bool>(ProfileSystem.Variable.UpgradeTutorialDone))
        {
            ConversationManager.Instance.StartConversation(upgradesTabDialogue);
            table1Highlight.SetActive(true);
            ProfileSystem.Set(ProfileSystem.Variable.StartedUpgradeTutorial, true);
        }
    }

    public void ChihuahuaGiveMoney()
    {
        if (!ProfileSystem.Get<bool>(ProfileSystem.Variable.UpgradeTutorialDone))
        {
            _money += 25;
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, _money);
        }
        //_money += 25;
    }

    public void UpgradedChair()
    {
        //table1Highlight.SetActive(false);
        //clipboardUI.SetActive(false);
        //otherTableBlocks.SetActive(false);
        //mainTab.SetActive(true);
        //upgradesTab.SetActive(false);

        if (!ProfileSystem.Get<bool>(ProfileSystem.Variable.UpgradeTutorialDone))
        {
            ConversationManager.Instance.StartConversation(upgradedChairDialogue);
            table1Highlight.SetActive(false);
            clipboardUI.SetActive(false);
            otherTableBlocks.SetActive(false);
            mainTab.SetActive(true);
            upgradesTab.SetActive(false);
        }
    }

    public void FinishUpgradeTutorial()
    {
        ProfileSystem.Set(ProfileSystem.Variable.UpgradeTutorialDone, true);

        if (!ProfileSystem.Get<bool>(ProfileSystem.Variable.DecoratingTutorialDone) && !ProfileSystem.Get<bool>(ProfileSystem.Variable.DecoratingTutorialStarted))
        {
            Invoke(nameof(ActivateDecorationDialogue), 0.3f);
            ProfileSystem.Set(ProfileSystem.Variable.DecoratingTutorialStarted, true);
        }
    }

    //decoration tutorial

    public void ActivateDecorationDialogue()
    {
        ConversationManager.Instance.StartConversation(beginDecoratingTutorial);
    }

    public void ChooseDecorationDialogue()
    {
        if (!ProfileSystem.Get<bool>(ProfileSystem.Variable.DecoratingTutorialDone))
        {
            ConversationManager.Instance.StartConversation(choosingDecorationTutorial);
        }
    }

    public void EndDecoratingTutorial()
    {
        if (!ProfileSystem.Get<bool>(ProfileSystem.Variable.DecoratingTutorialDone))
        {
            ConversationManager.Instance.StartConversation(endDecoratingTutorial);
        }
    }

    //public void Suggestion()
    //{
    //    if (!ProfileSystem.Get<bool>(ProfileSystem.Variable.DecoratingTutorialDone))
    //    {
    //        ConversationManager.Instance.StartConversation(suggestionDialogue);
    //    }
    //}

    public void ActivateClicker()
    {
        Invoke(nameof(VisibleClicker), 0.25f);
    }

    private void VisibleClicker()
    {
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
        CameraManager.Instance.SetInputModeUI(true);
    }

    public void DecoratingTutorialCompleted()
    {
        ProfileSystem.Set(ProfileSystem.Variable.DecoratingTutorialDone, true);
    }
}
