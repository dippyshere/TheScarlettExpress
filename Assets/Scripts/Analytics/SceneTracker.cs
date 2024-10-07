#region

using System.Collections.Generic;
using UnityEngine;

#endregion

public class SceneTracker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("scene_change",
                new Dictionary<string, object> { { "sceneName", gameObject.scene.name } });
        }
    }
}