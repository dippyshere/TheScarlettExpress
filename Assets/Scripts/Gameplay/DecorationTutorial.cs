using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationTutorial : MonoBehaviour
{
    public NPCConversation beginDecoratingTutorial;
    public NPCConversation choosingDecorationTutorial;
    public NPCConversation endDecoratingTutorial;

    bool hasCompletedDTutorial;

    // Start is called before the first frame update
    void Start()
    {
        hasCompletedDTutorial = ProfileSystem.Get<bool>(ProfileSystem.Variable.DecoratingTutorialDone);

        if (!hasCompletedDTutorial)
        {
            ConversationManager.Instance.StartConversation(beginDecoratingTutorial);
        }
    }

    // Update is called once per frame
    void Update()
    {
        hasCompletedDTutorial = ProfileSystem.Get<bool>(ProfileSystem.Variable.DecoratingTutorialDone);
    }

    public void ChooseDecorationDialogue()
    {
        if (!hasCompletedDTutorial)
        {
            ConversationManager.Instance.StartConversation(choosingDecorationTutorial);
        }
    }

    public void EndDecoratingTutorial()
    {
        if (!hasCompletedDTutorial)
        {
            ConversationManager.Instance.StartConversation(endDecoratingTutorial);
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
        //hasCompletedDTutorial = true;
    }
}
