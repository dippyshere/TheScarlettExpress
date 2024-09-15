#region

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace DialogueEditor
{
    /// <summary>
    ///     This class holds all of the values for a node which
    ///     need to be serialized.
    /// </summary>
    [Serializable]
    public class NodeEventHolder : MonoBehaviour
    {
        [SerializeField] public AudioClip Audio;
        [SerializeField] public UnityEvent Event;
        [SerializeField] public Sprite Icon;

        [SerializeField] public int NodeID;
        [SerializeField] public TMP_FontAsset TMPFont;
    }
}