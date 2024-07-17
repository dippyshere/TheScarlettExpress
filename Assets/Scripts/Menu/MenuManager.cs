using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        // clear profile for playtesting
        ProfileSystem.ClearProfile();
        SceneManager.LoadSceneAsync("PlayerTesting");
    }
}
