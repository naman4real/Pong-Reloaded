using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private int[] _direction = { -1, 1 };
    private int _redScoreStreak = 0;
    private int _blueScoreStreak = 0;
    private int _previousWinner = -1;
    private Rigidbody2D _rb;

    public float BallSpeed;

    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Countdown.firstStart)
        {
            LaunchBall();
            Countdown.firstStart = false;
        }
    }
    #region Ball Movement
    public void LaunchBall()
    {

        var xDir = _direction[Random.Range(0, _direction.Length)];
        var yDir = _direction[Random.Range(0, _direction.Length)];
        _rb.velocity = new Vector2(BallSpeed * xDir, BallSpeed * yDir); 
        Player.destroyed = false;
    }
    #endregion

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region Goal Detection and score streak calculation
        if (collision.collider.CompareTag("Goal"))
        {
            if(collision.collider.gameObject.name == "RedsGoal")
            {
                GameManager.instance.PlayerBlueScored();
                _redScoreStreak = 0;
                _blueScoreStreak += 1;
                if (_previousWinner == 1)
                {
                    _blueScoreStreak = 1;
                }
                if (_blueScoreStreak == 3)
                { 
                    _blueScoreStreak = 0;
                    StartCoroutine(WaitBeforeInstantiatingFireballs("blue"));
                }
                _previousWinner = 0;
            }
            else
            {
                GameManager.instance.PlayerRedScored();
                _blueScoreStreak = 0;
                _redScoreStreak += 1;
                if (_previousWinner == 0)
                {
                    _redScoreStreak = 1;
                }
                if (_redScoreStreak == 3)
                {
                    _redScoreStreak = 0;
                    StartCoroutine(WaitBeforeInstantiatingFireballs("red"));
                    
                }
                _previousWinner = 1;
            }
            GameManager.instance.ResetPositions();
            Invoke("LaunchBall", 1f);
        }

        

        #endregion

        #region Increase ball speed with every hit
        else if(collision.collider.CompareTag("Player"))
        {
            var currentVelocity = GameManager.instance.GetBallVelocity();
            if (currentVelocity.x > 0)
                currentVelocity.x += 0.5f;
            else
                currentVelocity.x -= 0.5f;
            if (currentVelocity.y > 0)
                currentVelocity.y += 0.5f;
            else
                currentVelocity.y -= 0.5f;
            GameManager.instance.SetBallVelocity(currentVelocity);
            GameManager.instance.SFXaudioSource.PlayOneShot(GameManager.instance.bounceClip);
        }
        #endregion


        IEnumerator WaitBeforeInstantiatingFireballs(string s)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(GameManager.instance.flameThrower.InstatiateParticleSystem(s));
        }
    }


 

}
