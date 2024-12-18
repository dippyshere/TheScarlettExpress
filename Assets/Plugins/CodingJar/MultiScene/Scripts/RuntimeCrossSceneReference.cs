﻿#region

using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

#endregion

namespace CodingJar.MultiScene
{
    public class ResolveException : Exception
    {
        public ResolveException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public struct GenericData
    {
        public Object @object;
        public string @string;

        public GenericData(Object obj)
        {
            @object = obj;
            @string = null;
        }

        public GenericData(string str)
        {
            @object = null;
            @string = str;
        }

        public static implicit operator GenericData(Object obj)
        {
            return new GenericData(obj);
        }

        public static implicit operator GenericData(string str)
        {
            return new GenericData(str);
        }
    }

    [Serializable]
    public struct RuntimeCrossSceneReference
    {
        // From which UniqueObject.PropertyPath?
        [SerializeField] Object _sourceObject;

        [FormerlySerializedAs("_fromField"), SerializeField]
		
        string _sourceField;

        [FormerlySerializedAs("_fromObject"), SerializeField, HideInInspector]
		
        UniqueObject DEPRECATED_fromObject;

        // Which UniqueObject are we referencing?
        [SerializeField] UniqueObject _toObject;

        // New ability to save any data with this cross-scene reference for generic resolving
        [Serializable]
        struct GenericDataBundle
        {
            public List<GenericData> data;
        }

        [SerializeField] GenericDataBundle _data;

        public RuntimeCrossSceneReference(Object from, string fromField, UniqueObject to, List<GenericData> data)
        {
            DEPRECATED_fromObject = new UniqueObject();

            _sourceObject = from;
            _sourceField = fromField;
            _toObject = to;
            _data = new GenericDataBundle { data = data };

            _toObjectCached = null;
        }

        public Object fromObject
        {
            get
            {
                // Update this object from old data
                if (!_sourceObject && DEPRECATED_fromObject.scene.IsValid())
                {
                    _sourceObject = DEPRECATED_fromObject.Resolve();

#if UNITY_EDITOR
                    if (!_sourceObject && !Application.isPlaying)
                    {
                        _sourceObject = DEPRECATED_fromObject.EditorResolveSlow();
                    }
#endif

                    // If we had a successful resolve, we're done...
                    if (_sourceObject)
                    {
                        DEPRECATED_fromObject = new UniqueObject();
                    }
                }

                return _sourceObject;
            }
        }

        Object _toObjectCached;

        public Object toObject
        {
            get
            {
                if (!_toObjectCached)
                {
                    _toObjectCached = _toObject.Resolve();

#if UNITY_EDITOR
                    // Editor fallback
                    if (!_toObjectCached)
                    {
                        _toObjectCached = _toObject.EditorResolveSlow();
                        if (_toObjectCached)
                        {
                            EditorUtility.SetDirty(_sourceObject);

                            GameObject gameObject = _sourceObject.EditorGetGameObjectFromComponent();
                            Scene scene = gameObject ? gameObject.scene : default;
                            if (scene.isLoaded && !scene.isDirty)
                            {
                                EditorSceneManager.MarkSceneDirty(scene);
                                EditorApplication.delayCall += () =>
                                {
                                    AmsDebug.LogWarning(gameObject,
                                        "Scene {0} needs to be resaved due to AMS entries being moved (see warnings above)",
                                        scene.name);
                                };
                            }
                        }
                    }
#endif
                }

                return _toObjectCached;
            }
        }

        public AmsSceneReference DEPRECATED_fromScene
        {
            set
            {
                if (!_sourceObject && DEPRECATED_fromObject.scene.IsValid())
                {
                    DEPRECATED_fromObject.scene = value;
                }
            }
        }

        public string sourceField
        {
            get { return _sourceField; }
        }

        public List<GenericData> data
        {
            get { return _data.data; }
        }

        public AmsSceneReference toScene
        {
            get { return _toObject.scene; }
            set { _toObject.scene = value; }
        }

        public override string ToString()
        {
            return string.Format("{0}.{1} => {2}", _sourceObject ? _sourceObject.ToString() : "(null)", _sourceField,
                _toObject);
        }

        public bool IsSameSource(RuntimeCrossSceneReference other)
        {
            try
            {
                return fromObject == other.fromObject && _sourceField == other._sourceField;
            }
            catch (Exception ex)
            {
                AmsDebug.Log(null, "IsSameSource: Could not compare: {0} and {1}: {2}", ToString(), other, ex);
            }

            return false;
        }
    } // struct 
} // namespace 