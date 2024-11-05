using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationTutorial : MonoBehaviour
{
    public NPCConversation beginDecoratingTutorial;
    public NPCConversation choosingDecorationTutorial;
    public NPCConversation endDecoratingTutorial;
    public NPCConversation suggestionDialogue;

    // Start is called before the first frame update
    void Start()
    {
        if (!ProfileSystem.Get<bool>(ProfileSystem.Variable.DecoratingTutorialDone) && !ProfileSystem.Get<bool>(ProfileSystem.Variable.DecoratingTutorialStarted))
        {
            ConversationManager.Instance.StartConversation(beginDecoratingTutorial);
            ProfileSystem.Set(ProfileSystem.Variable.DecoratingTutorialStarted, true);
        }
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

    public void Suggestion()
    {
        if (!ProfileSystem.Get<bool>(ProfileSystem.Variable.DecoratingTutorialDone))
        {
            ConversationManager.Instance.StartConversation(suggestionDialogue);
        }
    }

    public void ActivateClicker()
    {
        Invoke(nameof(VisibleClicker), 0.3f);
    }

    private void VisibleClicker()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DecoratingTutorialCompleted()
    {
        ProfileSystem.Set(ProfileSystem.Variable.DecoratingTutorialDone, true);
    }
}
