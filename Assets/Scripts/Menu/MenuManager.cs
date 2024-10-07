#region

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

public class MenuManager : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        // clear profile for playtesting
        ProfileSystem.ClearProfile();
        SceneManager.LoadSceneAsync("_Onboarding");
    }
}