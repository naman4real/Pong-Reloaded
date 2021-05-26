using System.Collections;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI GoText;
    public static bool firstStart = false;
    public static bool gameStarted = false;
    int time = 3;
    private void Start()
    {
        StartCoroutine(countDowntTimer());
    }

    // count down to start the game 3...2...1.. GO!

    IEnumerator countDowntTimer()
    {
        // 1st second
        yield return new WaitForSeconds(1f);
        time--;
        timerText.text = time.ToString();

        // 2nd second
        yield return new WaitForSeconds(1f);
        time--;
        timerText.text = time.ToString();

        // 3rd second
        yield return new WaitForSeconds(1f);
        Destroy(timerText);
        timerText.enabled = false;
        GoText.text = "GO!";

        //4th second - start the game
        yield return new WaitForSeconds(1f);
        Destroy(GoText);
        firstStart = true;
        gameStarted = true;
        GameManager.instance.blurPanel.SetActive(false);
        GameManager.instance.bgMusicAudioSource.Play();
    }

}
