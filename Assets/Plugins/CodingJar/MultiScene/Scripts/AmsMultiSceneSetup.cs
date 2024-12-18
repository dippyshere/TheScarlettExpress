﻿#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using FormerlySerializedAs = UnityEngine.Serialization.FormerlySerializedAsAttribute;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

#endregion

namespace CodingJar.MultiScene
{
    [ExecuteInEditMode]
    public class AmsMultiSceneSetup : MonoBehaviour, ISerializationCallbackReceiver
    {
        [Serializable]
        public enum LoadMethod
        {
            Baked,
            Additive,
            AdditiveAsync,
            DontLoad
        }

        // How / When do we autogenerate / save the SceneSetup?
        public enum SceneSetupManagement
        {
            Automatic, // Saved whenever the this Scene is the Active Scene
            Manual, // Manually manage the Scene Setup (this is Frozen in place)
            Disabled // This scene never pulls in any SubScenes.
        }

        // Since we've now deprecated some things, we have to start versioning.
        const int CurrentVersion = 1;

        public static Action<AmsMultiSceneSetup> OnAwake;
        public static Action<AmsMultiSceneSetup> OnStart;
        public static Action<AmsMultiSceneSetup> OnDestroyed;

        [Tooltip("When do we save the SceneSetup? See the README.txt (or help above) for more information."),
         SerializeField]
		
        SceneSetupManagement _sceneSetupMode = SceneSetupManagement.Automatic;

        [SerializeField] List<SceneEntry> _sceneSetup = new();

        [Obsolete("This variable is deprecated as of AMS v0.91. Please view the README.txt for more information."),
         SerializeField, HideInInspector]
		
        bool _isMainScene = true;

        [SerializeField, HideInInspector] int _version;

        // This is required to be serialized due SceneManager.Scene being a temp scene during build process
        [Readonly, SerializeField] string _thisScenePath;
        readonly List<SceneEntry> _bakedScenesMerged = new(); // Already merged

#if UNITY_EDITOR
	    /// <summary>
	    ///     Easy accessor for the Editor
	    /// </summary>
	    public string scenePath
        {
            get { return _thisScenePath; }
        }
#endif

	    /// <summary>
	    ///     Awake can be used to tell anyone that a Scene has just been loaded.
	    ///     Due to a bug in PostProcessScene, this is the first thing to occur in a loaded scene.
	    /// </summary>
	    void Awake()
        {
            AmsDebug.Log(this, "{0}.Awake() (Scene {1}). IsLoaded: {2}. Frame: {3}", GetType().Name,
                gameObject.scene.name, gameObject.scene.isLoaded, Time.frameCount);

#if UNITY_EDITOR
            CheckVersion();

            if (!BuildPipeline.isBuildingPlayer)
            {
                _thisScenePath = gameObject.scene.path;
            }
#endif

            // Notify any listeners we're now awake
            if (OnAwake != null)
            {
                OnAwake(this);
            }

            if (!Application.isEditor || gameObject.scene.isLoaded || Time.frameCount > 1)
            {
                LoadSceneSetup();
            }
        }

	    /// <summary>
	    ///     This executes in the Editor when a behaviour is initially added to a GameObject.
	    /// </summary>
	    void Reset()
        {
            transform.SetAsFirstSibling();
        }

	    /// <summary>
	    ///     Start is executed just before the first Update (a frame after Awake/OnEnable).
	    ///     We execute the Scene loading here because Unity has issues loading scenes during the initial Awake/OnEnable calls.
	    /// </summary>
	    void Start()
        {
            AmsDebug.Log(this, "{0}.Start() Scene: {1}. Frame: {2}", GetType().Name, gameObject.scene.name,
                Time.frameCount);

            // Notify any listeners (like the cross-scene referencer)
            if (OnStart != null)
            {
                OnStart(this);
            }

            // Second chance at loading scenes
            LoadSceneSetup();
        }

        void OnDestroy()
        {
            if (OnDestroyed != null)
            {
                OnDestroyed(this);
            }
        }

        // Intentionally left blank
        public void OnAfterDeserialize()
        {
        }

        /// <summary>
        ///     OnBeforeSerialize is called whenever we're about to save or inspect this Component.
        ///     We want to match exactly what the Editor has in terms of Scene setup, so we do it here.
        /// </summary>
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (!this || !gameObject || BuildPipeline.isBuildingPlayer || Application.isPlaying)
            {
                return;
            }

            // Save off the scene path
            if (gameObject.scene.IsValid())
            {
                _thisScenePath = gameObject.scene.path;
            }
#endif
        }

        /// <summary>
        ///     Read-only access to the Scene Setup.
        /// </summary>
#if UNITY_METRO && !UNITY_UWP
        public IList<SceneEntry> GetSceneSetup()
        {
	        return _sceneSetup;
        }
#else
        public ReadOnlyCollection<SceneEntry> GetSceneSetup()
        {
            return _sceneSetup.AsReadOnly();
        }
#endif

	    /// <summary>
	    ///     Check our version and upgrade it if necessary
	    /// </summary>
	    void CheckVersion()
        {
            if (_version >= CurrentVersion)
            {
                return;
            }

#pragma warning disable 618
            // The isMainScene variable was used to distinguish scenes that should load other scenes
            // Instead, we now assume the Scene Setup is always correct (but we should clear it on upgrade).
            if (!_isMainScene)
            {
                _sceneSetup.Clear();
            }

            _isMainScene = false;
            _version = CurrentVersion;
#pragma warning restore 618
        }

        void LoadSceneSetup()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                LoadSceneSetupInEditor();
            }
            else
            {
                LoadSceneSetupAtRuntime();
            }
#else
	        LoadSceneSetupAtRuntime();
#endif
        }

        /// <summary>
        ///     Load Scene Setup at Runtime.
        /// </summary>
        void LoadSceneSetupAtRuntime()
        {
            List<SceneEntry> sceneSetup = new(_sceneSetup);
            foreach (SceneEntry entry in sceneSetup)
            {
                LoadEntryAtRuntime(entry);
            }
        }

        /// <summary>
        ///     Load a particular Scene Entry
        /// </summary>
        /// <param name="entry">The Entry to load</param>
        void LoadEntryAtRuntime(SceneEntry entry)
        {
            // Don't load 
            if (entry.loadMethod == LoadMethod.DontLoad)
            {
                return;
            }

            // Already loaded, try editor first
            Scene existingScene = SceneManager.GetSceneByPath(entry.scene.editorPath);

            // Try runtime path
            if (!existingScene.IsValid())
            {
                existingScene = SceneManager.GetSceneByPath(entry.scene.runtimePath);
            }

#if UNITY_EDITOR
            // Could be we just created the scene because it's baked
            if (!existingScene.IsValid())
            {
                existingScene = SceneManager.GetSceneByName(entry.scene.runtimePath);
            }

            if (Application.isEditor && entry.loadMethod == LoadMethod.Baked)
            {
                // If we've already processed this, return early
                if (_bakedScenesLoading.Contains(entry) || _bakedScenesMerged.Contains(entry))
                {
                    return;
                }

                // We're loading this entry, don't allow this to be re-entrant
                _bakedScenesLoading.Add(entry);

                if (!existingScene.IsValid())
                {
                    // This allows us to load the level even in playmode
#if UNITY_2018_3_OR_NEWER
                    EditorSceneManager.LoadSceneInPlayMode(entry.scene.editorPath,
                        new LoadSceneParameters(LoadSceneMode.Additive));
#else
	                EditorApplication.LoadLevelAdditiveInPlayMode(entry.scene.editorPath);
#endif
                }

                // Loading a scene can take multiple frames so we have to wait.
                // Baking scenes can only take place when they're all loaded due to cross-scene referencing
                if (_waitingToBake != null)
                {
                    StopCoroutine(_waitingToBake);
                }

                _waitingToBake = StartCoroutine(CoWaitAndBake());
                return;
            }
#endif

            // If it's already loaded, return early
            if (existingScene.IsValid())
            {
                return;
            }

            switch (entry.loadMethod)
            {
                case LoadMethod.AdditiveAsync:
                    AmsDebug.Log(this, "Loading {0} Asynchronously from {1}", entry.scene.name, gameObject.scene.name);
                    entry.asyncOp = SceneManager.LoadSceneAsync(entry.scene.runtimePath, LoadSceneMode.Additive);
                    return;
                case LoadMethod.Additive:
                    AmsDebug.Log(this, "Loading {0} from {1}", entry.scene.name, gameObject.scene.name);
                    SceneManager.LoadScene(entry.scene.runtimePath, LoadSceneMode.Additive);
                    break;
            }
        }

        [Serializable]
        public class SceneEntry
        {
            // The scene that is automatically processed both in Editor and Runtime
            [BeginReadonly] public AmsSceneReference scene;

            [Tooltip("Should this be automatically loaded in the Editor?"), FormerlySerializedAs("isLoaded")]
            public bool loadInEditor;

            [EndReadonly, Tooltip("How should we load this scene at Runtime?")]
            public LoadMethod loadMethod;

#if UNITY_EDITOR
	        /// <summary>
	        ///     Construct from a Unity SceneSetup
	        /// </summary>
	        public SceneEntry(SceneSetup sceneSetup)
            {
                scene = new AmsSceneReference(sceneSetup.path);

                loadInEditor = sceneSetup.isLoaded;
                loadMethod = LoadMethod.Additive;
            }
#endif

            public AsyncOperation asyncOp { get; set; }

            public override string ToString()
            {
                return string.Format("{0} loadInEditor: {1} loadMethod: {2}", scene.name, loadInEditor, loadMethod);
            }

            /// <summary>
            ///     Overridden Equals to we can compare entries.  Entries with the same scene references and load settings are
            ///     considered equal.
            /// </summary>
            public override bool Equals(object obj)
            {
                if (this == obj)
                {
                    return true;
                }

                SceneEntry other = obj as SceneEntry;
                if (other == null)
                {
                    return false;
                }

                return scene.Equals(other.scene) && loadInEditor == other.loadInEditor &&
                       loadMethod == other.loadMethod && asyncOp == other.asyncOp;
            }

            public override int GetHashCode()
            {
                return scene.GetHashCode() * 4 + loadInEditor.GetHashCode() * 2 + loadMethod.GetHashCode();
            }
        }

#if UNITY_EDITOR
        public SceneSetupManagement sceneSetupMode
        {
            get { return _sceneSetupMode; }
            set { _sceneSetupMode = value; }
        }

        /// <summary> The co-routine that runs in play-in-editor mode that ensures our scenes are baked correctly </summary>
        Coroutine _waitingToBake;

        readonly List<SceneEntry> _bakedScenesLoading = new(); // Currently loading or loaded
#endif

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void OnEditorInit()
        {
            EditorSceneManager.sceneSaving -= OnSceneSaving;
            EditorSceneManager.sceneSaving += OnSceneSaving;
        }

        public static void OnSceneSaving(Scene scene, string path)
        {
            if (!scene.IsValid() || !scene.isLoaded)
            {
                return;
            }

            AmsMultiSceneSetup instance = scene.GetSceneSingleton<AmsMultiSceneSetup>(true);
            if (!instance)
            {
                return;
            }

            instance.OnBeforeSerialize();

            bool isSceneSetupManual = instance._sceneSetupMode == SceneSetupManagement.Manual;
            if (isSceneSetupManual)
            {
                return;
            }

            // Clear if we're disabled... by implicitly never setting any values
            List<SceneEntry> newSceneSetup = new();
            bool bForceDirty = false;

            // We only update the scene setup if we're the active scene
            Scene activeScene = SceneManager.GetActiveScene();
            bool isSceneSetupAuto = instance._sceneSetupMode == SceneSetupManagement.Automatic && activeScene == scene;
            if (isSceneSetupAuto)
            {
                // Update our scene setup
                SceneSetup[] editorSceneSetup = EditorSceneManager.GetSceneManagerSetup();
                for (int i = 0; i < editorSceneSetup.Length; ++i)
                {
                    // If we're the active scene, don't save it.
                    SceneSetup editorEntry = editorSceneSetup[i];
                    if (editorEntry.path == activeScene.path)
                    {
                        continue;
                    }

                    SceneEntry newEntry = new(editorEntry);
                    newSceneSetup.Add(newEntry);

                    // Save the baked settings
                    SceneEntry oldEntry = instance._sceneSetup.Find(x => newEntry.scene.Equals(x.scene));
                    if (oldEntry != null)
                    {
                        newEntry.loadMethod = oldEntry.loadMethod;

                        // We need to update the path if the runtime paths aren't the same (implies a rename)
                        bForceDirty = bForceDirty || newEntry.scene.runtimePath != oldEntry.scene.runtimePath;
                    }
                }
            }

            // If we had a new scene setup...
            if (bForceDirty || !newSceneSetup.SequenceEqual(instance._sceneSetup))
            {
                instance._sceneSetup = newSceneSetup;
                EditorUtility.SetDirty(instance);
                EditorSceneManager.MarkSceneDirty(scene);

                AmsDebug.Log(instance,
                    "SceneSetup for {0} has been updated. If this is unexpected, click here to double-check the entries!",
                    activeScene.name);
            }
        }

        /// <summary>
        ///     Loads the scene setup in the Editor
        /// </summary>
        void LoadSceneSetupInEditor()
        {
            foreach (SceneEntry entry in _sceneSetup)
            {
                LoadEntryInEditor(entry);
            }
        }

        /// <summary>
        ///     Loads a particular Scene Entry in the Editor
        /// </summary>
        /// <param name="entry">The entry to load</param>
        void LoadEntryInEditor(SceneEntry entry)
        {
            // Bad time to do this.
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                return;
            }

            // We can't do this
            if (string.IsNullOrEmpty(entry.scene.editorPath) || entry.scene.editorPath == gameObject.scene.path)
            {
                return;
            }

            bool bShouldLoad = entry.loadInEditor && AmsPreferences.AllowAutoload;
            Scene scene = entry.scene.scene;

            try
            {
                if (!scene.IsValid())
                {
                    if (bShouldLoad)
                    {
                        AmsDebug.Log(this, "Scene {0} is loading Scene {1} in Editor", gameObject.scene.name,
                            entry.scene.name);
                        EditorSceneManager.OpenScene(entry.scene.editorPath, OpenSceneMode.Additive);
                    }
                    else
                    {
                        AmsDebug.Log(this, "Scene {0} is opening Scene {1} (without loading) in Editor",
                            gameObject.scene.name, entry.scene.name);
                        EditorSceneManager.OpenScene(entry.scene.editorPath, OpenSceneMode.AdditiveWithoutLoading);
                    }
                }
                else if (bShouldLoad != scene.isLoaded)
                {
                    if (bShouldLoad && !scene.isLoaded)
                    {
                        AmsDebug.Log(this, "Scene {0} is loading existing Scene {1} in Editor", gameObject.scene.name,
                            entry.scene.name);
                        EditorSceneManager.OpenScene(entry.scene.editorPath, OpenSceneMode.Additive);
                    }
                    else
                    {
                        AmsDebug.Log(this, "Scene {0} is closing Scene {1} in Editor", gameObject.scene.name,
                            entry.scene.name);
                        EditorSceneManager.CloseScene(scene, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, this);
            }
        }

        /// <summary>
        ///     Scene loads take a frame to complete, so we wait until all of the baked scenes are loaded, then we merge them.
        /// </summary>
        IEnumerator CoWaitAndBake()
        {
            // Ensure ALL of the Baked Scenes are loaded
            bool bAllLoaded = false;
            while (!bAllLoaded)
            {
                bAllLoaded = true;
                foreach (SceneEntry entry in _sceneSetup)
                {
                    // For now, we wait until EVERYTHING is loaded so the cross-scene references are correct.
                    bAllLoaded = bAllLoaded && (entry.loadMethod == LoadMethod.DontLoad || entry.scene.isLoaded ||
                                                _bakedScenesMerged.Contains(entry));
                }

                // We're not all loaded, wait another frame.
                if (!bAllLoaded)
                {
                    yield return null;
                }
            }

            // Give us the ability to fix-up the cross-scene references.
            foreach (SceneEntry entry in _sceneSetup)
            {
                // If it's Invalid, it's already baked.
                if (CanMerge(entry))
                {
                    PreMerge(entry);
                }
            }

            // Now merge the scenes
            foreach (SceneEntry entry in _sceneSetup)
            {
                if (CanMerge(entry))
                {
                    MergeScene(entry);
                    _bakedScenesMerged.Add(entry);
                }
            }
        }

        /// <summary>
        ///     Are we ready to merge in this scene entry?
        /// </summary>
        /// <param name="entry">The entry to merge</param>
        /// <returns>True iff it can be merged</returns>
        bool CanMerge(SceneEntry entry)
        {
            if (entry.loadMethod != LoadMethod.Baked)
            {
                return false;
            }

            Scene scene = entry.scene.scene;
            if (!scene.IsValid())
            {
                return false;
            }

            Scene activeScene = SceneManager.GetActiveScene();
            if (scene == activeScene || scene == gameObject.scene)
            {
                return false;
            }

            if (!gameObject.scene.isLoaded)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     'Bakes' a scene by merging it into our scene.
        /// </summary>
        /// <param name="entry">The entry to bake</param>
        void PreMerge(SceneEntry entry)
        {
            Scene scene = entry.scene.scene;

            // This is a last chance before that scene gets destroyed.
            AmsCrossSceneReferences crossSceneRefs = AmsCrossSceneReferences.GetSceneSingleton(scene, false);
            if (crossSceneRefs)
            {
                crossSceneRefs.ResolvePendingCrossSceneReferences();
            }
        }

        void MergeScene(SceneEntry entry)
        {
            Scene scene = entry.scene.scene;

            // Make sure there is only ever one AmsMultiSceneSetup in a given scene
            AmsMultiSceneSetup sourceSetup = scene.GetSceneSingleton<AmsMultiSceneSetup>(false);
            if (sourceSetup)
            {
                Destroy(sourceSetup.gameObject);
            }

            AmsDebug.Log(this, "Merging {0} into {1}", scene.path, gameObject.scene.path);
            SceneManager.MergeScenes(scene, gameObject.scene);
        }
#endif
    }
}