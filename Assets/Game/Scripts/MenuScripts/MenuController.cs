using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _menuButtons;
    [SerializeField] private CameraMovement _cameraMovement;

    private int _modeIndex = 0;


    private void Start()
    {
        _menuButtons.SetActive(false);
        _cameraMovement.StartMovement();
        _cameraMovement.isAnimationEnd += StartUpAnimationEnd;
    }

    private void OnDestroy()
    {
        _cameraMovement.isAnimationEnd -= StartUpAnimationEnd;
    }

    private void StartUpAnimationEnd(bool isEnd,string eventName) 
    {
        if (isEnd && eventName == "Start")
        {
            _menuButtons.SetActive(true);
        }
        else if (isEnd && eventName == "Leave") 
        {
            SceneManager.LoadScene(_modeIndex);
        }
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

    public void PlayInSomeMode(int ModeID)
    {
        _modeIndex = ModeID;
        _cameraMovement.EndMovement();
    }
}
