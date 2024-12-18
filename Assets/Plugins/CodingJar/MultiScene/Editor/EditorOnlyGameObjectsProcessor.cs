﻿#region

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

#endregion

/// <summary>
///     Post processor that destroys all of the objects tagged with "EditorOnly" during the build process.
///     This is supposed to be done by Unity but won't work if you instantiate (or in our case load) anything during the
///     build process.
/// </summary>
public static class EditorOnlyGameObjectsProcessor
{
    [PostProcessScene(-1)]
    static void OnPostProcessScene()
    {
        // We are not building, so don't delete this stuff (we're in the Editor!)
        if (!BuildPipeline.isBuildingPlayer)
        {
            return;
        }

        GameObject[] allGameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject gameObject in allGameObjects)
        {
            if (!gameObject.CompareTag("EditorOnly"))
            {
                continue;
            }

            if (gameObject && !EditorUtility.IsPersistent(gameObject))
            {
                Debug.LogFormat(gameObject.transform.parent, "Destroying left-over 'EditorOnly'-tagged GameObject: {0}",
                    gameObject.name);
                Object.DestroyImmediate(gameObject, false);
            }
        }
    }
}