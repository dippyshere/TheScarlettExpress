#region

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace DialogueEditor
{
    public class UIConversationButton : MonoBehaviour
    {
        // Node data
        eHoverState m_hoverState;

        // Hovering 
        float m_hoverT;
        ConversationNode m_node;
        RectTransform m_rect;
        [SerializeField] Image OptionBackgroundImage;

        // UI Elements
        [SerializeField] TextMeshProUGUI TextMesh;

        // Getters
        public eButtonType ButtonType { get; private set; }

        bool Hovering
        {
            get { return m_hoverState == eHoverState.animatingOn || m_hoverState == eHoverState.animatingOff; }
        }

        Vector3 BigSize
        {
            get { return Vector3.one * 1.2f; }
        }


        //--------------------------------------
        // MonoBehaviour
        //--------------------------------------

        void Awake()
        {
            m_rect = GetComponent<RectTransform>();
        }

        void Update()
        {
            if (Hovering)
            {
                m_hoverT += Time.deltaTime;
                float normalised = m_hoverT / 0.2f;
                bool done = false;
                if (normalised >= 1)
                {
                    normalised = 1;
                    done = true;
                }

                Vector3 size = Vector3.one;
                float ease = EaseOutQuart(normalised);


                switch (m_hoverState)
                {
                    case eHoverState.animatingOn:
                        size = Vector3.Lerp(Vector3.one, BigSize, ease);
                        break;
                    case eHoverState.animatingOff:
                        size = Vector3.Lerp(BigSize, Vector3.one, ease);
                        break;
                }

                m_rect.localScale = size;

                if (done)
                {
                    m_hoverState = m_hoverState == eHoverState.animatingOn ? eHoverState.idleOn : eHoverState.idleOff;
                }
            }
        }


        //--------------------------------------
        // Input Events
        //--------------------------------------

        public void OnHover(bool hovering)
        {
            if (!ConversationManager.Instance.AllowMouseInteraction)
            {
                return;
            }

            if (hovering)
            {
                ConversationManager.Instance.AlertHover(this);
            }
            else
            {
                ConversationManager.Instance.AlertHover(null);
            }
        }

        public void OnClick()
        {
            if (!ConversationManager.Instance.AllowMouseInteraction)
            {
                return;
            }

            DoClickBehaviour();
        }

        public void OnButtonPressed()
        {
            DoClickBehaviour();
        }


        //--------------------------------------
        // Public calls
        //--------------------------------------

        public void SetHovering(bool selected)
        {
            switch (selected)
            {
                case true when (m_hoverState == eHoverState.animatingOn || m_hoverState == eHoverState.idleOn):
                case false when (m_hoverState == eHoverState.animatingOff || m_hoverState == eHoverState.idleOff):
                    return;
                case true:
                    m_hoverState = eHoverState.animatingOn;
                    break;
                default:
                    m_hoverState = eHoverState.animatingOff;
                    break;
            }

            m_hoverT = 0f;
        }

        public void SetImage(Sprite sprite, bool sliced)
        {
            if (sprite != null)
            {
                OptionBackgroundImage.sprite = sprite;

                if (sliced)
                {
                    OptionBackgroundImage.type = Image.Type.Sliced;
                }
                else
                {
                    OptionBackgroundImage.type = Image.Type.Simple;
                }
            }
        }

        public void InitButton(OptionNode option)
        {
            // Set font
            if (option.TMPFont != null)
            {
                TextMesh.font = option.TMPFont;
            }
            else
            {
                TextMesh.font = null;
            }
        }

        public void SetAlpha(float a)
        {
            Color c_image = OptionBackgroundImage.color;
            Color c_text = TextMesh.color;
            c_image.a = a;
            c_text.a = a;
            OptionBackgroundImage.color = c_image;
            TextMesh.color = c_text;
        }

        public void SetupButton(eButtonType buttonType, ConversationNode node, TMP_FontAsset continueFont = null,
            TMP_FontAsset endFont = null)
        {
            ButtonType = buttonType;
            m_node = node;

            switch (ButtonType)
            {
                case eButtonType.Option:
                {
                    TextMesh.text = node.Text;
                    TextMesh.font = node.TMPFont;
                }
                    break;

                case eButtonType.Speech:
                {
                    TextMesh.text = "Continue.";
                    TextMesh.font = continueFont;
                }
                    break;

                case eButtonType.End:
                {
                    TextMesh.text = "End.";
                    TextMesh.font = endFont;
                }
                    break;
            }
        }


        //--------------------------------------
        // Private logic
        //--------------------------------------

        void DoClickBehaviour()
        {
            switch (ButtonType)
            {
                case eButtonType.Speech:
                    ConversationManager.Instance.SpeechSelected(m_node as SpeechNode);
                    break;

                case eButtonType.Option:
                    ConversationManager.Instance.OptionSelected(m_node as OptionNode);
                    break;

                case eButtonType.End:
                    ConversationManager.Instance.EndButtonSelected();
                    break;
            }
        }


        //--------------------------------------
        // Util
        //--------------------------------------

        static float EaseOutQuart(float normalized)
        {
            return 1 - Mathf.Pow(1 - normalized, 4);
        }

        public enum eHoverState
        {
            idleOff,
            animatingOn,
            idleOn,
            animatingOff
        }

        public enum eButtonType
        {
            Option,
            Speech,
            End
        }
    }
}