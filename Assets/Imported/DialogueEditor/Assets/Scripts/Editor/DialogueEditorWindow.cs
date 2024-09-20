#region

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;
#endif

#endregion

namespace DialogueEditor
{
#if UNITY_EDITOR
    public class DialogueEditorWindow : EditorWindow
    {
        public enum eInputState
        {
            Regular,
            PlacingOption,
            PlacingSpeech,
            ConnectingNode,
            draggingPanel
        }

        // Consts
        public const float TOOLBAR_HEIGHT = 17;
        public const float START_PANEL_WIDTH = 250;
        const float PANEL_RESIZER_PADDING = 5;
        const string WINDOW_NAME = "DIALOGUE_EDITOR_WINDOW";
        const string HELP_URL = "https://grasshopdev.github.io/dialogueeditor.html";
        const string CONTROL_NAME = "DEFAULT_CONTROL";
        public const int MIN_PANEL_WIDTH = 180;
        const string UNAVAILABLE_DURING_PLAY_TEXT = "Dialogue Editor unavaiable during play mode.";
        bool clickInBox;

        // Private variables:     
        NPCConversation CurrentAsset; // The Conversation scriptable object that is currently being viewed/edited

        // Selected asset logic
        NPCConversation currentlySelectedAsset;
        Vector2 dragDelta;

        // Dragging information
        bool dragging;
        SelectableUI m_cachedSelectedObject;
        EditableConversationNode m_connectionDeleteParent, m_connectionDeleteChild;
        UINode m_currentConnectingNode;
        UINode m_currentPlacingNode;

        // Input and input-state logic
        eInputState m_inputState;
        Transform newlySelectedAsset;
        Vector2 offset;
        GUIStyle panelPropertyStyle;
        Rect panelRect;
        Rect panelResizerRect;
        GUIStyle panelStyle;
        GUIStyle panelTitleStyle;

        Vector2 panelVerticalScroll;

        // Right-hand display pannel vars
        float panelWidth;
        GUIStyle resizerStyle;
        List<UINode> uiNodes; // List of all UI nodes

        // Static properties
        public static bool SelectableClickedOnThisUpdate { get; set; }
        static SelectableUI CurrentlySelectedObject { get; set; }
        public static EditableSpeechNode ConversationRoot { get; private set; } // The root node of the conversation


        //--------------------------------------
        // Update
        //--------------------------------------

        void Update()
        {
            if (Application.isPlaying)
            {
                return;
            }

            switch (m_inputState)
            {
                case eInputState.PlacingOption:
                case eInputState.PlacingSpeech:
                    Repaint();
                    break;
            }
        }


        //--------------------------------------
        // OnEnable, OnDisable, OnFocus, LostFocus, 
        // Destroy, SelectionChange, ReloadScripts
        //--------------------------------------

        void OnEnable()
        {
            if (uiNodes == null)
            {
                uiNodes = new List<UINode>();
            }

            InitGUIStyles();

            UINode.OnUINodeSelected += SelectNode;
            UINode.OnUINodeDeleted += DeleteUINode;
            UISpeechNode.OnCreateOption += CreateNewOption;
            UINode.OnCreateSpeech += CreateNewSpeech;
            UINode.OnConnect += ConnectNode;

            name = WINDOW_NAME;
            panelWidth = START_PANEL_WIDTH;

            EditorApplication.playModeStateChanged += PlayModeStateChanged;
        }

        void OnDisable()
        {
            UINode.OnUINodeSelected -= SelectNode;
            UINode.OnUINodeDeleted -= DeleteUINode;
            UISpeechNode.OnCreateOption -= CreateNewOption;
            UINode.OnCreateSpeech -= CreateNewSpeech;
            UINode.OnConnect -= ConnectNode;

            EditorApplication.playModeStateChanged -= PlayModeStateChanged;

            if (Application.isPlaying)
            {
                return;
            }

            Log("Saving. Reason: Disable.");
            Save();
        }

        protected void OnDestroy()
        {
            if (Application.isPlaying)
            {
                return;
            }

            Log("Saving conversation. Reason: Window closed.");
            Save();
        }


        //--------------------------------------
        // Draw
        //--------------------------------------

        void OnGUI()
        {
            if (Application.isPlaying)
            {
                DrawMessageDuringPlay();
                return;
            }

            if (CurrentAsset == null)
            {
                DrawTitleBar();
                if (GUI.changed)
                {
                    Repaint();
                }

                return;
            }

            // Process interactions
            ProcessInput();

            // Draw
            DrawGrid(20, 0.2f, Color.gray);
            DrawGrid(100, 0.4f, Color.gray);
            DrawConnections();
            DrawNodes();
            DrawPanel();
            DrawResizer();
            DrawTitleBar();

            if (GUI.changed)
            {
                Repaint();
            }
        }

        protected void OnFocus()
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (uiNodes == null)
            {
                uiNodes = new List<UINode>();
            }

            // Get asset the user is selecting
            newlySelectedAsset = Selection.activeTransform;

            // If it's not null
            if (newlySelectedAsset != null)
            {
                // If its a conversation scriptable, load new asset
                if (newlySelectedAsset.GetComponent<NPCConversation>() != null)
                {
                    currentlySelectedAsset = newlySelectedAsset.GetComponent<NPCConversation>();

                    if (currentlySelectedAsset != CurrentAsset)
                    {
                        LoadNewAsset(currentlySelectedAsset);
                    }
                }
            }
        }

        protected void OnLostFocus()
        {
            if (Application.isPlaying)
            {
                return;
            }

            bool keepOnWindow = focusedWindow != null && focusedWindow.titleContent.text.Equals("Dialogue Editor");

            if (CurrentAsset != null && !keepOnWindow)
            {
                Log("Saving conversation. Reason: Window Lost Focus.");
                Save();
            }
        }

        protected void OnSelectionChange()
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (uiNodes == null)
            {
                uiNodes = new List<UINode>();
            }

            // Get asset the user is selecting
            newlySelectedAsset = Selection.activeTransform;

            // If it's not null
            if (newlySelectedAsset != null)
            {
                // If it's a different asset and our current asset isn't null, save our current asset
                if (currentlySelectedAsset != null && currentlySelectedAsset != newlySelectedAsset)
                {
                    Log("Saving conversation. Reason: Different asset selected");
                    Save();
                    currentlySelectedAsset = null;
                }

                // If its a conversation scriptable, load new asset
                currentlySelectedAsset = newlySelectedAsset.GetComponent<NPCConversation>();
                if (currentlySelectedAsset != null && currentlySelectedAsset != CurrentAsset)
                {
                    LoadNewAsset(currentlySelectedAsset);
                }
                else
                {
                    CurrentAsset = null;
                    Repaint();
                }
            }
            else
            {
                Log("Saving conversation. Reason: Conversation asset de-selected");
                Save();

                CurrentAsset = null;
                currentlySelectedAsset = null;
                Repaint();
            }
        }


        //--------------------------------------
        // Open window
        //--------------------------------------

        [MenuItem("Window/DialogueEditor")]
        public static DialogueEditorWindow ShowWindow()
        {
            return GetWindow<DialogueEditorWindow>("Dialogue Editor");
        }

        [OnOpenAsset(1)]
        public static bool OpenDialogue(int assetInstanceID, int line)
        {
            NPCConversation conversation = EditorUtility.InstanceIDToObject(assetInstanceID) as NPCConversation;

            if (conversation != null)
            {
                DialogueEditorWindow window = ShowWindow();
                window.LoadNewAsset(conversation);
                return true;
            }

            return false;
        }


        //--------------------------------------
        // Load New Asset
        //--------------------------------------

        public void LoadNewAsset(NPCConversation asset)
        {
            if (Application.isPlaying)
            {
                Log("Load new asset aborted. Will not open assets during play.");
                return;
            }

            CurrentAsset = asset;
            Log("Loading new asset: " + CurrentAsset.name);

            // Clear all current UI Nodes
            if (uiNodes == null)
            {
                uiNodes = new List<UINode>();
            }

            uiNodes.Clear();

            // Deseralize the asset and get the conversation root
            EditableConversation conversation = CurrentAsset.DeserializeForEditor();

            // Get root
            ConversationRoot = conversation.GetRootNode();

            // If it's null, create a root
            if (ConversationRoot == null)
            {
                ConversationRoot = new EditableSpeechNode();
                ConversationRoot.EditorInfo.xPos = Screen.width / 2 - UISpeechNode.Width / 2;
                ConversationRoot.EditorInfo.yPos = 0;
                ConversationRoot.EditorInfo.isRoot = true;
                conversation.SpeechNodes.Add(ConversationRoot);
            }

            // Create UI
            RecreateUI(conversation);

            // Refresh the Editor window
            Recenter();
            Repaint();

#if UNITY_EDITOR
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
#endif
        }


        public void RecreateUI(EditableConversation conversation)
        {
            // Get a list of every node in the conversation
            List<EditableConversationNode> allNodes = new();
            for (int i = 0; i < conversation.SpeechNodes.Count; i++)
            {
                allNodes.Add(conversation.SpeechNodes[i]);
            }

            for (int i = 0; i < conversation.Options.Count; i++)
            {
                allNodes.Add(conversation.Options[i]);
            }

            // For every node: 
            // Create a corresponding UI Node to represent it, and add it to the list
            // 2: Tell any of the nodes children that the node is the childs parent
            for (int i = 0; i < allNodes.Count; i++)
            {
                EditableConversationNode thisNode = allNodes[i];

                switch (thisNode.NodeType)
                {
                    // 1
                    case EditableConversationNode.eNodeType.Speech:
                    {
                        UISpeechNode uiNode = new(thisNode,
                            new Vector2(thisNode.EditorInfo.xPos, thisNode.EditorInfo.yPos));
                        uiNodes.Add(uiNode);
                        break;
                    }
                    case EditableConversationNode.eNodeType.Option:
                    {
                        UIOptionNode uiNode = new(thisNode,
                            new Vector2(thisNode.EditorInfo.xPos, thisNode.EditorInfo.yPos));
                        uiNodes.Add(uiNode);
                        break;
                    }
                }
            }

            Recenter();
            Repaint();
            MarkSceneDirty();
        }

        void InitGUIStyles()
        {
            // Panel style
            panelStyle = new GUIStyle();
            panelStyle.normal.background = DialogueEditorUtil.MakeTexture(10, 10, DialogueEditorUtil.GetEditorColor());

            // Panel title style
            panelTitleStyle = new GUIStyle();
            panelTitleStyle.alignment = TextAnchor.MiddleCenter;
            panelTitleStyle.fontStyle = FontStyle.Bold;
            panelTitleStyle.wordWrap = true;
            if (EditorGUIUtility.isProSkin)
            {
                panelTitleStyle.normal.textColor = DialogueEditorUtil.ProSkinTextColour;
            }


            // Resizer style
            resizerStyle = new GUIStyle();
            resizerStyle.normal.background = EditorGUIUtility.Load("icons/d_AvatarBlendBackground.png") as Texture2D;
        }

        [DidReloadScripts]
        static void OnScriptsReloaded()
        {
            // Clear our reffrence to the CurrentAsset on script reload in order to prevent 
            // save detection overwriting the object with an empty conversation (save triggerred 
            // with no uiNodes present in window due to recompile). 

            // UPDATE 2021/04/23 - This is no longer neccessary as a better fix has been put in place. Thus, 
            // this can be commented out, and this also prevents the window from opening up again after every recompile. 

            //Log("Scripts reloaded. Clearing current asset.");
            //ShowWindow().CurrentAsset = null;
        }

        void DrawMessageDuringPlay()
        {
            float width = position.width;
            float centerX = width / 2;
            float height = position.height;
            float centerY = height / 2;
            Vector2 textDimensions = GUI.skin.label.CalcSize(new GUIContent(UNAVAILABLE_DURING_PLAY_TEXT));
            Rect textRect = new(centerX - textDimensions.x / 2, centerY - textDimensions.y / 2, textDimensions.x,
                textDimensions.y);
            EditorGUI.LabelField(textRect, UNAVAILABLE_DURING_PLAY_TEXT);
        }

        void DrawTitleBar()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            if (GUILayout.Button("Reset view", EditorStyles.toolbarButton))
            {
                Recenter();
            }

            if (GUILayout.Button("Reset panel", EditorStyles.toolbarButton))
            {
                ResetPanelSize();
            }

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Manual Save", EditorStyles.toolbarButton))
            {
                Save(true);
            }

            if (GUILayout.Button("Help", EditorStyles.toolbarButton))
            {
                Application.OpenURL(HELP_URL);
            }

            GUILayout.EndHorizontal();
        }

        void DrawNodes()
        {
            if (uiNodes != null)
            {
                for (int i = 0; i < uiNodes.Count; i++)
                {
                    uiNodes[i].Draw();
                }
            }
        }

        void DrawConnections()
        {
            EditableConnection selectedConnection = null;
            if (CurrentlySelectedObject != null && CurrentlySelectedObject.Type == SelectableUI.eType.Connection)
            {
                selectedConnection = (CurrentlySelectedObject as SelectableUIConnection).Connection;
            }

            for (int i = 0; i < uiNodes.Count; i++)
            {
                uiNodes[i].DrawConnections(selectedConnection);
            }

            //----

            if (m_inputState == eInputState.ConnectingNode)
            {
                // Validate check
                if (m_currentConnectingNode == null)
                {
                    m_inputState = eInputState.Regular;
                    return;
                }

                Vector2 start, end;
                start = new Vector2(
                    m_currentConnectingNode.rect.x + UIOptionNode.Width / 2,
                    m_currentConnectingNode.rect.y + UIOptionNode.Height / 2
                );
                end = Event.current.mousePosition;

                Vector2 toOption = (start - end).normalized;
                Vector2 toSpeech = (end - start).normalized;

                Handles.DrawBezier(
                    start, end,
                    start + toSpeech * 50f,
                    end + toOption * 50f,
                    Color.black, null, 5f);

                Repaint();
            }
        }

        void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += dragDelta * 0.5f;
            Vector3 newOffset = new(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            // Vertical lines
            for (int i = 0; i < widthDivs; i++)
            {
                Vector3 start = new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset;
                Vector3 end = new Vector3(gridSpacing * i, position.height, 0f) + newOffset;
                Handles.DrawLine(start, end);
            }

            // Horitonzal lines
            for (int j = 0; j < heightDivs; j++)
            {
                Vector3 start = new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset;
                Vector3 end = new Vector3(position.width, gridSpacing * j, 0f) + newOffset;
                Handles.DrawLine(start, end);
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }

        void DrawPanel()
        {
            const int VERTICAL_GAP = 20;
            const int VERTICAL_PADDING = 10;

            panelRect = new Rect(position.width - panelWidth, TOOLBAR_HEIGHT, panelWidth,
                position.height - TOOLBAR_HEIGHT);
            if (panelStyle.normal.background == null)
            {
                InitGUIStyles();
            }

            GUILayout.BeginArea(panelRect, panelStyle);
            GUILayout.BeginVertical();
            panelVerticalScroll = GUILayout.BeginScrollView(panelVerticalScroll);

            GUI.SetNextControlName("CONTROL_TITLE");

            GUILayout.Space(10);

            if (CurrentlySelectedObject == null)
            {
                // Parameters
                if (CurrentAsset.ParameterList == null)
                {
                    CurrentAsset.ParameterList = new List<EditableParameter>();
                }

                GUILayout.Label("Conversation: " + CurrentAsset.gameObject.name, panelTitleStyle);
                GUILayout.Space(VERTICAL_GAP);

                GUILayout.Label("Parameters", panelTitleStyle);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Add bool"))
                {
                    string newname = GetValidParamName("New bool");
                    CurrentAsset.ParameterList.Add(new EditableBoolParameter(newname));
                }

                if (GUILayout.Button("Add int"))
                {
                    string newname = GetValidParamName("New int");
                    CurrentAsset.ParameterList.Add(new EditableIntParameter(newname));
                }

                GUILayout.EndHorizontal();

                for (int i = 0; i < CurrentAsset.ParameterList.Count; i++)
                {
                    GUILayout.BeginHorizontal();

                    float paramNameWidth = panelWidth * 0.6f;
                    CurrentAsset.ParameterList[i].ParameterName = GUILayout.TextField(
                        CurrentAsset.ParameterList[i].ParameterName,
                        EditableParameter.MAX_NAME_SIZE, GUILayout.Width(paramNameWidth), GUILayout.ExpandWidth(false));

                    switch (CurrentAsset.ParameterList[i])
                    {
                        case EditableBoolParameter:
                        {
                            EditableBoolParameter param = CurrentAsset.ParameterList[i] as EditableBoolParameter;
                            param.BoolValue = EditorGUILayout.Toggle(param.BoolValue);
                            break;
                        }
                        case EditableIntParameter:
                        {
                            EditableIntParameter param = CurrentAsset.ParameterList[i] as EditableIntParameter;
                            param.IntValue = EditorGUILayout.IntField(param.IntValue);
                            break;
                        }
                    }

                    if (GUILayout.Button("X"))
                    {
                        CurrentAsset.ParameterList.RemoveAt(i);
                        i--;
                    }

                    GUILayout.EndHorizontal();
                }

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);


                // Default options
                GUILayout.Label("Default Speech-Node values", panelTitleStyle);

                float labelWidth = panelWidth * 0.4f;
                float fieldWidth = panelWidth * 0.6f;

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Default Name:", GUILayout.MinWidth(labelWidth),
                    GUILayout.MaxWidth(labelWidth));
                CurrentAsset.DefaultName =
                    EditorGUILayout.TextField(CurrentAsset.DefaultName, GUILayout.MaxWidth(fieldWidth));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Default Icon:", GUILayout.MinWidth(labelWidth),
                    GUILayout.MaxWidth(labelWidth));
                CurrentAsset.DefaultSprite = (Sprite)EditorGUILayout.ObjectField(CurrentAsset.DefaultSprite,
                    typeof(Sprite), false, GUILayout.MaxWidth(fieldWidth));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Default Font:", GUILayout.MinWidth(labelWidth),
                    GUILayout.MaxWidth(labelWidth));
                CurrentAsset.DefaultFont = (TMP_FontAsset)EditorGUILayout.ObjectField(CurrentAsset.DefaultFont,
                    typeof(TMP_FontAsset), false, GUILayout.MaxWidth(fieldWidth));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                // Font options
                GUILayout.Label("'Continue' and 'End' button font", panelTitleStyle);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("'Continue' font:", GUILayout.MinWidth(labelWidth),
                    GUILayout.MaxWidth(labelWidth));
                CurrentAsset.ContinueFont = (TMP_FontAsset)EditorGUILayout.ObjectField(CurrentAsset.ContinueFont,
                    typeof(TMP_FontAsset), false, GUILayout.MaxWidth(fieldWidth));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("'End' font:", GUILayout.MinWidth(labelWidth),
                    GUILayout.MaxWidth(labelWidth));
                CurrentAsset.EndConversationFont = (TMP_FontAsset)EditorGUILayout.ObjectField(
                    CurrentAsset.EndConversationFont, typeof(TMP_FontAsset), false, GUILayout.MaxWidth(fieldWidth));
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                bool differentNodeSelected = m_cachedSelectedObject != CurrentlySelectedObject;
                m_cachedSelectedObject = CurrentlySelectedObject;
                if (differentNodeSelected)
                {
                    GUI.FocusControl(CONTROL_NAME);
                }

                switch (CurrentlySelectedObject.Type)
                {
                    case SelectableUI.eType.Node:
                    {
                        UINode selectedNode = (CurrentlySelectedObject as SelectableUINode).Node;

                        switch (selectedNode)
                        {
                            case UISpeechNode:
                            {
                                EditableSpeechNode node = selectedNode.Info as EditableSpeechNode;
                                GUILayout.Label("[" + node.ID + "] NPC Dialogue Node.", panelTitleStyle);
                                EditorGUILayout.Space();

                                GUILayout.Label("Character Name", EditorStyles.boldLabel);
                                GUI.SetNextControlName(CONTROL_NAME);
                                node.Name = GUILayout.TextField(node.Name);
                                EditorGUILayout.Space();

                                GUILayout.Label("Dialogue", EditorStyles.boldLabel);
                                node.Text = GUILayout.TextArea(node.Text);
                                EditorGUILayout.Space();

                                // Advance
                                if (node.Connections.Count > 0 && node.Connections[0] is EditableSpeechConnection)
                                {
                                    GUILayout.Label("Auto-Advance options", EditorStyles.boldLabel);
                                    node.AdvanceDialogueAutomatically = EditorGUILayout.Toggle("Automatically Advance",
                                        node.AdvanceDialogueAutomatically);
                                    if (node.AdvanceDialogueAutomatically)
                                    {
                                        node.AutoAdvanceShouldDisplayOption = EditorGUILayout.Toggle("Display continue option",
                                            node.AutoAdvanceShouldDisplayOption);
                                        node.TimeUntilAdvance =
                                            EditorGUILayout.FloatField("Dialogue Time", node.TimeUntilAdvance);
                                        if (node.TimeUntilAdvance < 0.1f)
                                        {
                                            node.TimeUntilAdvance = 0.1f;
                                        }
                                    }

                                    EditorGUILayout.Space();
                                }

                                GUILayout.Label("Icon", EditorStyles.boldLabel);
                                node.Icon = (Sprite)EditorGUILayout.ObjectField(node.Icon, typeof(Sprite), false,
                                    GUILayout.ExpandWidth(true));
                                EditorGUILayout.Space();

                                GUILayout.Label("Audio Options", EditorStyles.boldLabel);
                                GUILayout.Label("Audio");
                                node.Audio = (AudioClip)EditorGUILayout.ObjectField(node.Audio, typeof(AudioClip), false);

                                GUILayout.Label("Audio Volume");
                                node.Volume = EditorGUILayout.Slider(node.Volume, 0, 1);
                                EditorGUILayout.Space();

                                GUILayout.Label("TMP Font", EditorStyles.boldLabel);
                                node.TMPFont =
                                    (TMP_FontAsset)EditorGUILayout.ObjectField(node.TMPFont, typeof(TMP_FontAsset), false);
                                EditorGUILayout.Space();

                                // Event
                                {
                                    NodeEventHolder NodeEvent = CurrentAsset.GetNodeData(node.ID);
                                    if (differentNodeSelected)
                                    {
                                        CurrentAsset.Event = NodeEvent.Event;
                                    }

                                    if (NodeEvent != null && NodeEvent.Event != null)
                                    {
                                        // Load the object and property of the node
                                        SerializedObject o = new(NodeEvent);
                                        SerializedProperty p = o.FindProperty("Event");

                                        // Load the dummy event
                                        SerializedObject o2 = new(CurrentAsset);
                                        SerializedProperty p2 = o2.FindProperty("Event");

                                        // Draw dummy event
                                        GUILayout.Label("Events:", EditorStyles.boldLabel);
                                        EditorGUILayout.PropertyField(p2);

                                        // Apply changes to dummy
                                        o2.ApplyModifiedProperties();

                                        // Copy dummy changes onto the nodes event
                                        p = p2;
                                        o.ApplyModifiedProperties();
                                    }
                                }

                                Panel_NodeParamActions(node);
                                break;
                            }
                            case UIOptionNode:
                            {
                                EditableOptionNode node = selectedNode.Info as EditableOptionNode;
                                GUILayout.Label("[" + node.ID + "] Option Node.", panelTitleStyle);
                                EditorGUILayout.Space();

                                GUILayout.Label("Option text:", EditorStyles.boldLabel);
                                node.Text = GUILayout.TextArea(node.Text);
                                EditorGUILayout.Space();

                                GUILayout.Label("TMP Font", EditorStyles.boldLabel);
                                node.TMPFont =
                                    (TMP_FontAsset)EditorGUILayout.ObjectField(node.TMPFont, typeof(TMP_FontAsset), false);
                                EditorGUILayout.Space();


                                // Event
                                {
                                    NodeEventHolder NodeEvent = CurrentAsset.GetNodeData(node.ID);
                                    if (differentNodeSelected)
                                    {
                                        CurrentAsset.Event = NodeEvent.Event;
                                    }

                                    if (NodeEvent != null && NodeEvent.Event != null)
                                    {
                                        // Load the object and property of the node
                                        SerializedObject o = new(NodeEvent);
                                        SerializedProperty p = o.FindProperty("Event");

                                        // Load the dummy event
                                        SerializedObject o2 = new(CurrentAsset);
                                        SerializedProperty p2 = o2.FindProperty("Event");

                                        // Draw dummy event
                                        GUILayout.Label("Events:", EditorStyles.boldLabel);
                                        EditorGUILayout.PropertyField(p2);

                                        // Apply changes to dummy
                                        o2.ApplyModifiedProperties();

                                        // Copy dummy changes onto the nodes event
                                        p = p2;
                                        o.ApplyModifiedProperties();
                                    }
                                }

                                Panel_NodeParamActions(node);
                                break;
                            }
                        }

                        break;
                    }
                    case SelectableUI.eType.Connection:
                    {
                        GUILayout.Label("Connection.", panelTitleStyle);
                        EditorGUILayout.Space();

                        EditableConnection connection = (CurrentlySelectedObject as SelectableUIConnection).Connection;

                        // Validate conditions
                        for (int i = 0; i < connection.Conditions.Count; i++)
                        {
                            if (CurrentAsset.GetParameter(connection.Conditions[i].ParameterName) == null)
                            {
                                connection.Conditions.RemoveAt(i);
                                i--;
                            }
                        }


                        // Button
                        {
                            GUILayout.BeginHorizontal();
                            if (GUILayout.Button("Add condition"))
                            {
                                GenericMenu rightClickMenu = new();

                                for (int i = 0; i < CurrentAsset.ParameterList.Count; i++)
                                {
                                    // Skip if node already has action for this param
                                    if (ConnectionContainsParameter(connection,
                                            CurrentAsset.ParameterList[i].ParameterName))
                                    {
                                        continue;
                                    }

                                    switch (CurrentAsset.ParameterList[i].ParameterType)
                                    {
                                        case EditableParameter.eParamType.Int:
                                        {
                                            EditableIntParameter intParam =
                                                CurrentAsset.ParameterList[i] as EditableIntParameter;
                                            rightClickMenu.AddItem(new GUIContent(intParam.ParameterName), false,
                                                delegate
                                                {
                                                    connection.AddCondition(new EditableIntCondition(intParam.ParameterName));
                                                });
                                            break;
                                        }
                                        case EditableParameter.eParamType.Bool:
                                        {
                                            EditableBoolParameter boolParam =
                                                CurrentAsset.ParameterList[i] as EditableBoolParameter;
                                            rightClickMenu.AddItem(new GUIContent(boolParam.ParameterName), false,
                                                delegate
                                                {
                                                    connection.AddCondition(new EditableBoolCondition(boolParam.ParameterName));
                                                });
                                            break;
                                        }
                                    }
                                }

                                rightClickMenu.ShowAsContext();
                            }

                            GUILayout.EndHorizontal();
                        }

                        // Draw conditions
                        GUILayout.Space(VERTICAL_PADDING);
                        GUILayout.Label("Required conditions.", EditorStyles.boldLabel);
                        float conditionNameWidth = panelWidth * 0.4f;
                        for (int i = 0; i < connection.Conditions.Count; i++)
                        {
                            GUILayout.BeginHorizontal();

                            string name = connection.Conditions[i].ParameterName;
                            GUILayout.Label(name, GUILayout.MinWidth(conditionNameWidth),
                                GUILayout.MaxWidth(conditionNameWidth));

                            switch (connection.Conditions[i].ConditionType)
                            {
                                case EditableCondition.eConditionType.IntCondition:
                                {
                                    EditableIntCondition intCond = connection.Conditions[i] as EditableIntCondition;

                                    intCond.CheckType =
                                        (EditableIntCondition.eCheckType)EditorGUILayout.EnumPopup(intCond.CheckType);
                                    intCond.RequiredValue = EditorGUILayout.IntField(intCond.RequiredValue);
                                    break;
                                }
                                case EditableCondition.eConditionType.BoolCondition:
                                {
                                    EditableBoolCondition boolCond = connection.Conditions[i] as EditableBoolCondition;

                                    boolCond.CheckType =
                                        (EditableBoolCondition.eCheckType)EditorGUILayout.EnumPopup(boolCond.CheckType);
                                    boolCond.RequiredValue = EditorGUILayout.Toggle(boolCond.RequiredValue);
                                    break;
                                }
                            }

                            if (GUILayout.Button("X"))
                            {
                                connection.Conditions.RemoveAt(i);
                                i--;
                                GUI.changed = true;
                            }

                            GUILayout.EndHorizontal();
                        }

                        break;
                    }
                }
            }

            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        void DrawResizer()
        {
            panelResizerRect = new Rect(
                position.width - panelWidth - 2,
                0,
                5,
                position.height - TOOLBAR_HEIGHT);
            GUILayout.BeginArea(new Rect(panelResizerRect.position, new Vector2(2, position.height)), resizerStyle);
            GUILayout.EndArea();
        }

        void Panel_NodeParamActions(EditableConversationNode node)
        {
            // Param Actions
            GUILayout.Label("Set Param:", EditorStyles.boldLabel);
            {
                // Button
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Add Parameter Action"))
                    {
                        GenericMenu rightClickMenu = new();

                        for (int i = 0; i < CurrentAsset.ParameterList.Count; i++)
                        {
                            // Skip if node already has action for this param
                            if (NodeContainsSetParamAction(node, CurrentAsset.ParameterList[i].ParameterName))
                            {
                                continue;
                            }

                            switch (CurrentAsset.ParameterList[i].ParameterType)
                            {
                                case EditableParameter.eParamType.Int:
                                {
                                    EditableIntParameter intParam = CurrentAsset.ParameterList[i] as EditableIntParameter;
                                    rightClickMenu.AddItem(new GUIContent(intParam.ParameterName), false,
                                        delegate
                                        {
                                            node.ParamActions.Add(new EditableSetIntParamAction(intParam.ParameterName));
                                        });
                                    break;
                                }
                                case EditableParameter.eParamType.Bool:
                                {
                                    EditableBoolParameter boolParam =
                                        CurrentAsset.ParameterList[i] as EditableBoolParameter;
                                    rightClickMenu.AddItem(new GUIContent(boolParam.ParameterName), false,
                                        delegate
                                        {
                                            node.ParamActions.Add(new EditableSetBoolParamAction(boolParam.ParameterName));
                                        });
                                    break;
                                }
                            }
                        }

                        rightClickMenu.ShowAsContext();
                    }

                    GUILayout.EndHorizontal();
                }

                // Validate all params exist
                for (int i = 0; i < node.ParamActions.Count; i++)
                {
                    if (CurrentAsset.GetParameter(node.ParamActions[i].ParameterName) == null)
                    {
                        node.ParamActions.RemoveAt(i);
                        i--;
                    }
                }

                // Draw
                float conditionNameWidth = panelWidth * 0.4f;
                for (int i = 0; i < node.ParamActions.Count; i++)
                {
                    GUILayout.BeginHorizontal();

                    string name = node.ParamActions[i].ParameterName;
                    GUILayout.Label(name, GUILayout.MinWidth(conditionNameWidth),
                        GUILayout.MaxWidth(conditionNameWidth));

                    switch (node.ParamActions[i].ParamActionType)
                    {
                        case EditableSetParamAction.eParamActionType.Int:
                        {
                            EditableSetIntParamAction intAction = node.ParamActions[i] as EditableSetIntParamAction;
                            intAction.Value = EditorGUILayout.IntField(intAction.Value);
                            break;
                        }
                        case EditableSetParamAction.eParamActionType.Bool:
                        {
                            EditableSetBoolParamAction boolAction = node.ParamActions[i] as EditableSetBoolParamAction;
                            boolAction.Value = EditorGUILayout.Toggle(boolAction.Value);
                            break;
                        }
                    }

                    if (GUILayout.Button("X"))
                    {
                        node.ParamActions.RemoveAt(i);
                        i--;
                        GUI.changed = true;
                    }

                    GUILayout.EndHorizontal();
                }
            }
        }


        //--------------------------------------
        // Input
        //--------------------------------------

        void ProcessInput()
        {
            Event e = Event.current;

            switch (m_inputState)
            {
                case eInputState.Regular:
                    bool inPanel = panelRect.Contains(e.mousePosition) || e.mousePosition.y < TOOLBAR_HEIGHT;
                    SelectableClickedOnThisUpdate = false;
                    ProcessNodeEvents(e, inPanel);
                    ProcessConnectionEvents(e, inPanel);
                    ProcessEvents(e);
                    break;

                case eInputState.draggingPanel:
                    panelWidth = position.width - e.mousePosition.x;
                    if (panelWidth < MIN_PANEL_WIDTH)
                    {
                        panelWidth = MIN_PANEL_WIDTH;
                    }

                    if (e.type == EventType.MouseUp && e.button == 0)
                    {
                        m_inputState = eInputState.Regular;
                        e.Use();
                    }

                    Repaint();
                    break;

                case eInputState.PlacingOption:
                    m_currentPlacingNode.SetPosition(e.mousePosition);

                    // Left click
                    if (e.type == EventType.MouseDown && e.button == 0)
                    {
                        // Place the option
                        SelectNode(m_currentPlacingNode, true);
                        m_inputState = eInputState.Regular;
                        Repaint();
                        e.Use();
                    }

                    break;

                case eInputState.PlacingSpeech:
                    m_currentPlacingNode.SetPosition(e.mousePosition);

                    // Left click
                    if (e.type == EventType.MouseDown && e.button == 0)
                    {
                        // Place the option
                        SelectNode(m_currentPlacingNode, true);
                        m_inputState = eInputState.Regular;
                        Repaint();
                        e.Use();
                    }

                    break;

                case eInputState.ConnectingNode:
                    // Click.
                    if (e.type == EventType.MouseDown && e.button == 0)
                    {
                        // Loop through each node
                        for (int i = 0; i < uiNodes.Count; i++)
                        {
                            if (uiNodes[i] == m_currentConnectingNode)
                            {
                                continue;
                            }

                            // Clicked on node
                            if (uiNodes[i].rect.Contains(e.mousePosition))
                            {
                                UINode parent = m_currentConnectingNode;
                                UINode target = uiNodes[i];

                                switch (target)
                                {
                                    // Connecting node->Option
                                    case UIOptionNode node:
                                    {
                                        UIOptionNode targetOption = node;

                                        // Only speech -> option is valid
                                        if (parent is UISpeechNode)
                                        {
                                            (parent as UISpeechNode).SpeechNode.AddOption(targetOption.OptionNode);
                                        }

                                        break;
                                    }
                                    // Connectingnode->Speech
                                    case UISpeechNode node:
                                    {
                                        UISpeechNode targetSpeech = node;

                                        switch (parent)
                                        {
                                            // Connect
                                            case UISpeechNode speechNode:
                                                speechNode.SpeechNode.AddSpeech(targetSpeech.SpeechNode);
                                                break;
                                            case UIOptionNode optionNode:
                                                optionNode.OptionNode.AddSpeech(targetSpeech.SpeechNode);
                                                break;
                                        }

                                        break;
                                    }
                                }

                                m_inputState = eInputState.Regular;
                                e.Use();
                                break;
                            }
                        }
                    }

                    // Esc
                    if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Escape)
                    {
                        m_inputState = eInputState.Regular;
                    }

                    break;
            }
        }

        void ProcessEvents(Event e)
        {
            dragDelta = Vector2.zero;

            switch (e.type)
            {
                case EventType.MouseDown:
                    switch (e.button)
                    {
                        // Left click
                        case 0:
                        {
                            if (panelRect.Contains(e.mousePosition))
                            {
                                clickInBox = true;
                            }
                            else if (InPanelDrag(e.mousePosition))
                            {
                                clickInBox = true;
                                m_inputState = eInputState.draggingPanel;
                            }
                            else if (e.mousePosition.y > TOOLBAR_HEIGHT)
                            {
                                clickInBox = false;
                                if (!SelectableClickedOnThisUpdate)
                                {
                                    UnselectObject();
                                    e.Use();
                                }
                            }

                            break;
                        }
                        // Right click
                        case 1:
                        {
                            if (DialogueEditorUtil.IsPointerNearConnection(uiNodes, e.mousePosition,
                                    out m_connectionDeleteParent, out m_connectionDeleteChild))
                            {
                                GenericMenu rightClickMenu = new();
                                rightClickMenu.AddItem(new GUIContent("Delete connection"), false, DeleteConnection);
                                rightClickMenu.ShowAsContext();
                            }

                            break;
                        }
                    }

                    if (e.button == 0 || e.button == 2)
                    {
                        dragging = true;
                    }
                    else
                    {
                        dragging = false;
                    }

                    break;

                case EventType.MouseDrag:
                    if (dragging && (e.button == 0 || e.button == 2) && !clickInBox && !IsANodeSelected())
                    {
                        OnDrag(e.delta);
                    }

                    break;

                case EventType.MouseUp:
                    dragging = false;
                    break;
            }
        }

        void ProcessNodeEvents(Event e, bool inPanel)
        {
            if (uiNodes != null)
            {
                for (int i = 0; i < uiNodes.Count; i++)
                {
                    bool guiChanged = uiNodes[i].ProcessEvents(e, inPanel);
                    if (guiChanged)
                    {
                        GUI.changed = true;
                    }
                }
            }
        }

        void ProcessConnectionEvents(Event e, bool inPanel)
        {
            if (uiNodes != null && !inPanel && e.type == EventType.MouseDown && e.button == 0)
            {
                EditableConnection selectedConnection;
                bool success =
                    DialogueEditorUtil.IsPointerNearConnection(uiNodes, e.mousePosition, out selectedConnection);

                if (success)
                {
                    SelectableClickedOnThisUpdate = true;
                    SelectConnection(selectedConnection, true);
                }
            }
        }

        void OnDrag(Vector2 delta)
        {
            dragDelta = delta;

            if (uiNodes != null)
            {
                for (int i = 0; i < uiNodes.Count; i++)
                {
                    uiNodes[i].Drag(delta);
                }
            }

            GUI.changed = true;
        }


        //--------------------------------------
        // Event listeners
        //--------------------------------------

        /* -- Creating Nodes -- */

        public void CreateNewOption(UISpeechNode speechUI)
        {
            // Create new option, the argument speech is the options parent
            EditableOptionNode newOption = new();
            newOption.ID = CurrentAsset.CurrentIDCounter++;

            // Give the speech it's default values
            newOption.TMPFont = CurrentAsset.DefaultFont;

            // Add the option to the speechs' list of options
            speechUI.SpeechNode.AddOption(newOption);

            // Create a new UI object to represent the new option
            UIOptionNode ui = new(newOption, Vector2.zero);
            uiNodes.Add(ui);

            // Set the input state appropriately
            m_inputState = eInputState.PlacingOption;
            m_currentPlacingNode = ui;
        }


        public void CreateNewSpeech(UINode node)
        {
            // Create new speech, the argument option is the speechs parent
            EditableSpeechNode newSpeech = new();
            newSpeech.ID = CurrentAsset.CurrentIDCounter++;

            // Give the speech it's default values
            newSpeech.Name = CurrentAsset.DefaultName;
            newSpeech.Icon = CurrentAsset.DefaultSprite;
            newSpeech.TMPFont = CurrentAsset.DefaultFont;

            switch (node)
            {
                // Set this new speech as the options child
                case UIOptionNode optionNode:
                    optionNode.OptionNode.AddSpeech(newSpeech);
                    break;
                case UISpeechNode speechNode:
                    speechNode.SpeechNode.AddSpeech(newSpeech);
                    break;
            }

            // Create a new UI object to represent the new speech
            UISpeechNode ui = new(newSpeech, Vector2.zero);
            uiNodes.Add(ui);

            // Set the input state appropriately
            m_inputState = eInputState.PlacingSpeech;
            m_currentPlacingNode = ui;
        }


        /* -- Connecting Nodes -- */

        public void ConnectNode(UINode option)
        {
            // The option if what we are connecting
            m_currentConnectingNode = option;

            // Set the input state appropriately
            m_inputState = eInputState.ConnectingNode;
        }


        /* -- Deleting Nodes -- */

        public void DeleteUINode(UINode node)
        {
            if (ConversationRoot == node.Info)
            {
                Log("Cannot delete root node.");
                return;
            }

            // Delete tree/internal objects
            node.Info.RemoveSelfFromTree();

            // Delete the EventHolder script if it's an speech node
            CurrentAsset.DeleteDataForNode(node.Info.ID);

            // Delete the UI classes
            uiNodes.Remove(node);
            node = null;

            // "Unselect" what we were looking at.
            CurrentlySelectedObject = null;
        }

        /* -- Deleting connection -- */

        public void DeleteConnection()
        {
            if (m_connectionDeleteParent != null && m_connectionDeleteChild != null)
            {
                // Remove child->parent relationship
                m_connectionDeleteChild.parents.Remove(m_connectionDeleteParent);

                // Remove parent->child relationship
                // Look through each connection the parent has
                // Remove the connection if it points to the child
                for (int i = 0; i < m_connectionDeleteParent.Connections.Count; i++)
                {
                    EditableConnection connection = m_connectionDeleteParent.Connections[i];

                    switch (connection)
                    {
                        case EditableSpeechConnection speechConnection when
                            speechConnection.Speech == m_connectionDeleteChild:
                            m_connectionDeleteParent.Connections.RemoveAt(i);
                            i--;
                            break;
                        case EditableOptionConnection optionConnection when
                            optionConnection.Option == m_connectionDeleteChild:
                            m_connectionDeleteParent.Connections.RemoveAt(i);
                            i--;
                            break;
                    }
                }
            }

            m_connectionDeleteParent = null;
            m_connectionDeleteChild = null;
        }


        //--------------------------------------
        // Util
        //--------------------------------------

        void SelectNode(UINode node, bool selected)
        {
            UnselectObject();

            if (selected)
            {
                CurrentlySelectedObject = new SelectableUINode(node);
                node.SetSelected(true);
            }
        }

        public void SelectConnection(EditableConnection connection, bool selected)
        {
            UnselectObject();

            if (selected)
            {
                CurrentlySelectedObject = new SelectableUIConnection(connection);
            }
        }

        void UnselectObject()
        {
            if (CurrentlySelectedObject != null && CurrentlySelectedObject.Type == SelectableUI.eType.Node)
            {
                (CurrentlySelectedObject as SelectableUINode).Node.SetSelected(false);
            }

            CurrentlySelectedObject = null;
        }

        bool IsANodeSelected()
        {
            if (uiNodes != null)
            {
                for (int i = 0; i < uiNodes.Count; i++)
                {
                    if (uiNodes[i].isSelected)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        bool InPanelDrag(Vector2 pos)
        {
            return pos.x > panelResizerRect.x - panelResizerRect.width - PANEL_RESIZER_PADDING &&
                   pos.x < panelResizerRect.x + panelResizerRect.width + PANEL_RESIZER_PADDING &&
                   pos.y > panelResizerRect.y &&
                   panelResizerRect.y < panelResizerRect.y + panelResizerRect.height;
        }

        public bool NodeContainsSetParamAction(EditableConversationNode node, string parameterName)
        {
            for (int i = 0; i < node.ParamActions.Count; i++)
            {
                if (node.ParamActions[i].ParameterName == parameterName)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ConnectionContainsParameter(EditableConnection connection, string parameterName)
        {
            for (int i = 0; i < connection.Conditions.Count; i++)
            {
                if (connection.Conditions[i].ParameterName == parameterName)
                {
                    return true;
                }
            }

            return false;
        }

        string GetValidParamName(string baseName)
        {
            string newName = baseName;

            if (CurrentAsset.GetParameter(newName) != null)
            {
                int counter = 0;
                do
                {
                    newName = baseName + "_" + counter;
                    counter++;
                } while (CurrentAsset.GetParameter(newName) != null);
            }

            return newName;
        }

        static void Log(string str)
        {
#if DIALOGUE_DEBUG
            Debug.Log("[DialogueEditor]: " + str);
#endif
        }

        void PlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                Log("Saving. Reason: Editor exiting edit mode.");
                Save();
            }
        }

        void MarkSceneDirty()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }
#endif
        }


        //--------------------------------------
        // User / Save functionality
        //--------------------------------------

        void Recenter()
        {
            if (ConversationRoot == null)
            {
                return;
            }

            // Calc delta to move head to (middle, 0) and then apply this to all nodes
            Vector2 target = new(position.width / 2 - UISpeechNode.Width / 2 - panelWidth / 2, TOOLBAR_HEIGHT + 5);
            Vector2 delta = target - new Vector2(ConversationRoot.EditorInfo.xPos, ConversationRoot.EditorInfo.yPos);
            for (int i = 0; i < uiNodes.Count; i++)
            {
                uiNodes[i].Drag(delta);
            }

            Repaint();
        }

        void ResetPanelSize()
        {
            panelWidth = START_PANEL_WIDTH;
        }

        void Save(bool manual = false)
        {
            if (Application.isPlaying)
            {
                Log("Save failed. Reason: Play mode.");
                return;
            }

            if (CurrentAsset != null)
            {
                EditableConversation conversation = new();

                // Prepare each node for serialization
                for (int i = 0; i < uiNodes.Count; i++)
                {
                    uiNodes[i].Info.SerializeAssetData(CurrentAsset);
                }

                // Now that each node has been prepared for serialization: 
                // - Register the UIDs of their parents/children
                // - Add it to the conversation
                for (int i = 0; i < uiNodes.Count; i++)
                {
                    uiNodes[i].Info.RegisterUIDs();

                    switch (uiNodes[i])
                    {
                        case UISpeechNode:
                            conversation.SpeechNodes.Add((uiNodes[i] as UISpeechNode).SpeechNode);
                            break;
                        case UIOptionNode:
                            conversation.Options.Add((uiNodes[i] as UIOptionNode).OptionNode);
                            break;
                    }
                }

                // Serialize
                CurrentAsset.Serialize(conversation);

                // Null / clear everything. We aren't pointing to it anymore. 
                if (!manual)
                {
                    CurrentAsset = null;
                    while (uiNodes.Count != 0)
                    {
                        uiNodes.RemoveAt(0);
                    }

                    CurrentlySelectedObject = null;
                }

                MarkSceneDirty();
            }
        }

        public abstract class SelectableUI
        {
            public enum eType
            {
                Node,
                Connection
            }

            public abstract eType Type { get; }
        }

        public class SelectableUINode : SelectableUI
        {
            public SelectableUINode(UINode node)
            {
                Node = node;
            }

            public override eType Type
            {
                get { return eType.Node; }
            }

            public UINode Node { get; }
        }

        public class SelectableUIConnection : SelectableUI
        {
            public SelectableUIConnection(EditableConnection connection)
            {
                Connection = connection;
            }

            public override eType Type
            {
                get { return eType.Connection; }
            }

            public EditableConnection Connection { get; }
        }
    }
#endif
}