﻿#region

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace DialogueEditor
{
    public class ConversationManager : MonoBehaviour
    {
        public delegate void ConversationEndEvent();

        public delegate void ConversationStartEvent();

        const float TRANSITION_TIME = 0.2f; // Transition time for fades

        public static ConversationStartEvent OnConversationStarted;
        public static ConversationEndEvent OnConversationEnded;

        public bool AllowMouseInteraction;

        // Components
        public AudioSource AudioPlayer;
        public Sprite BackgroundImage;

        public bool BackgroundImageSliced;

        // Default values
        public Sprite BlankSprite;

        // Prefabs
        public UIConversationButton ButtonPrefab;

        // Dialogue UI
        public Image DialogueBackground;


        // Non-User facing 
        // Not exposed via custom inspector
        // 
        // Base panels
        public RectTransform DialoguePanel;
        public TextMeshProUGUI DialogueText;
        public int m_targetScrollTextCount;
        public TextMeshProUGUI NameText;
        public Image NpcIcon;
        public Sprite OptionImage;
        public bool OptionImageSliced;
        public RectTransform OptionsPanel;
        public float ScrollSpeed = 1;

        // User-Facing options
        // Drawn by custom inspector
        public bool ScrollText;

        Conversation m_conversation;
        int m_currentSelectedIndex;
        SpeechNode m_currentSpeech;

        // Private
        float m_elapsedScrollTime;

        int m_scrollIndex;
        OptionNode m_selectedOption;
        eState m_state;
        float m_stateTime;
        
        public Sprite ChihuahuaImage;
        public Sprite EveImage;
        public Sprite SterlingImage;
        public Sprite BrownImage;
        public Sprite GreenImage;
        public Sprite PinkImage;
        public Sprite RedImage;
        public Sprite YellowImage;
        public Sprite JosephImage;
        public Sprite MatildaImage;
        public Sprite MadamFruFruImage;
        public Sprite EbonyImage;
        
        public VoiceBank ChihuahuaVoiceBank;
        public VoiceBank EbonyVoiceBank;
        public VoiceBank EveVoiceBank;
        public VoiceBank JosephVoiceBank;
        public VoiceBank MatildaVoiceBank;

        // Selection options
        List<UIConversationButton> m_uiOptions;

        public static ConversationManager Instance { get; private set; }

        // Getter properties
        public bool IsConversationActive
        {
            get { return m_state != eState.NONE && m_state != eState.Off; }
        }


        //--------------------------------------
        // Awake, Start, Destroy, Update
        //--------------------------------------


        void Awake()
        {
            // Destroy myself if I am not the singleton
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }

            Instance = this;

            m_uiOptions = new List<UIConversationButton>();

            NpcIcon.sprite = BlankSprite;
            DialogueText.text = "";
            TurnOffUI();
        }

        void Update()
        {
            switch (m_state)
            {
                case eState.TransitioningDialogueBoxOn:
                    TransitioningDialogueBoxOn_Update();
                    break;

                case eState.ScrollingText:
                    ScrollingText_Update();
                    break;

                case eState.TransitioningOptionsOn:
                    TransitionOptionsOn_Update();
                    break;

                case eState.Idle:
                    Idle_Update();
                    break;

                case eState.TransitioningOptionsOff:
                    TransitionOptionsOff_Update();
                    break;

                case eState.TransitioningDialogueOff:
                    TransitioningDialogueBoxOff_Update();
                    break;
            }
        }

        void OnDestroy()
        {
            Instance = null;
        }


        //--------------------------------------
        // Public functions
        //--------------------------------------

        public void StartConversation(NPCConversation conversation)
        {
            m_conversation = conversation.Deserialize();
            OnConversationStarted?.Invoke();

            TurnOnUI();
            m_currentSpeech = m_conversation.Root;
            SetState(eState.TransitioningDialogueBoxOn);
        }

        public void EndConversation()
        {
            SetState(eState.TransitioningDialogueOff);

            OnConversationEnded?.Invoke();
        }

        public void SelectNextOption()
        {
            int next = m_currentSelectedIndex + 1;
            if (next > m_uiOptions.Count - 1)
            {
                next = 0;
            }

            SetSelectedOption(next);
        }

        public void SelectPreviousOption()
        {
            int previous = m_currentSelectedIndex - 1;
            if (previous < 0)
            {
                previous = m_uiOptions.Count - 1;
            }

            SetSelectedOption(previous);
        }

        public void PressSelectedOption()
        {
            if (m_state != eState.Idle)
            {
                return;
            }

            if (m_currentSelectedIndex < 0)
            {
                return;
            }

            if (m_currentSelectedIndex >= m_uiOptions.Count)
            {
                return;
            }

            if (m_uiOptions.Count == 0)
            {
                return;
            }

            UIConversationButton button = m_uiOptions[m_currentSelectedIndex];
            button.OnButtonPressed();
        }

        public void AlertHover(UIConversationButton button)
        {
            for (int i = 0; i < m_uiOptions.Count; i++)
            {
                if (m_uiOptions[i] == button && m_currentSelectedIndex != i)
                {
                    SetSelectedOption(i);
                    return;
                }
            }

            if (button == null)
            {
                UnselectOption();
            }
        }

        public void SetInt(string paramName, int value)
        {
            eParamStatus status;
            m_conversation.SetInt(paramName, value, out status);

            if (status == eParamStatus.NoParamFound)
            {
                LogWarning("parameter \'" + paramName + "\' does not exist.");
            }
        }

        public void SetBool(string paramName, bool value)
        {
            eParamStatus status;
            m_conversation.SetBool(paramName, value, out status);

            if (status == eParamStatus.NoParamFound)
            {
                LogWarning("parameter \'" + paramName + "\' does not exist.");
            }
        }

        public int GetInt(string paramName)
        {
            eParamStatus status;
            int value = m_conversation.GetInt(paramName, out status);

            if (status == eParamStatus.NoParamFound)
            {
                LogWarning("parameter \'" + paramName + "\' does not exist.");
            }

            return value;
        }

        public bool GetBool(string paramName)
        {
            eParamStatus status;
            bool value = m_conversation.GetBool(paramName, out status);

            if (status == eParamStatus.NoParamFound)
            {
                LogWarning("parameter \'" + paramName + "\' does not exist.");
            }

            return value;
        }


        //--------------------------------------
        // Set state
        //--------------------------------------

        void SetState(eState newState)
        {
            // Exit
            switch (m_state)
            {
                case eState.TransitioningOptionsOff:
                    m_selectedOption = null;
                    break;
                case eState.TransitioningDialogueBoxOn:
                    SetColorAlpha(DialogueBackground, 1);
                    SetColorAlpha(NpcIcon, 1);
                    SetColorAlpha(NameText, 1);
                    break;
            }

            m_state = newState;
            m_stateTime = 0f;

            // Enter 
            switch (m_state)
            {
                case eState.TransitioningDialogueBoxOn:
                {
                    SetColorAlpha(DialogueBackground, 0);
                    SetColorAlpha(NpcIcon, 0);
                    SetColorAlpha(NameText, 0);

                    DialogueText.text = "";
                    NameText.text = m_currentSpeech.Name;
                    if (!m_currentSpeech.Icon)
                    {
                        switch (m_currentSpeech.CharacterImage)
                        {
                            case SpeechNode.eCharacter.Chihuahua:
                                NpcIcon.sprite = ChihuahuaImage;
                                break;
                            case SpeechNode.eCharacter.Eve:
                                NpcIcon.sprite = EveImage;
                                break;
                            case SpeechNode.eCharacter.Sterling:
                                NpcIcon.sprite = SterlingImage;
                                break;
                            case SpeechNode.eCharacter.Brown:
                                NpcIcon.sprite = BrownImage;
                                break;
                            case SpeechNode.eCharacter.Green:
                                NpcIcon.sprite = GreenImage;
                                break;
                            case SpeechNode.eCharacter.Pink:
                                NpcIcon.sprite = PinkImage;
                                break;
                            case SpeechNode.eCharacter.Red:
                                NpcIcon.sprite = RedImage;
                                break;
                            case SpeechNode.eCharacter.Yellow:
                                NpcIcon.sprite = YellowImage;
                                break;
                            case SpeechNode.eCharacter.Joseph:
                                NpcIcon.sprite = JosephImage;
                                break;
                            case SpeechNode.eCharacter.Matilda:
                                NpcIcon.sprite = MatildaImage;
                                break;
                            case SpeechNode.eCharacter.MadamFruFru:
                                NpcIcon.sprite = MadamFruFruImage;
                                break;
                            case SpeechNode.eCharacter.Ebony:
                                NpcIcon.sprite = EbonyImage;
                                break;
                        }
                    }
                    else
                    {
                        NpcIcon.sprite = m_currentSpeech.Icon;
                    }
                }
                    break;

                case eState.ScrollingText:
                {
                    SetColorAlpha(DialogueText, 1);
                }
                    break;

                case eState.TransitioningOptionsOn:
                {
                    SetColorAlpha(DialogueText, 1);

                    CreateUIOptions();

                    for (int i = 0; i < m_uiOptions.Count; i++)
                    {
                        m_uiOptions[i].gameObject.SetActive(true);
                    }
                }
                    break;
            }
        }


        //--------------------------------------
        // Update
        //--------------------------------------

        void TransitioningDialogueBoxOn_Update()
        {
            m_stateTime += Time.deltaTime;
            float t = m_stateTime / TRANSITION_TIME;

            if (t > 1)
            {
                SetupSpeech(m_currentSpeech);
                return;
            }

            SetColorAlpha(DialogueBackground, t);
            SetColorAlpha(NpcIcon, t);
            SetColorAlpha(NameText, t);
        }

        void ScrollingText_Update()
        {
            const float charactersPerSecond = 1500;
            float timePerChar = 60.0f / charactersPerSecond;
            timePerChar *= ScrollSpeed;

            m_elapsedScrollTime += Time.deltaTime;

            if (m_elapsedScrollTime > timePerChar)
            {
                m_elapsedScrollTime = 0f;

                DialogueText.maxVisibleCharacters = m_scrollIndex;
                m_scrollIndex++;

                if (m_currentSpeech.VoiceBank != SpeechNode.eVoiceBank.None)
                {
                    int lastVisibleChar = DialogueText.maxVisibleCharacters;
                    if (lastVisibleChar > 0)
                    {
                        char lastChar = DialogueText.text[lastVisibleChar - 1];
                        VoiceBank bank = m_currentSpeech.VoiceBank switch
                        {
                            SpeechNode.eVoiceBank.Chihuahua => ChihuahuaVoiceBank,
                            SpeechNode.eVoiceBank.Ebony     => EbonyVoiceBank,
                            SpeechNode.eVoiceBank.Eve       => EveVoiceBank,
                            SpeechNode.eVoiceBank.Joseph    => JosephVoiceBank,
                            SpeechNode.eVoiceBank.Matilda   => MatildaVoiceBank,
                            _                               => null
                        };
                        if (bank != null)
                        {
                            AudioClip clip = bank.GetClip(char.ToLower(lastChar));
                            if (clip != null)
                            {
                                AudioPlayer.pitch = bank.pitch;
                                AudioPlayer.PlayOneShot(clip, bank.volume);
                            }
                        }
                    }
                }

                // Finished?
                if (m_scrollIndex >= m_targetScrollTextCount)
                {
                    SetState(eState.TransitioningOptionsOn);
                }
            }
        }

        void TransitionOptionsOn_Update()
        {
            m_stateTime += Time.deltaTime;
            float t = m_stateTime / TRANSITION_TIME;

            if (t > 1)
            {
                SetState(eState.Idle);
                return;
            }

            for (int i = 0; i < m_uiOptions.Count; i++)
            {
                m_uiOptions[i].SetAlpha(t);
            }
        }

        void Idle_Update()
        {
            m_stateTime += Time.deltaTime;

            if (m_currentSpeech.AutomaticallyAdvance)
            {
                if (m_currentSpeech.ConnectionType == Connection.eConnectionType.None ||
                    m_currentSpeech.ConnectionType == Connection.eConnectionType.Speech)
                {
                    if (m_stateTime > m_currentSpeech.TimeUntilAdvance)
                    {
                        SetState(eState.TransitioningOptionsOff);
                    }
                }
            }
        }

        void TransitionOptionsOff_Update()
        {
            m_stateTime += Time.deltaTime;
            float t = m_stateTime / TRANSITION_TIME;

            if (t > 1)
            {
                ClearOptions();

                if (m_currentSpeech.AutomaticallyAdvance)
                {
                    if (IsAutoAdvance())
                    {
                        return;
                    }
                }

                if (m_selectedOption == null)
                {
                    EndConversation();
                    return;
                }

                SpeechNode nextSpeech = GetValidSpeechOfNode(m_selectedOption);
                if (nextSpeech == null)
                {
                    EndConversation();
                }
                else
                {
                    SetupSpeech(nextSpeech);
                }

                return;
            }


            for (int i = 0; i < m_uiOptions.Count; i++)
            {
                m_uiOptions[i].SetAlpha(1 - t);
            }

            SetColorAlpha(DialogueText, 1 - t);
        }

        void TransitioningDialogueBoxOff_Update()
        {
            m_stateTime += Time.deltaTime;
            float t = m_stateTime / TRANSITION_TIME;

            if (t > 1)
            {
                TurnOffUI();
                return;
            }

            SetColorAlpha(DialogueBackground, 1 - t);
            SetColorAlpha(NpcIcon, 1 - t);
            SetColorAlpha(NameText, 1 - t);
        }


        //--------------------------------------
        // Do Speech
        //--------------------------------------

        void SetupSpeech(SpeechNode speech)
        {
            if (speech == null)
            {
                EndConversation();
                return;
            }

            m_currentSpeech = speech;

            // Clear current options
            ClearOptions();
            m_currentSelectedIndex = 0;

            // Set sprite
            if (speech.Icon == null)
            {
                switch (speech.CharacterImage)
                {
                    case SpeechNode.eCharacter.Chihuahua:
                        NpcIcon.sprite = ChihuahuaImage;
                        break;
                    case SpeechNode.eCharacter.Eve:
                        NpcIcon.sprite = EveImage;
                        break;
                    case SpeechNode.eCharacter.Sterling:
                        NpcIcon.sprite = SterlingImage;
                        break;
                    case SpeechNode.eCharacter.Brown:
                        NpcIcon.sprite = BrownImage;
                        break;
                    case SpeechNode.eCharacter.Green:
                        NpcIcon.sprite = GreenImage;
                        break;
                    case SpeechNode.eCharacter.Pink:
                        NpcIcon.sprite = PinkImage;
                        break;
                    case SpeechNode.eCharacter.Red:
                        NpcIcon.sprite = RedImage;
                        break;
                    case SpeechNode.eCharacter.Yellow:
                        NpcIcon.sprite = YellowImage;
                        break;
                    case SpeechNode.eCharacter.Joseph:
                        NpcIcon.sprite = JosephImage;
                        break;
                    case SpeechNode.eCharacter.Matilda:
                        NpcIcon.sprite = MatildaImage;
                        break;
                    case SpeechNode.eCharacter.MadamFruFru:
                        NpcIcon.sprite = MadamFruFruImage;
                        break;
                    case SpeechNode.eCharacter.Ebony:
                        NpcIcon.sprite = EbonyImage;
                        break;
                }
            }
            else
            {
                NpcIcon.sprite = speech.Icon;
            }

            // Set font
            if (speech.TMPFont != null)
            {
                DialogueText.font = speech.TMPFont;
            }
            else
            {
                DialogueText.font = null;
            }

            // Set name
            NameText.text = speech.Name;

            // Set text
            if (string.IsNullOrEmpty(speech.Text))
            {
                if (ScrollText)
                {
                    DialogueText.text = "";
                    m_targetScrollTextCount = 0;
                    DialogueText.maxVisibleCharacters = 0;
                    m_elapsedScrollTime = 0f;
                    m_scrollIndex = 0;
                }
                else
                {
                    DialogueText.text = "";
                    DialogueText.maxVisibleCharacters = 1;
                }
            }
            else
            {
                if (ScrollText)
                {
                    DialogueText.text = speech.Text;
                    m_targetScrollTextCount = speech.Text.Length + 1;
                    DialogueText.maxVisibleCharacters = 0;
                    m_elapsedScrollTime = 0f;
                    m_scrollIndex = 0;
                }
                else
                {
                    DialogueText.text = speech.Text;
                    DialogueText.maxVisibleCharacters = speech.Text.Length;
                }
            }

            // Call the event
            if (speech.Event != null)
            {
                speech.Event.Invoke();
            }

            DoParamAction(speech);

            // Play the audio
            if (speech.Audio != null)
            {
                AudioPlayer.clip = speech.Audio;
                AudioPlayer.volume = speech.Volume;
                AudioPlayer.Play();
            }

            if (ScrollText)
            {
                SetState(eState.ScrollingText);
            }
            else
            {
                SetState(eState.TransitioningOptionsOn);
            }
        }


        //--------------------------------------
        // Option Selected
        //--------------------------------------

        public void SpeechSelected(SpeechNode speech)
        {
            SetupSpeech(speech);
        }

        public void OptionSelected(OptionNode option)
        {
            m_selectedOption = option;
            DoParamAction(option);
            if (option.Event != null)
            {
                option.Event.Invoke();
            }

            SetState(eState.TransitioningOptionsOff);
        }

        public void EndButtonSelected()
        {
            m_selectedOption = null;
            SetState(eState.TransitioningOptionsOff);
        }


        //--------------------------------------
        // Util
        //--------------------------------------

        bool IsAutoAdvance()
        {
            switch (m_currentSpeech.ConnectionType)
            {
                case Connection.eConnectionType.Speech:
                {
                    SpeechNode next = GetValidSpeechOfNode(m_currentSpeech);
                    if (next != null)
                    {
                        SetupSpeech(next);
                        return true;
                    }

                    break;
                }
                case Connection.eConnectionType.None:
                    EndConversation();
                    return true;
            }

            return false;
        }

        /// <summary> Returns the first, valid child connection to a Speech Node. </summary>
        SpeechNode GetValidSpeechOfNode(ConversationNode parentNode)
        {
            if (parentNode.ConnectionType != Connection.eConnectionType.Speech)
            {
                return null;
            }

            if (parentNode.Connections.Count == 0)
            {
                return null;
            }

            // Loop through connections, until a valid connection is found.
            for (int i = 0; i < parentNode.Connections.Count; i++)
            {
                SpeechConnection connection = parentNode.Connections[i] as SpeechConnection;
                bool conditionsMet = ConditionsMet(connection);

                if (conditionsMet)
                {
                    return connection.SpeechNode;
                }
            }

            return null;
        }

        void TurnOnUI()
        {
            DialoguePanel.gameObject.SetActive(true);
            OptionsPanel.gameObject.SetActive(true);

            if (BackgroundImage != null)
            {
                DialogueBackground.sprite = BackgroundImage;

                if (BackgroundImageSliced)
                {
                    DialogueBackground.type = Image.Type.Sliced;
                }
                else
                {
                    DialogueBackground.type = Image.Type.Simple;
                }
            }

            NpcIcon.sprite = BlankSprite;
        }

        void TurnOffUI()
        {
            DialoguePanel.gameObject.SetActive(false);
            OptionsPanel.gameObject.SetActive(false);
            SetState(eState.Off);
#if UNITY_EDITOR
            // Debug.Log("[ConversationManager]: Conversation UI off.");
#endif
        }

        void CreateUIOptions()
        {
            // Display new options
            if (m_currentSpeech.ConnectionType == Connection.eConnectionType.Option)
            {
                for (int i = 0; i < m_currentSpeech.Connections.Count; i++)
                {
                    OptionConnection connection = m_currentSpeech.Connections[i] as OptionConnection;
                    if (ConditionsMet(connection))
                    {
                        UIConversationButton uiOption = CreateButton();
                        uiOption.SetupButton(UIConversationButton.eButtonType.Option, connection.OptionNode);
                    }
                }
            }
            // Display Continue/End options
            else
            {
                bool notAutoAdvance = !m_currentSpeech.AutomaticallyAdvance;
                bool allowVisibleOptionWithAuto = m_currentSpeech.AutomaticallyAdvance &&
                                                  m_currentSpeech.AutoAdvanceShouldDisplayOption;

                if (notAutoAdvance || allowVisibleOptionWithAuto)
                {
                    switch (m_currentSpeech.ConnectionType)
                    {
                        case Connection.eConnectionType.Speech:
                        {
                            UIConversationButton uiOption = CreateButton();
                            SpeechNode next = GetValidSpeechOfNode(m_currentSpeech);

                            // If there was no valid speech node (due to no conditions being met) this becomes a None button type
                            if (next == null)
                            {
                                uiOption.SetupButton(UIConversationButton.eButtonType.End, null,
                                    endFont: m_conversation.EndConversationFont);
                            }
                            // Else, valid speech node found
                            else
                            {
                                uiOption.SetupButton(UIConversationButton.eButtonType.Speech, next,
                                    m_conversation.ContinueFont);
                            }

                            break;
                        }
                        case Connection.eConnectionType.None:
                        {
                            UIConversationButton uiOption = CreateButton();
                            uiOption.SetupButton(UIConversationButton.eButtonType.End, null,
                                endFont: m_conversation.EndConversationFont);
                            break;
                        }
                    }
                }
            }

            SetSelectedOption(0);

            // Set the button sprite and alpha
            for (int i = 0; i < m_uiOptions.Count; i++)
            {
                m_uiOptions[i].SetImage(OptionImage, OptionImageSliced);
                m_uiOptions[i].SetAlpha(0);
                m_uiOptions[i].gameObject.SetActive(false);
            }
        }

        void ClearOptions()
        {
            while (m_uiOptions.Count != 0)
            {
                Destroy(m_uiOptions[0].gameObject);
                m_uiOptions.RemoveAt(0);
            }
        }

        void SetColorAlpha(MaskableGraphic graphic, float a)
        {
            Color col = graphic.color;
            col.a = a;
            graphic.color = col;
        }

        void SetSelectedOption(int index)
        {
            if (m_uiOptions.Count == 0)
            {
                return;
            }

            if (index < 0)
            {
                index = 0;
            }

            if (index > m_uiOptions.Count - 1)
            {
                index = m_uiOptions.Count - 1;
            }

            if (m_currentSelectedIndex >= 0)
            {
                m_uiOptions[m_currentSelectedIndex].SetHovering(false);
            }

            m_currentSelectedIndex = index;
            m_uiOptions[index].SetHovering(true);
        }

        void UnselectOption()
        {
            if (m_currentSelectedIndex < 0)
            {
                return;
            }

            m_uiOptions[m_currentSelectedIndex].SetHovering(false);
            m_currentSelectedIndex = -1;
        }

        UIConversationButton CreateButton()
        {
            UIConversationButton button = Instantiate(ButtonPrefab, OptionsPanel);
            m_uiOptions.Add(button);
            return button;
        }

        bool ConditionsMet(Connection connection)
        {
            List<Condition> conditions = connection.Conditions;
            for (int i = 0; i < conditions.Count; i++)
            {
                bool conditionMet = false;

                // Int condition
                if (conditions[i].ConditionType == Condition.eConditionType.IntCondition)
                {
                    IntCondition condition = conditions[i] as IntCondition;

                    string paramName = condition.ParameterName;
                    int requiredValue = condition.RequiredValue;
                    eParamStatus status;
                    int currentValue = m_conversation.GetInt(paramName, out status);

                    switch (condition.CheckType)
                    {
                        case IntCondition.eCheckType.equal:
                            conditionMet = currentValue == requiredValue;
                            break;

                        case IntCondition.eCheckType.greaterThan:
                            conditionMet = currentValue > requiredValue;
                            break;

                        case IntCondition.eCheckType.lessThan:
                            conditionMet = currentValue < requiredValue;
                            break;
                    }
                }

                // Bool condition
                if (conditions[i].ConditionType == Condition.eConditionType.BoolCondition)
                {
                    BoolCondition condition = conditions[i] as BoolCondition;

                    string paramName = condition.ParameterName;
                    bool requiredValue = condition.RequiredValue;
                    eParamStatus status;
                    bool currentValue = m_conversation.GetBool(paramName, out status);

                    switch (condition.CheckType)
                    {
                        case BoolCondition.eCheckType.equal:
                            conditionMet = currentValue == requiredValue;
                            break;

                        case BoolCondition.eCheckType.notEqual:
                            conditionMet = currentValue != requiredValue;
                            break;
                    }
                }

                if (!conditionMet)
                {
                    return false;
                }
            }

            return true;
        }

        public void DoParamAction(ConversationNode node)
        {
            if (node.ParamActions == null)
            {
                return;
            }

            for (int i = 0; i < node.ParamActions.Count; i++)
            {
                string name = node.ParamActions[i].ParameterName;

                switch (node.ParamActions[i].ParamActionType)
                {
                    case SetParamAction.eParamActionType.Int:
                    {
                        int val = (node.ParamActions[i] as SetIntParamAction).Value;
                        SetInt(name, val);
                        break;
                    }
                    case SetParamAction.eParamActionType.Bool:
                    {
                        bool val = (node.ParamActions[i] as SetBoolParamAction).Value;
                        SetBool(name, val);
                        break;
                    }
                }
            }
        }

        void LogWarning(string warning)
        {
#if UNITY_EDITOR
            Debug.LogWarning("[Dialogue Editor]: " + warning);
#endif
        }


        enum eState
        {
            TransitioningDialogueBoxOn,
            ScrollingText,
            TransitioningOptionsOn,
            Idle,
            TransitioningOptionsOff,
            TransitioningDialogueOff,
            Off,
            NONE
        }
    }
}