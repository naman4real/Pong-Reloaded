using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject[] buttonsAndTitle;
    public AudioSource audioSource;
    public AudioClip buttonHoverClip, buttonClickClip;

    //public methods for buttons' OnClick() functionality
    public void ShowInstructions()
    {
        PlayButtonClickSound();
        instructionsPanel.SetActive(true);
        for(int i = 0; i < buttonsAndTitle.Length; i++)
            buttonsAndTitle[i].SetActive(false);
        
    }

    public void HideInstructions()
    {
        PlayButtonClickSound();
        instructionsPanel.SetActive(false);
        for (int i = 0; i < buttonsAndTitle.Length; i++)
            buttonsAndTitle[i].SetActive(true);

    }

    public void quit()
    {
        PlayButtonClickSound();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
                Application.OpenURL(webplayerQuitURL);
        #else
                Application.Quit();
        #endif
    }

    public void LoadGameScene()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene("GameScene");
    }

    public void PlayButtonHoverSound()
    {
        audioSource.PlayOneShot(buttonHoverClip);
    }

    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickClip);
    }
}
