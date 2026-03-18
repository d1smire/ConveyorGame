using UnityEngine;
using UnityEngine.SceneManagement;

public enum TypeOfAnimationEnd
{
    StartUpMenu,
    LeaveStartUp,
}

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _menuButtons;
    [SerializeField] private CameraMovement _cameraMovement;

    private int _modeIndex = 0;


    private void Start()
    {
        _menuButtons.SetActive(false);
        _cameraMovement.StartMovement();
        _cameraMovement.onAnimationEnd += StartUpAnimationEnd;
    }

    private void OnDestroy()
    {
        _cameraMovement.onAnimationEnd -= StartUpAnimationEnd;
    }

    private void StartUpAnimationEnd(bool isEnd, TypeOfAnimationEnd eventName) 
    {
        if (isEnd && eventName == TypeOfAnimationEnd.StartUpMenu)
        {
            _menuButtons.SetActive(true);
        }
        else if (isEnd && eventName == TypeOfAnimationEnd.LeaveStartUp) 
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
