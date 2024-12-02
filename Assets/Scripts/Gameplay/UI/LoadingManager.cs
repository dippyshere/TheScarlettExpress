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
    
    public void LoadScene(string sceneName)
    {
        ShowLoadingScreen();
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    
    IEnumerator LoadSceneAsync(string sceneName)
    {
        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.SetInputModeUI(false, false);
        }
        yield return null;
        yield return new WaitForSecondsRealtime(1.035f);
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
