using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private int pauseCount = 0; // keep count of how many times the escape key was pressed

    void Update()
    {
        //Enter if escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            // check the count of escape-key-pressed to determine pause/unpause
            pauseCount++;
            if (pauseCount % 2 == 1)
            {
                // pause and enable pause menu buttons
                Time.timeScale = 0f;
                Time.fixedDeltaTime = 0f;
                GameManager.instance.blurPanel.SetActive(true);
                GameManager.instance.pauseMenuButtons.SetActive(true);
                GameManager.instance.bgMusicAudioSource.pitch = 0f;
            }
            else
            {
                // unpause and disable pause menu buttons
                GameManager.instance.blurPanel.SetActive(false);
                GameManager.instance.pauseMenuButtons.SetActive(false);
            }
            
        }
        // unpause smoothly
        if (pauseCount%2==0 && Time.timeScale < 1f)
        {
            // slowly increase Time scale
            Time.timeScale += (1f / 2f) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

            //slowly increase pitch of the audio
            GameManager.instance.bgMusicAudioSource.pitch += (1f / 2f) * Time.unscaledDeltaTime;
            GameManager.instance.bgMusicAudioSource.pitch = Mathf.Clamp(GameManager.instance.bgMusicAudioSource.pitch, 0f, 1f);
        }
        

    }

    //public methods for button OnClick() functionality 
    public void Unpause()
    {
        GameManager.instance.PlayClickSound();
        GameManager.instance.blurPanel.SetActive(false);
        GameManager.instance.pauseMenuButtons.SetActive(false);
        pauseCount++;
    }

    public void quit()
    {
        GameManager.instance.PlayClickSound();
        SceneManager.LoadScene("StartScene");
    }
}
