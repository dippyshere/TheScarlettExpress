#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using View = UnityEngine.ScriptableObject;
using ContainerWindow = UnityEngine.ScriptableObject;

#endregion

namespace FullscreenEditor
{
    public class FullscreenWindow : FullscreenContainer
    {
        [SerializeField] RectOffset m_rectOffset;
        [SerializeField] RectOffset m_toolbarOffset;
        [SerializeField] bool m_createdByFullscreenOnPlay;

        public RectOffset ClipOffset
        {
            get { return m_rectOffset; }
            set
            {
                if (m_dst.View)
                {
                    m_rectOffset = value;
                    m_dst.View.InvokeMethod("SetPosition", value.Add(new Rect(Vector2.zero, Rect.size)));
                }
            }
        }

        internal bool CreatedByFullscreenOnPlay
        {
            get { return m_createdByFullscreenOnPlay; }
            set { m_createdByFullscreenOnPlay = value; }
        }

        public bool HasToolbarOffset
        {
            get { return ToolbarOffset != null; }
        }

        public virtual RectOffset ToolbarOffset
        {
            get
            {
                if (m_toolbarOffset == null)
                {
                    m_toolbarOffset = new RectOffset(0, 0, (int)FullscreenUtility.GetToolbarHeight(), 0);
                }

                return m_toolbarOffset;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            FullscreenPreferences.ToolbarVisible.OnValueSaved += SetToolbarStatus;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            FullscreenPreferences.ToolbarVisible.OnValueSaved -= SetToolbarStatus;
        }

        void SwapWindows(EditorWindow a, EditorWindow b)
        {
            View parentA = a.GetFieldValue<View>("m_Parent");
            View parentB = b.GetFieldValue<View>("m_Parent");

            ContainerWindow containerA = parentA.GetPropertyValue<ContainerWindow>("window");
            ContainerWindow containerB = parentB.GetPropertyValue<ContainerWindow>("window");

            EditorWindow selectedPaneA = parentA.GetPropertyValue<EditorWindow>("actualView");
            EditorWindow selectedPaneB = parentB.GetPropertyValue<EditorWindow>("actualView");

            SetFreezeContainer(containerA, true);
            SetFreezeContainer(containerB, true);

            Logger.Debug("Swapping windows {0} and {1} @ {2} and {3}", a, b, parentA, parentB);

            parentA.SetPropertyValue("actualView", b);
            parentB.SetPropertyValue("actualView", a);

            ReplaceDockAreaPane(parentA, a, b);
            ReplaceDockAreaPane(parentB, b, a);

            a.InvokeMethod("MakeParentsSettingsMatchMe");
            b.InvokeMethod("MakeParentsSettingsMatchMe");

            if (selectedPaneA != a)
            {
                parentA.SetPropertyValue("actualView", selectedPaneA);
            }

            if (selectedPaneB != b)
            {
                parentB.SetPropertyValue("actualView", selectedPaneB);
            }

            SetFreezeContainer(containerA, false);
            SetFreezeContainer(containerB, false);
        }

        protected void ReplaceDockAreaPane(View dockArea, EditorWindow originalPane, EditorWindow newPane)
        {
            if (dockArea.HasField("m_Panes"))
            {
                List<EditorWindow> dockedPanes = dockArea.GetFieldValue<List<EditorWindow>>("m_Panes");
                int dockIndex = dockedPanes.IndexOf(originalPane);
                dockedPanes[dockIndex] = newPane;
            }
        }

        public void SetToolbarStatus(bool toolbarVisible)
        {
            if (!HasToolbarOffset)
            {
                return;
            }

            if (FullscreenPreferences.UseGlobalToolbarHiding)
            {
                return;
            }

            ClipOffset = toolbarVisible ? new RectOffset() : ToolbarOffset;
        }

        public override void Focus()
        {
            EditorWindow window = ActualViewPyramid.Window;

            if (window)
            {
                window.Focus();
            }
            else
            {
                base.Focus();
            }
        }

        public override bool IsFocused()
        {
            return EditorWindow.focusedWindow && EditorWindow.focusedWindow == ActualViewPyramid.Window;
        }

        protected override void AfterOpening()
        {
            base.AfterOpening();

            Focus();

            if (m_src.Window)
            {
                m_dst.Window.titleContent =
                    m_src.Window.titleContent; // Copy the title of the window to the placeholder
            }

            SetToolbarStatus(FullscreenPreferences.ToolbarVisible); // Hide/show the toolbar
            // macOS doesn't like fast things, so we'll wait a bit and do it again
            // Looks like Linux does not like it too
            After.Milliseconds(100d, () => SetToolbarStatus(FullscreenPreferences.ToolbarVisible));

            EditorWindow notificationWindow = ActualViewPyramid.Window;

            After.Milliseconds(50d, () =>
            {
                if (!notificationWindow) // Might have been closed
                {
                    return;
                }

                string menuItemPath = string.Empty;
                if (notificationWindow.IsOfType(Types.GameView))
                {
                    menuItemPath = Fullscreen
                        .GetAllFullscreen()
                        .Where(fs =>
                            fs.ActualViewPyramid.Window && fs.ActualViewPyramid.Window.IsOfType(Types.GameView))
                        .Count() > 1
                        ? Shortcut.MOSAIC_PATH
                        : Shortcut.GAME_VIEW_PATH;
                }
                else if (notificationWindow is SceneView)
                {
                    menuItemPath = Shortcut.SCENE_VIEW_PATH;
                }
                else
                {
                    menuItemPath = Shortcut.CURRENT_VIEW_PATH;
                }

                FullscreenUtility.ShowFullscreenExitNotification(notificationWindow, menuItemPath);
            });
        }

        internal void OpenWindow<T>(Rect rect, T window = null) where T : EditorWindow
        {
            OpenWindow(rect, typeof(T), window);
        }

        internal void OpenWindow(Rect rect, Type type, EditorWindow window = null, bool disposableWindow = false)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsOfType(typeof(EditorWindow)))
            {
                throw new ArgumentException("Type must be inherited from UnityEditor.EditorWindow", "type");
            }

            if (window is PlaceholderWindow)
            {
                FullscreenUtility.ShowFullscreenNotification(window,
                    "Wanna fullscreen the placeholder?\nSorry, not possible");
                Logger.Debug("Tried to fullscreen a placeholder window");
                return;
            }

            if (Fullscreen.GetFullscreenFromView(window))
            {
                FullscreenUtility.ShowFullscreenNotification(window,
                    "You can't fullscreen a window already in fullscreen");
                Logger.Debug("Tried to fullscreen a view already in fullscreen");
                return;
            }

            BeforeOpening();

            if (window)
            {
                m_src = new ViewPyramid(window);
            }

            EditorWindow
                childWindow =
                    window
                        ? CreateInstance<PlaceholderWindow>()
                        : (EditorWindow)CreateInstance(type); // Instantiate a new window for this fullscreen

            m_dst = CreateFullscreenViewPyramid(rect, childWindow);

            if (window) // We can't swap the src window if we didn't create a placeholder window
            {
                SwapWindows(m_src.Window, m_dst.Window);
            }

            Rect = rect;

            if (disposableWindow && childWindow is PlaceholderWindow)
            {
                childWindow.Close(); // Close the pyramid we created because disposable views are not restore later
                m_dst.Window = m_src.Window;
            }

            AfterOpening();
        }

        internal bool IsPlaceholderVisible()
        {
            if (!(m_dst.Window is PlaceholderWindow))
            {
                return false;
            }

            ViewPyramid pyramid = new(m_dst.Window);

            if (!pyramid.View || !pyramid.View.IsOfType(Types.HostView))
            {
                return false;
            }

            View actualView = pyramid.View.GetPropertyValue<View>("actualView");

            return actualView == m_dst.Window;
        }

        public override void Close()
        {
            bool shouldRefocus = IsFocused() && IsPlaceholderVisible();

            if (m_src.Window && m_dst.Window)
            {
                SwapWindows(m_src.Window, m_dst.Window); // Swap back the source window
            }

            base.Close();

            if (shouldRefocus && m_src.Window)
            {
                m_src.Window.Focus();
            }
        }
    }
}