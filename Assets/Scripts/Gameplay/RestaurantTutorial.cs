using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantTutorial : MonoBehaviour
{
    bool hasCompletedRTutorial;
    public GameObject tutorialChihuahua;
    public GameObject exclamation;

    // Start is called before the first frame update
    void Start()
    {
        hasCompletedRTutorial = ProfileSystem.Get<bool>(ProfileSystem.Variable.RestaurantTutorialDone);
    }

    // Update is called once per frame
    void Update()
    {
        hasCompletedRTutorial = ProfileSystem.Get<bool>(ProfileSystem.Variable.RestaurantTutorialDone);

        if (!hasCompletedRTutorial)
        {
            tutorialChihuahua.SetActive(true);
        }

        if (hasCompletedRTutorial)
        {
            tutorialChihuahua.SetActive(false);
        }
    }

    public void RestaurantTutorialCompleted()
    {
        hasCompletedRTutorial = true;
    }

    public void StartTutorial()
    {
        exclamation.SetActive(true);
    }
}
