using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public static bool destroyed = false; // a flag used to indicate if the current player is destroyed or not
    public string player;
    public float movementSpeed;

    private float _movementDirection;
    private Rigidbody2D _rb;
    [SerializeField] private CameraShake _camShake;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Only take player inputs if the paddle is not destoryed
        if (!destroyed && Countdown.gameStarted)
        {
            if (player == "BluePlayer")
                _movementDirection = Input.GetAxisRaw("Vertical");

            else
                _movementDirection = Input.GetAxisRaw("Vertical2");

            _rb.velocity = new Vector2(0, _movementDirection * movementSpeed);
        }
    }

    #region Explosion detection

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name.Contains("Blue") && transform.gameObject.name == "RedPlayer")
        {
            destroyed = true;

            StartCoroutine(_camShake.CamShake(0.15f, 0.5f));

            // freeze ball
            GameManager.instance.ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            // increment score
            GameManager.instance.PlayerBlueScored();

            // enable explosion effect and play explosion sound
            transform.GetChild(0).gameObject.SetActive(true);
            GameManager.instance.PlayExplosionSound();


            // imitating player destruction. Can't use Destory() as this script will be destroyed too.
            transform.GetComponent<SpriteRenderer>().enabled = false;

            //  destroy all flame particle systems if remaining, reset the player and ball positions and launch the ball again.
            StartCoroutine(DestoryResetAndLaunch());
        }
        else if (collision.gameObject.name.Contains("Red") && transform.gameObject.name == "BluePlayer")
        {
            destroyed = true;          
            GameManager.instance.ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GameManager.instance.PlayerRedScored();
            GameManager.instance.PlayExplosionSound();
            transform.GetChild(0).gameObject.SetActive(true); 
            transform.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(DestoryResetAndLaunch());
            StartCoroutine(_camShake.CamShake(0.15f, 0.5f));
        }

    }

    IEnumerator DestoryResetAndLaunch()
    {
        GameManager.instance.DestroyParticleSystems(); // destroy all flame/fire particle systems if any active.
        yield return new WaitForSeconds(1f);
        GameManager.instance.ResetPositions(); // reset all postions to start next round
        yield return new WaitForSeconds(1f);
        GameManager.instance.ball.GetComponent<Ball>().LaunchBall(); // launch the ball and start
        
    }

    #endregion




}
