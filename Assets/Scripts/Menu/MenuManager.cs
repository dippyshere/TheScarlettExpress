#region

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    Canvas _titleCanvas;
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        _titleCanvas = GetComponent<Canvas>();
    }

    public void OnStartButtonClicked()
    {
        _titleCanvas.enabled = false;
        ProfileSystem.CurrentSaveSlot = 4;
        ProfileSystem.ClearProfile(4);
        LoadingManager.Instance.LoadScene(ProfileSystem.Get<string>(ProfileSystem.Variable.LastScene));
    }
    
    public void OnPlayButtonClicked()
    {
        SaveSlotsManager.Instance.ShowSaveSlots();
        _titleCanvas.enabled = false;
    }
    
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
    
    public void ShowTitle()
    {
        _titleCanvas.enabled = true;
    }
}