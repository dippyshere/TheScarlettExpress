using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;
    [SerializeField] Animator loadingScreenAnimator;
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        StartCoroutine(HideLoadingScreenAfterLoad());
    }
    
    public void ShowLoadingScreen()
    {
        loadingScreenAnimator.SetBool("BeginTransition", true);
    }
    
    public void HideLoadingScreen()
    {
        loadingScreenAnimator.SetBool("EndTransition", true);
    }
    
    public void LoadScene(string sceneName, float delay = 0)
    {
        ShowLoadingScreen();
        StartCoroutine(LoadSceneAsync(sceneName, delay));
    }
    
    IEnumerator LoadSceneAsync(string sceneName, float delay = 0)
    {
        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.SetInputModeUI(false, false);
        }
        yield return null;
        yield return new WaitForSecondsRealtime(1.1435f);
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
    
    IEnumerator HideLoadingScreenAfterLoad()
    {
        yield return null;
        yield return null;
        yield return new WaitForEndOfFrame();
        HideLoadingScreen();
    }
}
