#region

using System;
using UnityEngine;
using View = UnityEngine.ScriptableObject;
using ContainerWindow = UnityEngine.ScriptableObject;

#endregion

namespace FullscreenEditor
{
    public class FullscreenView : FullscreenContainer
    {
        protected void SwapViews(View a, View b)
        {
            ContainerWindow containerA = a.GetPropertyValue<ContainerWindow>("window");
            ContainerWindow containerB = b.GetPropertyValue<ContainerWindow>("window");

            SetFreezeContainer(containerA, true);
            SetFreezeContainer(containerB, true);

            Logger.Debug("Swapping views {0} and {1} @ {2} and {3}", a, b, containerA, containerB);

            containerA.SetPropertyValue("rootView", b);
            containerB.SetPropertyValue("rootView", a);

            SetFreezeContainer(containerA, true);
            SetFreezeContainer(containerB, true);
        }

        internal void OpenView(Rect rect, ScriptableObject view)
        {
            if (!view)
            {
                throw new ArgumentNullException("view");
            }

            view.EnsureOfType(Types.View);

            if (FullscreenUtility.IsLinux)
            {
                throw new PlatformNotSupportedException("Linux does not support fullscreen from View class");
            }

            if (Fullscreen.GetFullscreenFromView(view))
            {
                Logger.Debug("Tried to fullscreen a view already in fullscreen");
                return;
            }

            BeforeOpening();

            PlaceholderWindow placeholder = CreateInstance<PlaceholderWindow>();

            m_src = new ViewPyramid(view);
            m_dst = CreateFullscreenViewPyramid(rect, placeholder);

            SwapViews(m_src.View, m_dst.View);
            Rect = rect;

            AfterOpening();
        }

        public override void Close()
        {
            if (m_src.View && m_dst.View)
            {
                SwapViews(m_src.View, m_dst.View); // Swap back the source view
            }

            base.Close();
        }
    }
}