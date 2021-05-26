using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{


    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Score found!");
            return;
        }
        instance = this;
    }
    #endregion

    
    [Header("Key entities")]
    public GameObject redPlayer;
    public GameObject bluePlayer;
    public GameObject ball;

    [Header("Score UI")]
    public TextMeshProUGUI playerRedScoretext;
    public TextMeshProUGUI playerBlueScoretext;

    [Header("Spawn points")]
    public Transform[] redSpawnPoints;
    public Transform[] blueSpawnPoints;

    [Header("Particle systems")]
    public GameObject redFire;
    public GameObject blueFire;
    public FlameThrower flameThrower;
    public List<GameObject> flameParticlesList;

    [Header("Pause menu items")]
    public GameObject blurPanel;
    public GameObject pauseMenuButtons;

    [Header("Audio")]
    public AudioSource bgMusicAudioSource;
    public AudioSource SFXaudioSource;
    public AudioClip buttonHoverClip, buttonClickClip, explosionClip, bounceClip;

    public float currentBallVeloctiy = 0;


    private int _playerRedScore = 0;
    private int _playerBlueScore = 0;
    private Rigidbody2D _ballRB;
    private SpriteRenderer _blueSR, _redSR;

    private void Start()
    {
        _ballRB = ball.GetComponent<Rigidbody2D>();
        flameParticlesList = new List<GameObject>();
        _blueSR = bluePlayer.GetComponent<SpriteRenderer>();
        _redSR = redPlayer.GetComponent<SpriteRenderer>();
        currentBallVeloctiy = ball.GetComponent<Ball>().BallSpeed;
    }
    public void PlayerRedScored()
    {
        _playerRedScore++;
        playerRedScoretext.text = _playerRedScore.ToString();
    }

    public void PlayerBlueScored()
    {
        _playerBlueScore++;
        playerBlueScoretext.text = _playerBlueScore.ToString();
    }

    public Transform GetRandomSpawnPoint(string player)
    {
        if(player == "blue")
        {
            return blueSpawnPoints[Random.Range(0, blueSpawnPoints.Length)];
        }
        else
        {
            return redSpawnPoints[Random.Range(0, redSpawnPoints.Length)];
        }
    }

    public void ResetPositions()
    {
        // reset ball and players' positions
        _ballRB.velocity = new Vector2(0, 0);
        ball.transform.position = new Vector2(0, 0);
        redPlayer.transform.position = new Vector2(9, 0);
        bluePlayer.transform.position = new Vector2(-9, 0);

        // disable all explosion effects
        bluePlayer.transform.GetChild(0).gameObject.SetActive(false);
        redPlayer.transform.GetChild(0).gameObject.SetActive(false);

        //enable the players' sprite renderers
        _blueSR.enabled = true;
        _redSR.enabled = true;
    }

    public void DestroyParticleSystems()
    {
        for (int i = 0; i < flameParticlesList.Count; i++)
        {
            if (flameParticlesList[i])
                Destroy(flameParticlesList[i]);
        }
        flameParticlesList.Clear();
    }

    public void PlayHoverSound()
    {
        SFXaudioSource.PlayOneShot(buttonHoverClip);
    }

    public void PlayClickSound()
    {
        SFXaudioSource.PlayOneShot(buttonClickClip);
    }
    public void PlayExplosionSound()
    {
        SFXaudioSource.PlayOneShot(explosionClip);
    }

    public Vector2 GetBallVelocity()
    {
        return _ballRB.velocity;
    }

    public void SetBallVelocity(Vector2 v)
    {
        _ballRB.velocity = v;
    }
}
