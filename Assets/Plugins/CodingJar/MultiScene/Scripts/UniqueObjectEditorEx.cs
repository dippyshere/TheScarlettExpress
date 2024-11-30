#region

using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
#endif

#endregion

namespace CodingJar.MultiScene
{
	/// <summary>
	///     Editor extensions for UniqueObject to make manipulations quicker (and stronger) when using the Editor.
	/// </summary>
	public partial struct UniqueObject
    {
#if UNITY_EDITOR
        int GetEditorId(Object obj)
        {
            int editorId = 0;

            PropertyInfo inspectorModeInfo =
                typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
            SerializedObject sObj = new(obj);
            inspectorModeInfo.SetValue(sObj, InspectorMode.Debug, null);

            SerializedProperty sProp = sObj.FindProperty("m_LocalIdentfierInFile");
            if (sProp != null)
            {
                editorId = sProp.intValue;
                sProp.Dispose();
            }

            sObj.Dispose();

            return editorId;
        }

        internal Object EditorResolveSlow()
        {
            Scene scene = this.scene.scene;

            if (!scene.IsValid())
            {
                return null;
            }

            if (!scene.isLoaded)
            {
                return null;
            }

            // Find the object (this is potentially very slow).
            Object[] allObjs = EditorUtility.CollectDeepHierarchy(scene.GetRootGameObjects());
            foreach (Object obj in allObjs)
            {
                // Apparently this happens... bummer
                if (!obj)
                {
                    continue;
                }

#if UNITY_2018_3_OR_NEWER
                // If it's a prefab, it's a lot more difficult since Unity doesn't store IDs
                Object prefabObj = PrefabUtility.GetPrefabInstanceHandle(obj);
                if (prefabObj)
                {
                    // We only care about the root of a prefab
                    GameObject gameObject = obj as GameObject;
                    if (!gameObject)
                    {
                        continue;
                    }

                    // We can save a lot of time by making sure we only check the prefab roots
                    if (!PrefabUtility.IsAnyPrefabInstanceRoot(gameObject))
                    {
                        continue;
                    }

                    // Make sure we're actually trying to find this prefab...
                    if (editorLocalId != GetEditorId(prefabObj))
                    {
                        continue;
                    }
#else
	            // If it's a prefab, it's a lot more difficult since Unity doesn't store IDs
	            var prefabObj = PrefabUtility.GetPrefabObject(obj);
	            if (prefabObj && editorLocalId == GetEditorId(prefabObj))
	            {
		            GameObject gameObject = GameObjectEx.EditorGetGameObjectFromComponent(obj);
		            if (!gameObject)
		            {
			            Debug.LogWarningFormat(obj, "Could not SLOW resolve {0} (cross-scene reference is broken). Pointing it to {1}.", this, obj);
			            return obj;
		            }

		            // Make sure we do this from the root
		            var prefabRoot = PrefabUtility.FindPrefabRoot(gameObject);
		            if (prefabRoot)
			            gameObject = prefabRoot;
#endif

                    // If we have a relative path, grab that GameObject
                    if (!string.IsNullOrEmpty(editorPrefabRelativePath))
                    {
                        Transform transform = gameObject.transform.Find(editorPrefabRelativePath);
                        if (transform)
                        {
                            gameObject = transform.gameObject;
                        }
                        else
                        {
                            Debug.LogWarningFormat(gameObject,
                                "Tried to perform a slow resolve for {0} and found prefab {1}, but could not resolve the expected sub-path {2} which indicates the Prefab instance path was renamed.",
                                this, gameObject, editorPrefabRelativePath);
                        }
                    }

                    Debug.LogWarningFormat(gameObject,
                        "Performed a slow resolve on {0} due to a rename.  We found a PREFAB with same ID named {1} (but we're not sure). Attempting a resolve with it.",
                        this, gameObject);
                    fullPath = gameObject.GetFullName();
                    return RuntimeResolve();
                }

                if (editorLocalId == GetEditorId(obj))
                {
                    GameObject gameObject = obj.EditorGetGameObjectFromComponent();
                    Debug.LogWarningFormat(obj,
                        "Performed a slow resolve on {0} and found {1} ({2}). Double-check this is correct.", this,
                        gameObject ? gameObject.GetFullName() : "(Non-Existant GameObject)", obj.GetType());
                    return obj;
                }
            }

            return null;
        }

        public UniqueObject(Object obj)
        {
            scene = new AmsSceneReference();
            fullPath = string.Empty;
            componentName = string.Empty;
            version = CurrentSerializedVersion;
            componentIndex = 0;
            editorLocalId = 0;
            editorPrefabRelativePath = string.Empty;

            if (!obj)
            {
                return;
            }

            GameObject gameObject = obj.EditorGetGameObjectFromComponent();
            if (gameObject)
            {
                scene = new AmsSceneReference(gameObject.scene);
                fullPath = gameObject.GetFullName();

                Component comp = obj as Component;
                if (comp)
                {
                    componentName = obj.GetType().AssemblyQualifiedName;
                    gameObject.GetComponents(obj.GetType(), _reusableComponentsList);
                    componentIndex = _reusableComponentsList.IndexOf(comp);
                }
            }

#if UNITY_2018_3_OR_NEWER
            // Get the nearest root (which will have an editor id)
            Object prefabObj = PrefabUtility.GetPrefabInstanceHandle(obj);
            if (prefabObj)
            {
                editorLocalId = GetEditorId(prefabObj);

                GameObject prefabRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(obj);
                editorPrefabRelativePath = gameObject.transform.GetPathRelativeTo(prefabRoot.transform);
            }
#else
	        // Get the prefab object
	        var prefabObj = PrefabUtility.GetPrefabObject(obj);
	        if (prefabObj)
	        {
		        GameObject prefabRoot = PrefabUtility.FindPrefabRoot(gameObject);
		        editorLocalId = prefabRoot ? GetEditorId(prefabRoot) : GetEditorId(obj);
		        editorPrefabRelativePath = TransformEx.GetPathRelativeTo(gameObject.transform, prefabRoot.transform);
	        }
#endif

            if (editorLocalId == 0)
            {
                editorLocalId = GetEditorId(obj);
            }
        }
#endif // UNITY_EDITOR
    } // struct
} // namespace 